USE [master]
GO
/****** Object:  Database [GestionInventario]    Script Date: 10/04/2020 02:43:53 a. m. ******/
CREATE DATABASE [GestionInventario]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GestionInventario', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\GestionInventario.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GestionInventario_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\GestionInventario_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [GestionInventario] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GestionInventario].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GestionInventario] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GestionInventario] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GestionInventario] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GestionInventario] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GestionInventario] SET ARITHABORT OFF 
GO
ALTER DATABASE [GestionInventario] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GestionInventario] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GestionInventario] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GestionInventario] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GestionInventario] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GestionInventario] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GestionInventario] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GestionInventario] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GestionInventario] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GestionInventario] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GestionInventario] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GestionInventario] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GestionInventario] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GestionInventario] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GestionInventario] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GestionInventario] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GestionInventario] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GestionInventario] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GestionInventario] SET  MULTI_USER 
GO
ALTER DATABASE [GestionInventario] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GestionInventario] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GestionInventario] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GestionInventario] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [GestionInventario] SET DELAYED_DURABILITY = DISABLED 
GO
USE [GestionInventario]
GO
/****** Object:  Table [dbo].[Articulos]    Script Date: 10/04/2020 02:43:54 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Articulos](
	[IdArticulo] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](50) NULL,
	[Descripcion] [varchar](50) NULL,
	[Existencia] [int] NULL,
	[Precio] [decimal](18, 3) NULL,
 CONSTRAINT [PK_Articulos] PRIMARY KEY CLUSTERED 
(
	[IdArticulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Personal]    Script Date: 10/04/2020 02:43:54 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Personal](
	[IdPersonal] [int] IDENTITY(1,1) NOT NULL,
	[ID] [int] NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
	[Telefono] [varchar](50) NULL,
 CONSTRAINT [PK_Personal] PRIMARY KEY CLUSTERED 
(
	[IdPersonal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 10/04/2020 02:43:54 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Rol] [varchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TempoArticulos]    Script Date: 10/04/2020 02:43:54 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TempoArticulos](
	[IdTempo] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](50) NULL,
	[Descripcion] [varchar](50) NULL,
	[Cantidad] [int] NULL,
	[IdUsuario] [int] NULL,
 CONSTRAINT [PK_TempoArticulos] PRIMARY KEY CLUSTERED 
(
	[IdTempo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 10/04/2020 02:43:54 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
	[Telefono] [varchar](50) NULL,
	[Direccion] [varchar](50) NULL,
	[Correo] [varchar](50) NULL,
	[Usuario] [varchar](50) NULL,
	[Contraseña] [varchar](50) NULL,
	[Rol] [varchar](50) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Articulos] ON 

GO
INSERT [dbo].[Articulos] ([IdArticulo], [Codigo], [Descripcion], [Existencia], [Precio]) VALUES (2, N'75014785', N'Trapeador', 100, CAST(123.000 AS Decimal(18, 3)))
GO
INSERT [dbo].[Articulos] ([IdArticulo], [Codigo], [Descripcion], [Existencia], [Precio]) VALUES (3, N'7504569', N'Recogedor', 100, CAST(30.000 AS Decimal(18, 3)))
GO
INSERT [dbo].[Articulos] ([IdArticulo], [Codigo], [Descripcion], [Existencia], [Precio]) VALUES (5, N'789456', N'Martillo', 100, CAST(20.000 AS Decimal(18, 3)))
GO
INSERT [dbo].[Articulos] ([IdArticulo], [Codigo], [Descripcion], [Existencia], [Precio]) VALUES (6, N'750123456', N'Cubeta', 100, CAST(30.000 AS Decimal(18, 3)))
GO
INSERT [dbo].[Articulos] ([IdArticulo], [Codigo], [Descripcion], [Existencia], [Precio]) VALUES (8, N'756231000', N'Escoba', 100, CAST(20.000 AS Decimal(18, 3)))
GO
SET IDENTITY_INSERT [dbo].[Articulos] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

GO
INSERT [dbo].[Roles] ([IdRol], [Rol]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([IdRol], [Rol]) VALUES (2, N'User')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[usuarios] ON 

GO
INSERT [dbo].[usuarios] ([IdUsuario], [Nombre], [Apellido], [Telefono], [Direccion], [Correo], [Usuario], [Contraseña], [Rol]) VALUES (1, N'Francisco', N'Freyre', N'6183005074', N'no lo se rick', N'1010@gmail.com', N'FOKO', N'iX5JXkDCrPG+LV9S2REeMA==', N'Admin')
GO
INSERT [dbo].[usuarios] ([IdUsuario], [Nombre], [Apellido], [Telefono], [Direccion], [Correo], [Usuario], [Contraseña], [Rol]) VALUES (2, N'Jesus Antonio', N'Santillan Cepeda', N'6181234567', N'Daasd 252', N'asdasd@gmail.com', N'Jesusk007', N'mjjlNMJjznPe/z7piNSZvQ==', N'Admin')
GO
SET IDENTITY_INSERT [dbo].[usuarios] OFF
GO
USE [master]
GO
ALTER DATABASE [GestionInventario] SET  READ_WRITE 
GO
