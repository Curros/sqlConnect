using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sqltestquery : Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

            BindGrid();

            #region TableDraw
            //Populating a DataTable from database.
            DataTable dt = this.GetData();

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            html.Append("<table border = '1'>");

            //Building the Header row.
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                if (GetShowOrNot(column.ColumnName))
                {
                    html.Append("<th>");
                    html.Append(GetNewHeaderName(column.ColumnName));
                    html.Append("</th>");
                }
            }
            html.Append("</tr>");

            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    if (GetShowOrNot(column.ColumnName))
                    {
                        html.Append("<td>");
                        html.Append(row[column.ColumnName]);
                        html.Append("</td>");
                    }
                }
                html.Append("</tr>");
            }

            //Table end.
            html.Append("</table>");

            //Append the HTML string to Placeholder.
            placeTable.Controls.Add(new Literal { Text = html.ToString() }); 
            #endregion
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        //uploadToSQL(FileUpload1);
        uploadToFTP(FileUpload1);
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    private void uploadToSQL( FileUpload fUpload ) {
        string filename = Path.GetFileName(fUpload.PostedFile.FileName);
        string contentType = fUpload.PostedFile.ContentType;
        using (Stream fs = fUpload.PostedFile.InputStream)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "insert into [tblFiles] values (@Name, @ContentType, @Data)";
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Name", filename);
                        cmd.Parameters.AddWithValue("@ContentType", contentType);
                        cmd.Parameters.AddWithValue("@Data", bytes);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Send the file to a FTP server.
    /// </summary>
    /// <param name="fUpload"></param>
    /// <remarks>http://www.aspsnippets.com/Articles/Uploading-Files-to-FTP-Server-programmatically-in-ASPNet-using-C-and-VBNet.aspx</remarks>
    private void uploadToFTP( FileUpload fUpload) {

        //FTP Server Config.
        string ftp = ConfigurationManager.AppSettings["ftpURL"].ToString();
        string usr = ConfigurationManager.AppSettings["ftpUser"].ToString();
        string psw = ConfigurationManager.AppSettings["ftpPass"].ToString();

        //FTP Folder name. Leave blank if you want to upload to root folder.
        string ftpFolder = "";

        byte[] fileBytes = null;

        //Read the FileName and convert it to Byte array.
        string fileName = Path.GetFileName(FileUpload1.FileName);
        using (StreamReader fileStream = new StreamReader(FileUpload1.PostedFile.InputStream))
        {
            fileBytes = Encoding.UTF8.GetBytes(fileStream.ReadToEnd());
            fileStream.Close();
        }

        try
        {
            //Create FTP Request.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + fileName);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            //Enter FTP Server credentials.
            request.Credentials = new NetworkCredential(usr, psw);
            request.ContentLength = fileBytes.Length;
            request.UsePassive = true;
            request.UseBinary = true;
            request.ServicePoint.ConnectionLimit = fileBytes.Length;
            request.EnableSsl = false;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileBytes, 0, fileBytes.Length);
                requestStream.Close();
            }

            //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            //lblMessage.Text += fileName + " uploaded.<br />";
            //response.Close();
        }
        catch (WebException ex)
        {
            throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
        }
    }


    protected void DownloadFile(object sender, EventArgs e)
    {
        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        string fileName, contentType;
        string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Name, Data, ContentType from tblFiles where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    bytes = (byte[])sdr["Data"];
                    contentType = sdr["ContentType"].ToString();
                    fileName = sdr["Name"].ToString();
                }
                con.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }

    private void BindGrid()
    {
        string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Id, Name from tblFiles";
                cmd.Connection = con;
                con.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
                con.Close();
            }
        }
    }

    private DataTable GetData()
    {
        string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblFiles"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    private bool GetShowOrNot(string headrName ) {
        string[] scaped = { "Data", "id" };
        return !scaped.Contains(headrName);
    }

    private string GetNewHeaderName(string headrName)
    {
        switch (headrName.ToUpper())
        {
            case "NAME":
                return "File Name";
            case "MODDATE":
                return "Modification Date";
            default:
                return headrName;
        }
    }



}