USE [master]
GO
/****** Object:  Database [NetCoreWebApp]    Script Date: 2/3/2023 2:57:05 PM ******/
CREATE DATABASE [NetCoreWebApp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NetCoreWebApp_Data', FILENAME = N'c:\dzsqls\NetCoreWebApp.mdf' , SIZE = 8192KB , MAXSIZE = 30720KB , FILEGROWTH = 22528KB )
 LOG ON 
( NAME = N'NetCoreWebApp_Logs', FILENAME = N'c:\dzsqls\NetCoreWebApp.ldf' , SIZE = 8192KB , MAXSIZE = 30720KB , FILEGROWTH = 22528KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [NetCoreWebApp] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NetCoreWebApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NetCoreWebApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NetCoreWebApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NetCoreWebApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET  ENABLE_BROKER 
GO
ALTER DATABASE [NetCoreWebApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NetCoreWebApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NetCoreWebApp] SET  MULTI_USER 
GO
ALTER DATABASE [NetCoreWebApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NetCoreWebApp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NetCoreWebApp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NetCoreWebApp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NetCoreWebApp] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NetCoreWebApp] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [NetCoreWebApp] SET QUERY_STORE = OFF
GO
USE [NetCoreWebApp]
GO
/****** Object:  User [netzer288_SQLLogin_1]    Script Date: 2/3/2023 2:57:06 PM ******/
CREATE USER [netzer288_SQLLogin_1] FOR LOGIN [netzer288_SQLLogin_1] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [netzer288_SQLLogin_1]
GO
/****** Object:  Schema [netzer288_SQLLogin_1]    Script Date: 2/3/2023 2:57:06 PM ******/
CREATE SCHEMA [netzer288_SQLLogin_1]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 2/3/2023 2:57:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[id] [uniqueidentifier] NOT NULL,
	[customer] [varchar](500) NOT NULL,
	[country] [varchar](100) NOT NULL,
	[city] [varchar](100) NOT NULL,
	[contactManager] [varchar](100) NOT NULL,
	[contactNumber] [varchar](50) NOT NULL,
	[status] [bit] NOT NULL,
	[createAt] [datetime] NOT NULL,
	[modifiedAt] [datetime] NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orderdetails]    Script Date: 2/3/2023 2:57:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orderdetails](
	[id] [uniqueidentifier] NOT NULL,
	[orderID] [uniqueidentifier] NOT NULL,
	[productID] [uniqueidentifier] NOT NULL,
	[price] [float] NOT NULL,
	[quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 2/3/2023 2:57:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[id] [uniqueidentifier] NOT NULL,
	[customerId] [uniqueidentifier] NOT NULL,
	[shipAddress] [varchar](100) NOT NULL,
	[shipAddress2] [varchar](100) NOT NULL,
	[city] [varchar](50) NOT NULL,
	[state] [varchar](50) NOT NULL,
	[zip] [varchar](20) NOT NULL,
	[country] [varchar](50) NOT NULL,
	[phone] [varchar](20) NOT NULL,
	[date] [datetime] NOT NULL,
	[shipped] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[productcategories]    Script Date: 2/3/2023 2:57:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productcategories](
	[id] [uniqueidentifier] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[createAt] [datetime] NOT NULL,
	[modifiedAt] [datetime] NULL,
 CONSTRAINT [PK__productc__23CAF1F831D77CB9] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 2/3/2023 2:57:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [uniqueidentifier] NOT NULL,
	[sku] [varchar](50) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[price] [float] NOT NULL,
	[shortDesc] [varchar](1000) NOT NULL,
	[longDesc] [text] NOT NULL,
	[image] [varchar](100) NULL,
	[categoryID] [uniqueidentifier] NULL,
	[Stock] [float] NULL,
	[location] [varchar](250) NULL,
	[createAt] [datetime] NOT NULL,
	[modifiedAt] [datetime] NULL,
 CONSTRAINT [PK__products__3213E83FB10F6630] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 2/3/2023 2:57:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[username] [varchar](15) NOT NULL,
	[password] [varchar](20) NOT NULL,
	[status] [bit] NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[customers] ([id], [customer], [country], [city], [contactManager], [contactNumber], [status], [createAt], [modifiedAt]) VALUES (N'7508f689-4044-4474-bbb4-3f33cb617e7d', N'Prueba2', N'Prueba2', N'Prueba2', N'Prueba2', N'Prueba2', 1, CAST(N'2023-02-02T20:19:31.330' AS DateTime), NULL)
GO
INSERT [dbo].[customers] ([id], [customer], [country], [city], [contactManager], [contactNumber], [status], [createAt], [modifiedAt]) VALUES (N'e9eca446-8f26-4059-ab9d-906520b07f4c', N'Prueba', N'Prueba', N'Prueba', N'Prueba', N'Prueba', 1, CAST(N'2023-02-02T20:14:38.817' AS DateTime), NULL)
GO
INSERT [dbo].[orderdetails] ([id], [orderID], [productID], [price], [quantity]) VALUES (N'00000000-0000-0000-0000-000000000000', N'ce9ece33-48c8-4644-8021-8d0a973bab5c', N'566c850c-3d23-4baf-a449-59d0fe92dd91', 15, 12)
GO
INSERT [dbo].[orderdetails] ([id], [orderID], [productID], [price], [quantity]) VALUES (N'f55451ac-5f90-48a6-ad65-11d36a442a6e', N'849e6b49-9e83-4971-86c7-7d251b5d4423', N'566c850c-3d23-4baf-a449-59d0fe92dd91', 15, 10)
GO
INSERT [dbo].[orderdetails] ([id], [orderID], [productID], [price], [quantity]) VALUES (N'2d355f34-db3d-41db-9ed8-42ab81eb5985', N'849e6b49-9e83-4971-86c7-7d251b5d4423', N'7117dd76-e9cc-42d0-8cdc-6ebe082f6e9c', 11, 1)
GO
INSERT [dbo].[orderdetails] ([id], [orderID], [productID], [price], [quantity]) VALUES (N'b911024a-3ace-4cbb-a545-44ab239a5f48', N'849e6b49-9e83-4971-86c7-7d251b5d4423', N'566c850c-3d23-4baf-a449-59d0fe92dd91', 15, 21)
GO
INSERT [dbo].[orderdetails] ([id], [orderID], [productID], [price], [quantity]) VALUES (N'939ddb70-c76a-42f9-a03d-6048313718ae', N'849e6b49-9e83-4971-86c7-7d251b5d4423', N'566c850c-3d23-4baf-a449-59d0fe92dd91', 15, 111)
GO
INSERT [dbo].[orderdetails] ([id], [orderID], [productID], [price], [quantity]) VALUES (N'4066455f-bf99-4650-8350-7aa980ab7d8f', N'849e6b49-9e83-4971-86c7-7d251b5d4423', N'7117dd76-e9cc-42d0-8cdc-6ebe082f6e9c', 1, 1)
GO
INSERT [dbo].[orderdetails] ([id], [orderID], [productID], [price], [quantity]) VALUES (N'c648f426-52f4-4b6d-a782-d820594768d2', N'849e6b49-9e83-4971-86c7-7d251b5d4423', N'566c850c-3d23-4baf-a449-59d0fe92dd91', 15, 123)
GO
INSERT [dbo].[orders] ([id], [customerId], [shipAddress], [shipAddress2], [city], [state], [zip], [country], [phone], [date], [shipped]) VALUES (N'849e6b49-9e83-4971-86c7-7d251b5d4423', N'e9eca446-8f26-4059-ab9d-906520b07f4c', N'Prueba', N'Prueba', N'Prueba', N'Prueba', N'Prueba', N'Prueba', N'Prueba', CAST(N'2023-02-02T22:53:44.903' AS DateTime), 1)
GO
INSERT [dbo].[orders] ([id], [customerId], [shipAddress], [shipAddress2], [city], [state], [zip], [country], [phone], [date], [shipped]) VALUES (N'ce9ece33-48c8-4644-8021-8d0a973bab5c', N'7508f689-4044-4474-bbb4-3f33cb617e7d', N'Prueba2', N'Prueba2', N'Prueba2', N'Prueba2', N'Prueba2', N'Prueba2', N'Prueba2', CAST(N'2023-02-03T14:26:26.810' AS DateTime), 1)
GO
INSERT [dbo].[productcategories] ([id], [name], [createAt], [modifiedAt]) VALUES (N'b5d17403-6fd7-4297-b4b8-0d6729f20b99', N'aaa', CAST(N'2023-02-02T18:58:17.470' AS DateTime), CAST(N'2023-02-02T21:52:51.057' AS DateTime))
GO
INSERT [dbo].[productcategories] ([id], [name], [createAt], [modifiedAt]) VALUES (N'82d4a6b9-9a3f-4358-87bc-1c0691978ee6', N'Running', CAST(N'2023-02-02T18:58:17.470' AS DateTime), NULL)
GO
INSERT [dbo].[productcategories] ([id], [name], [createAt], [modifiedAt]) VALUES (N'33a945bc-e1bf-4180-a6a9-239754e07288', N'Track and Trail', CAST(N'2023-02-02T18:58:17.470' AS DateTime), NULL)
GO
INSERT [dbo].[productcategories] ([id], [name], [createAt], [modifiedAt]) VALUES (N'd4975d09-0923-42bf-be79-2a4297ca7206', N'HIking', CAST(N'2023-02-02T18:58:17.470' AS DateTime), NULL)
GO
INSERT [dbo].[productcategories] ([id], [name], [createAt], [modifiedAt]) VALUES (N'd153c518-b011-4b60-8c29-5a217d63ac44', N'Walking', CAST(N'2023-02-02T18:58:17.470' AS DateTime), NULL)
GO
INSERT [dbo].[productcategories] ([id], [name], [createAt], [modifiedAt]) VALUES (N'c32ce310-e23f-4e1b-b0fa-c3a45cf49928', N'www', CAST(N'2023-02-02T21:53:33.000' AS DateTime), NULL)
GO
INSERT [dbo].[productcategories] ([id], [name], [createAt], [modifiedAt]) VALUES (N'5f013848-7105-4c50-ae6a-dd20acb7fc3f', N'Short Sleave', CAST(N'2023-02-02T18:58:17.470' AS DateTime), NULL)
GO
INSERT [dbo].[products] ([id], [sku], [name], [price], [shortDesc], [longDesc], [image], [categoryID], [Stock], [location], [createAt], [modifiedAt]) VALUES (N'566c850c-3d23-4baf-a449-59d0fe92dd91', N'Prueba', N'Prueba', 15, N'Prueba', N'Prueba', N'Prueba', N'd153c518-b011-4b60-8c29-5a217d63ac44', 10, N'Prueba', CAST(N'2023-02-02T19:36:22.443' AS DateTime), CAST(N'2023-02-03T11:33:37.083' AS DateTime))
GO
INSERT [dbo].[products] ([id], [sku], [name], [price], [shortDesc], [longDesc], [image], [categoryID], [Stock], [location], [createAt], [modifiedAt]) VALUES (N'7117dd76-e9cc-42d0-8cdc-6ebe082f6e9c', N'Prueba2', N'Prueba2', 11, N'Prueba2', N'Prueba2', N'Prueba2', N'33a945bc-e1bf-4180-a6a9-239754e07288', 11, N'Prueba2', CAST(N'2023-02-02T21:49:04.357' AS DateTime), NULL)
GO
INSERT [dbo].[users] ([username], [password], [status], [date]) VALUES (N'luis', N'123', 1, CAST(N'2023-02-02T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[users] ([username], [password], [status], [date]) VALUES (N'martin', N'123', 1, CAST(N'2023-02-02T00:00:00.000' AS DateTime))
GO
ALTER TABLE [dbo].[orders] ADD  DEFAULT (getdate()) FOR [date]
GO
ALTER TABLE [dbo].[products] ADD  CONSTRAINT [DF__products__Stock__2E1BDC42]  DEFAULT (NULL) FOR [Stock]
GO
ALTER TABLE [dbo].[orderdetails]  WITH CHECK ADD  CONSTRAINT [FK_orderdetails_orders] FOREIGN KEY([orderID])
REFERENCES [dbo].[orders] ([id])
GO
ALTER TABLE [dbo].[orderdetails] CHECK CONSTRAINT [FK_orderdetails_orders]
GO
ALTER TABLE [dbo].[orderdetails]  WITH CHECK ADD  CONSTRAINT [FK_orderdetails_products] FOREIGN KEY([productID])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[orderdetails] CHECK CONSTRAINT [FK_orderdetails_products]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_customers] FOREIGN KEY([customerId])
REFERENCES [dbo].[customers] ([id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_customers]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_products_productcategories] FOREIGN KEY([categoryID])
REFERENCES [dbo].[productcategories] ([id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_products_productcategories]
GO
USE [master]
GO
ALTER DATABASE [NetCoreWebApp] SET  READ_WRITE 
GO
