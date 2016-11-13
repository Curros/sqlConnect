USE [master]
GO
/****** Object:  Database [dbFiles]    Script Date: 9/28/2012 5:48:32 PM ******/
CREATE DATABASE [dbFiles]
GO
ALTER DATABASE [dbFiles] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbFiles] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbFiles] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbFiles] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbFiles] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbFiles] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbFiles] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [dbFiles] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbFiles] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbFiles] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbFiles] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbFiles] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbFiles] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbFiles] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbFiles] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbFiles] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbFiles] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbFiles] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbFiles] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbFiles] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbFiles] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbFiles] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbFiles] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbFiles] SET  MULTI_USER 
GO
ALTER DATABASE [dbFiles] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbFiles] SET DB_CHAINING OFF 
GO
USE [dbFiles]
GO
/****** Object:  Table [dbo].[tblFiles]    Script Date: 9/28/2012 5:48:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblFiles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ContentType] [nvarchar](200) NOT NULL,
	[Data] [varbinary](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [dbFiles] SET  READ_WRITE 
GO