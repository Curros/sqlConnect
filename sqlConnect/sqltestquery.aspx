<%@ Page Title="Sql Test Query" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="sqltestquery.aspx.cs" Inherits="sqltestquery" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <div class="row">
        <div class="col-md-4">
            <asp:Label runat="server" ID="lblFile">Select a file to upload:</asp:Label>
        </div>
        <div class="col-md-8">
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <h3>Data from GridView table:</h3>
            <asp:GridView ID="GridView1" runat="server" HeaderStyle-BackColor="" HeaderStyle-ForeColor=""
                RowStyle-BackColor="" AlternatingRowStyle-BackColor="" AlternatingRowStyle-ForeColor=""
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="File Name" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                                CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <h3>Data from self constructed table:</h3>
            <asp:PlaceHolder ID="placeTable" runat="server"></asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
