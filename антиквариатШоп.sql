USE [master]
GO
/****** Object:  Database [AntiqueShop]    Script Date: 19.05.2025 14:05:46 ******/
CREATE DATABASE [AntiqueShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AntiqueShop', FILENAME = N'C:\Users\10230251\AntiqueShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AntiqueShop_log', FILENAME = N'C:\Users\10230251\AntiqueShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AntiqueShop] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AntiqueShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AntiqueShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AntiqueShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AntiqueShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AntiqueShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AntiqueShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [AntiqueShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AntiqueShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AntiqueShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AntiqueShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AntiqueShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AntiqueShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AntiqueShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AntiqueShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AntiqueShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AntiqueShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AntiqueShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AntiqueShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AntiqueShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AntiqueShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AntiqueShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AntiqueShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AntiqueShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AntiqueShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AntiqueShop] SET  MULTI_USER 
GO
ALTER DATABASE [AntiqueShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AntiqueShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AntiqueShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AntiqueShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AntiqueShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AntiqueShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AntiqueShop] SET QUERY_STORE = OFF
GO
USE [AntiqueShop]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 19.05.2025 14:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[id_cart] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NULL,
	[id_item] [int] NULL,
	[quantity] [int] NOT NULL,
	[added_date] [date] NULL,
	[is_active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_cart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 19.05.2025 14:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[id_category] [int] IDENTITY(1,1) NOT NULL,
	[name_category] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_category] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favorites]    Script Date: 19.05.2025 14:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorites](
	[id_favorite] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NULL,
	[id_item] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_favorite] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item_logs]    Script Date: 19.05.2025 14:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item_logs](
	[id_log] [int] IDENTITY(1,1) NOT NULL,
	[id_item] [int] NULL,
	[changed_field] [nvarchar](100) NULL,
	[old_value] [nvarchar](255) NULL,
	[new_value] [nvarchar](255) NULL,
	[change_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_log] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 19.05.2025 14:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[id_item] [int] IDENTITY(1,1) NOT NULL,
	[name_item] [nvarchar](100) NOT NULL,
	[year] [int] NULL,
	[condition] [nvarchar](50) NULL,
	[authenticity] [nvarchar](50) NULL,
	[purchase_price] [decimal](10, 2) NULL,
	[selling_price] [decimal](10, 2) NULL,
	[arrival_date] [date] NULL,
	[image] [nvarchar](255) NULL,
	[id_category] [int] NULL,
	[id_material] [int] NULL,
	[id_supplier] [int] NULL,
	[id_status] [int] NULL,
	[id_origin_country] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_item] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Materials]    Script Date: 19.05.2025 14:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Materials](
	[id_material] [int] IDENTITY(1,1) NOT NULL,
	[name_material] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_material] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Origin_countries]    Script Date: 19.05.2025 14:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Origin_countries](
	[id_origin_country] [int] IDENTITY(1,1) NOT NULL,
	[name_country] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_origin_country] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 19.05.2025 14:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id_role] [int] IDENTITY(1,1) NOT NULL,
	[name_role] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 19.05.2025 14:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[id_sale] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NULL,
	[id_item] [int] NULL,
	[sale_date] [date] NULL,
	[final_price] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_sale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 19.05.2025 14:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[id_status] [int] IDENTITY(1,1) NOT NULL,
	[name_status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 19.05.2025 14:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[id_supplier] [int] IDENTITY(1,1) NOT NULL,
	[name_supplier] [nvarchar](100) NULL,
	[specialization] [nvarchar](100) NULL,
	[authenticity] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_supplier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 19.05.2025 14:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id_user] [int] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](50) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
	[first_name] [nvarchar](50) NULL,
	[middle_name] [nvarchar](50) NULL,
	[last_name] [nvarchar](50) NULL,
	[email] [nvarchar](100) NULL,
	[preferences] [nvarchar](255) NULL,
	[id_role] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cart] ON 

INSERT [dbo].[Cart] ([id_cart], [id_user], [id_item], [quantity], [added_date], [is_active]) VALUES (1, 2, 3, 1, CAST(N'2024-04-01' AS Date), 1)
INSERT [dbo].[Cart] ([id_cart], [id_user], [id_item], [quantity], [added_date], [is_active]) VALUES (2, 2, 5, 2, CAST(N'2024-04-02' AS Date), 1)
INSERT [dbo].[Cart] ([id_cart], [id_user], [id_item], [quantity], [added_date], [is_active]) VALUES (3, 3, 1, 1, CAST(N'2024-04-03' AS Date), 0)
INSERT [dbo].[Cart] ([id_cart], [id_user], [id_item], [quantity], [added_date], [is_active]) VALUES (4, 4, 7, 1, CAST(N'2024-04-04' AS Date), 1)
INSERT [dbo].[Cart] ([id_cart], [id_user], [id_item], [quantity], [added_date], [is_active]) VALUES (5, 5, 6, 3, CAST(N'2024-04-05' AS Date), 1)
INSERT [dbo].[Cart] ([id_cart], [id_user], [id_item], [quantity], [added_date], [is_active]) VALUES (6, 6, 2, 1, CAST(N'2024-04-06' AS Date), 0)
INSERT [dbo].[Cart] ([id_cart], [id_user], [id_item], [quantity], [added_date], [is_active]) VALUES (7, 7, 9, 2, CAST(N'2024-04-07' AS Date), 1)
INSERT [dbo].[Cart] ([id_cart], [id_user], [id_item], [quantity], [added_date], [is_active]) VALUES (8, 8, 4, 1, CAST(N'2024-04-08' AS Date), 1)
INSERT [dbo].[Cart] ([id_cart], [id_user], [id_item], [quantity], [added_date], [is_active]) VALUES (9, 9, 8, 1, CAST(N'2024-04-09' AS Date), 1)
INSERT [dbo].[Cart] ([id_cart], [id_user], [id_item], [quantity], [added_date], [is_active]) VALUES (10, 10, 10, 1, CAST(N'2024-04-10' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Cart] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([id_category], [name_category]) VALUES (1, N'Coins')
INSERT [dbo].[Categories] ([id_category], [name_category]) VALUES (2, N'Stamps')
INSERT [dbo].[Categories] ([id_category], [name_category]) VALUES (3, N'Books')
INSERT [dbo].[Categories] ([id_category], [name_category]) VALUES (4, N'Paintings')
INSERT [dbo].[Categories] ([id_category], [name_category]) VALUES (5, N'Figurines')
INSERT [dbo].[Categories] ([id_category], [name_category]) VALUES (6, N'Clocks')
INSERT [dbo].[Categories] ([id_category], [name_category]) VALUES (7, N'Jewelry')
INSERT [dbo].[Categories] ([id_category], [name_category]) VALUES (8, N'Sculptures')
INSERT [dbo].[Categories] ([id_category], [name_category]) VALUES (9, N'Furniture')
INSERT [dbo].[Categories] ([id_category], [name_category]) VALUES (10, N'Lamps')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Favorites] ON 

INSERT [dbo].[Favorites] ([id_favorite], [id_user], [id_item]) VALUES (1, 2, 3)
INSERT [dbo].[Favorites] ([id_favorite], [id_user], [id_item]) VALUES (2, 2, 5)
INSERT [dbo].[Favorites] ([id_favorite], [id_user], [id_item]) VALUES (3, 3, 1)
INSERT [dbo].[Favorites] ([id_favorite], [id_user], [id_item]) VALUES (4, 4, 7)
INSERT [dbo].[Favorites] ([id_favorite], [id_user], [id_item]) VALUES (5, 5, 6)
INSERT [dbo].[Favorites] ([id_favorite], [id_user], [id_item]) VALUES (6, 6, 2)
INSERT [dbo].[Favorites] ([id_favorite], [id_user], [id_item]) VALUES (7, 7, 9)
INSERT [dbo].[Favorites] ([id_favorite], [id_user], [id_item]) VALUES (8, 8, 4)
INSERT [dbo].[Favorites] ([id_favorite], [id_user], [id_item]) VALUES (9, 9, 8)
INSERT [dbo].[Favorites] ([id_favorite], [id_user], [id_item]) VALUES (10, 10, 10)
SET IDENTITY_INSERT [dbo].[Favorites] OFF
GO
SET IDENTITY_INSERT [dbo].[Item_logs] ON 

INSERT [dbo].[Item_logs] ([id_log], [id_item], [changed_field], [old_value], [new_value], [change_date]) VALUES (1, 1, N'selling_price', N'1400', N'1500', CAST(N'2024-04-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Item_logs] ([id_log], [id_item], [changed_field], [old_value], [new_value], [change_date]) VALUES (2, 2, N'status', N'Available', N'Sold', CAST(N'2024-04-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Item_logs] ([id_log], [id_item], [changed_field], [old_value], [new_value], [change_date]) VALUES (3, 3, N'condition', N'Good', N'Poor', CAST(N'2024-04-03T00:00:00.000' AS DateTime))
INSERT [dbo].[Item_logs] ([id_log], [id_item], [changed_field], [old_value], [new_value], [change_date]) VALUES (4, 4, N'name_item', N'Painting', N'Retro Painting', CAST(N'2024-04-04T00:00:00.000' AS DateTime))
INSERT [dbo].[Item_logs] ([id_log], [id_item], [changed_field], [old_value], [new_value], [change_date]) VALUES (5, 5, N'material', N'Metal', N'Silver', CAST(N'2024-04-05T00:00:00.000' AS DateTime))
INSERT [dbo].[Item_logs] ([id_log], [id_item], [changed_field], [old_value], [new_value], [change_date]) VALUES (6, 6, N'price', N'700', N'800', CAST(N'2024-04-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Item_logs] ([id_log], [id_item], [changed_field], [old_value], [new_value], [change_date]) VALUES (7, 7, N'authenticity', N'Unknown', N'Verified', CAST(N'2024-04-07T00:00:00.000' AS DateTime))
INSERT [dbo].[Item_logs] ([id_log], [id_item], [changed_field], [old_value], [new_value], [change_date]) VALUES (8, 8, N'status', N'Available', N'Sold', CAST(N'2024-04-08T00:00:00.000' AS DateTime))
INSERT [dbo].[Item_logs] ([id_log], [id_item], [changed_field], [old_value], [new_value], [change_date]) VALUES (9, 9, N'arrival_date', N'2024-02-01', N'2024-03-15', CAST(N'2024-04-09T00:00:00.000' AS DateTime))
INSERT [dbo].[Item_logs] ([id_log], [id_item], [changed_field], [old_value], [new_value], [change_date]) VALUES (10, 10, N'selling_price', N'2900', N'3000', CAST(N'2024-04-10T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Item_logs] OFF
GO
SET IDENTITY_INSERT [dbo].[Items] ON 

INSERT [dbo].[Items] ([id_item], [name_item], [year], [condition], [authenticity], [purchase_price], [selling_price], [arrival_date], [image], [id_category], [id_material], [id_supplier], [id_status], [id_origin_country]) VALUES (1, N'Gold Coin 1898', 1898, N'Good', N'Verified', CAST(1000.00 AS Decimal(10, 2)), CAST(1500.00 AS Decimal(10, 2)), CAST(N'2024-01-15' AS Date), N'gold_coin.jpg', 1, 1, 2, 1, 1)
INSERT [dbo].[Items] ([id_item], [name_item], [year], [condition], [authenticity], [purchase_price], [selling_price], [arrival_date], [image], [id_category], [id_material], [id_supplier], [id_status], [id_origin_country]) VALUES (2, N'Stamp 1945', 1945, N'Fair', N'Verified', CAST(200.00 AS Decimal(10, 2)), CAST(350.00 AS Decimal(10, 2)), CAST(N'2024-02-01' AS Date), N'stamp.jpg', 2, 4, 6, 1, 3)
INSERT [dbo].[Items] ([id_item], [name_item], [year], [condition], [authenticity], [purchase_price], [selling_price], [arrival_date], [image], [id_category], [id_material], [id_supplier], [id_status], [id_origin_country]) VALUES (3, N'Antique Book', 1850, N'Poor', N'Authentic', CAST(300.00 AS Decimal(10, 2)), CAST(500.00 AS Decimal(10, 2)), CAST(N'2024-01-20' AS Date), N'book.jpg', 3, 4, 1, 4, 1)
INSERT [dbo].[Items] ([id_item], [name_item], [year], [condition], [authenticity], [purchase_price], [selling_price], [arrival_date], [image], [id_category], [id_material], [id_supplier], [id_status], [id_origin_country]) VALUES (4, N'Retro Painting', 1920, N'Good', N'Verified', CAST(1200.00 AS Decimal(10, 2)), CAST(2000.00 AS Decimal(10, 2)), CAST(N'2024-03-03' AS Date), N'painting.jpg', 4, 9, 3, 1, 5)
INSERT [dbo].[Items] ([id_item], [name_item], [year], [condition], [authenticity], [purchase_price], [selling_price], [arrival_date], [image], [id_category], [id_material], [id_supplier], [id_status], [id_origin_country]) VALUES (5, N'Silver Spoon', 1900, N'Excellent', N'Verified', CAST(150.00 AS Decimal(10, 2)), CAST(300.00 AS Decimal(10, 2)), CAST(N'2024-02-10' AS Date), N'spoon.jpg', 6, 2, 5, 1, 2)
INSERT [dbo].[Items] ([id_item], [name_item], [year], [condition], [authenticity], [purchase_price], [selling_price], [arrival_date], [image], [id_category], [id_material], [id_supplier], [id_status], [id_origin_country]) VALUES (6, N'Wooden Clock', 1930, N'Good', N'Verified', CAST(400.00 AS Decimal(10, 2)), CAST(800.00 AS Decimal(10, 2)), CAST(N'2024-03-01' AS Date), N'clock.jpg', 6, 5, 4, 1, 7)
INSERT [dbo].[Items] ([id_item], [name_item], [year], [condition], [authenticity], [purchase_price], [selling_price], [arrival_date], [image], [id_category], [id_material], [id_supplier], [id_status], [id_origin_country]) VALUES (7, N'Roman Figurine', 1600, N'Fair', N'Verified', CAST(800.00 AS Decimal(10, 2)), CAST(1200.00 AS Decimal(10, 2)), CAST(N'2024-01-25' AS Date), N'figurine.jpg', 5, 3, 5, 1, 5)
INSERT [dbo].[Items] ([id_item], [name_item], [year], [condition], [authenticity], [purchase_price], [selling_price], [arrival_date], [image], [id_category], [id_material], [id_supplier], [id_status], [id_origin_country]) VALUES (8, N'Ceramic Vase', 1700, N'Excellent', N'Authentic', CAST(600.00 AS Decimal(10, 2)), CAST(1000.00 AS Decimal(10, 2)), CAST(N'2024-02-12' AS Date), N'vase.jpg', 5, 7, 5, 1, 6)
INSERT [dbo].[Items] ([id_item], [name_item], [year], [condition], [authenticity], [purchase_price], [selling_price], [arrival_date], [image], [id_category], [id_material], [id_supplier], [id_status], [id_origin_country]) VALUES (9, N'Ancient Lamp', 1800, N'Good', N'Authentic', CAST(450.00 AS Decimal(10, 2)), CAST(900.00 AS Decimal(10, 2)), CAST(N'2024-03-15' AS Date), N'lamp.jpg', 10, 6, 10, 1, 4)
INSERT [dbo].[Items] ([id_item], [name_item], [year], [condition], [authenticity], [purchase_price], [selling_price], [arrival_date], [image], [id_category], [id_material], [id_supplier], [id_status], [id_origin_country]) VALUES (10, N'French Jewelry', 1880, N'Excellent', N'Verified', CAST(2000.00 AS Decimal(10, 2)), CAST(3000.00 AS Decimal(10, 2)), CAST(N'2024-04-01' AS Date), N'jewelry.jpg', 7, 1, 9, 1, 2)
SET IDENTITY_INSERT [dbo].[Items] OFF
GO
SET IDENTITY_INSERT [dbo].[Materials] ON 

INSERT [dbo].[Materials] ([id_material], [name_material]) VALUES (1, N'Gold')
INSERT [dbo].[Materials] ([id_material], [name_material]) VALUES (2, N'Silver')
INSERT [dbo].[Materials] ([id_material], [name_material]) VALUES (3, N'Bronze')
INSERT [dbo].[Materials] ([id_material], [name_material]) VALUES (4, N'Paper')
INSERT [dbo].[Materials] ([id_material], [name_material]) VALUES (5, N'Wood')
INSERT [dbo].[Materials] ([id_material], [name_material]) VALUES (6, N'Glass')
INSERT [dbo].[Materials] ([id_material], [name_material]) VALUES (7, N'Ceramic')
INSERT [dbo].[Materials] ([id_material], [name_material]) VALUES (8, N'Marble')
INSERT [dbo].[Materials] ([id_material], [name_material]) VALUES (9, N'Canvas')
INSERT [dbo].[Materials] ([id_material], [name_material]) VALUES (10, N'Copper')
SET IDENTITY_INSERT [dbo].[Materials] OFF
GO
SET IDENTITY_INSERT [dbo].[Origin_countries] ON 

INSERT [dbo].[Origin_countries] ([id_origin_country], [name_country]) VALUES (1, N'Russia')
INSERT [dbo].[Origin_countries] ([id_origin_country], [name_country]) VALUES (2, N'France')
INSERT [dbo].[Origin_countries] ([id_origin_country], [name_country]) VALUES (3, N'Germany')
INSERT [dbo].[Origin_countries] ([id_origin_country], [name_country]) VALUES (4, N'USA')
INSERT [dbo].[Origin_countries] ([id_origin_country], [name_country]) VALUES (5, N'Italy')
INSERT [dbo].[Origin_countries] ([id_origin_country], [name_country]) VALUES (6, N'China')
INSERT [dbo].[Origin_countries] ([id_origin_country], [name_country]) VALUES (7, N'UK')
INSERT [dbo].[Origin_countries] ([id_origin_country], [name_country]) VALUES (8, N'Japan')
INSERT [dbo].[Origin_countries] ([id_origin_country], [name_country]) VALUES (9, N'India')
INSERT [dbo].[Origin_countries] ([id_origin_country], [name_country]) VALUES (10, N'Spain')
SET IDENTITY_INSERT [dbo].[Origin_countries] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([id_role], [name_role]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([id_role], [name_role]) VALUES (2, N'Client')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([id_sale], [id_user], [id_item], [sale_date], [final_price]) VALUES (1, 2, 1, CAST(N'2024-04-01' AS Date), CAST(1500.00 AS Decimal(10, 2)))
INSERT [dbo].[Sales] ([id_sale], [id_user], [id_item], [sale_date], [final_price]) VALUES (2, 3, 2, CAST(N'2024-04-02' AS Date), CAST(350.00 AS Decimal(10, 2)))
INSERT [dbo].[Sales] ([id_sale], [id_user], [id_item], [sale_date], [final_price]) VALUES (3, 4, 4, CAST(N'2024-04-03' AS Date), CAST(2000.00 AS Decimal(10, 2)))
INSERT [dbo].[Sales] ([id_sale], [id_user], [id_item], [sale_date], [final_price]) VALUES (4, 5, 6, CAST(N'2024-04-04' AS Date), CAST(800.00 AS Decimal(10, 2)))
INSERT [dbo].[Sales] ([id_sale], [id_user], [id_item], [sale_date], [final_price]) VALUES (5, 6, 3, CAST(N'2024-04-05' AS Date), CAST(500.00 AS Decimal(10, 2)))
INSERT [dbo].[Sales] ([id_sale], [id_user], [id_item], [sale_date], [final_price]) VALUES (6, 7, 5, CAST(N'2024-04-06' AS Date), CAST(300.00 AS Decimal(10, 2)))
INSERT [dbo].[Sales] ([id_sale], [id_user], [id_item], [sale_date], [final_price]) VALUES (7, 8, 7, CAST(N'2024-04-07' AS Date), CAST(1200.00 AS Decimal(10, 2)))
INSERT [dbo].[Sales] ([id_sale], [id_user], [id_item], [sale_date], [final_price]) VALUES (8, 9, 9, CAST(N'2024-04-08' AS Date), CAST(900.00 AS Decimal(10, 2)))
INSERT [dbo].[Sales] ([id_sale], [id_user], [id_item], [sale_date], [final_price]) VALUES (9, 10, 8, CAST(N'2024-04-09' AS Date), CAST(1000.00 AS Decimal(10, 2)))
INSERT [dbo].[Sales] ([id_sale], [id_user], [id_item], [sale_date], [final_price]) VALUES (10, 2, 10, CAST(N'2024-04-10' AS Date), CAST(3000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Sales] OFF
GO
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([id_status], [name_status]) VALUES (1, N'Available')
INSERT [dbo].[Statuses] ([id_status], [name_status]) VALUES (2, N'Reserved')
INSERT [dbo].[Statuses] ([id_status], [name_status]) VALUES (3, N'Sold')
INSERT [dbo].[Statuses] ([id_status], [name_status]) VALUES (4, N'Restoration')
INSERT [dbo].[Statuses] ([id_status], [name_status]) VALUES (5, N'Archived')
INSERT [dbo].[Statuses] ([id_status], [name_status]) VALUES (6, N'On hold')
INSERT [dbo].[Statuses] ([id_status], [name_status]) VALUES (7, N'Pending approval')
INSERT [dbo].[Statuses] ([id_status], [name_status]) VALUES (8, N'Published')
INSERT [dbo].[Statuses] ([id_status], [name_status]) VALUES (9, N'Draft')
INSERT [dbo].[Statuses] ([id_status], [name_status]) VALUES (10, N'Unavailable')
SET IDENTITY_INSERT [dbo].[Statuses] OFF
GO
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([id_supplier], [name_supplier], [specialization], [authenticity]) VALUES (1, N'AntiqueRus', N'Books', N'High')
INSERT [dbo].[Suppliers] ([id_supplier], [name_supplier], [specialization], [authenticity]) VALUES (2, N'OldCoinz', N'Coins', N'High')
INSERT [dbo].[Suppliers] ([id_supplier], [name_supplier], [specialization], [authenticity]) VALUES (3, N'RetroArts', N'Paintings', N'Medium')
INSERT [dbo].[Suppliers] ([id_supplier], [name_supplier], [specialization], [authenticity]) VALUES (4, N'TimeKeepers', N'Clocks', N'High')
INSERT [dbo].[Suppliers] ([id_supplier], [name_supplier], [specialization], [authenticity]) VALUES (5, N'VintageWorld', N'Various', N'Medium')
INSERT [dbo].[Suppliers] ([id_supplier], [name_supplier], [specialization], [authenticity]) VALUES (6, N'Collectibles Inc.', N'Stamps', N'High')
INSERT [dbo].[Suppliers] ([id_supplier], [name_supplier], [specialization], [authenticity]) VALUES (7, N'HistoricTrade', N'Weapons', N'Low')
INSERT [dbo].[Suppliers] ([id_supplier], [name_supplier], [specialization], [authenticity]) VALUES (8, N'ClassicDesigns', N'Furniture', N'High')
INSERT [dbo].[Suppliers] ([id_supplier], [name_supplier], [specialization], [authenticity]) VALUES (9, N'ElegantPast', N'Jewelry', N'Medium')
INSERT [dbo].[Suppliers] ([id_supplier], [name_supplier], [specialization], [authenticity]) VALUES (10, N'AntiqueGlobal', N'Various', N'High')
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([id_user], [login], [password], [first_name], [middle_name], [last_name], [email], [preferences], [id_role]) VALUES (1, N'admin1', N'pass123', N'Ivan', N'Petrovich', N'Sidorov', N'ivan@mail.com', N'coins', 1)
INSERT [dbo].[Users] ([id_user], [login], [password], [first_name], [middle_name], [last_name], [email], [preferences], [id_role]) VALUES (2, N'user1', N'pass456', N'Anna', NULL, N'Kuznetsova', N'anna@mail.com', N'stamps', 2)
INSERT [dbo].[Users] ([id_user], [login], [password], [first_name], [middle_name], [last_name], [email], [preferences], [id_role]) VALUES (3, N'user2', N'pass789', N'Oleg', N'Ivanovich', N'Smirnov', N'oleg@mail.com', N'figurines', 2)
INSERT [dbo].[Users] ([id_user], [login], [password], [first_name], [middle_name], [last_name], [email], [preferences], [id_role]) VALUES (4, N'user3', N'123456', N'Maria', NULL, N'Fedorova', N'maria@mail.com', N'books', 2)
INSERT [dbo].[Users] ([id_user], [login], [password], [first_name], [middle_name], [last_name], [email], [preferences], [id_role]) VALUES (5, N'user4', N'abcdef', N'Dmitry', N'Sergeevich', N'Volkov', N'dmitry@mail.com', N'clocks', 2)
INSERT [dbo].[Users] ([id_user], [login], [password], [first_name], [middle_name], [last_name], [email], [preferences], [id_role]) VALUES (6, N'user5', N'qwerty', N'Elena', NULL, N'Ivanova', N'elena@mail.com', N'dishes', 2)
INSERT [dbo].[Users] ([id_user], [login], [password], [first_name], [middle_name], [last_name], [email], [preferences], [id_role]) VALUES (7, N'user6', N'zxcvbn', N'Alexey', N'Petrovich', N'Zuev', N'alexey@mail.com', N'lamps', 2)
INSERT [dbo].[Users] ([id_user], [login], [password], [first_name], [middle_name], [last_name], [email], [preferences], [id_role]) VALUES (8, N'user7', N'pass222', N'Nina', N'Grigoryevna', N'Lapina', N'nina@mail.com', N'books', 2)
INSERT [dbo].[Users] ([id_user], [login], [password], [first_name], [middle_name], [last_name], [email], [preferences], [id_role]) VALUES (9, N'user8', N'pass333', N'Igor', NULL, N'Orlov', N'igor@mail.com', N'coins', 2)
INSERT [dbo].[Users] ([id_user], [login], [password], [first_name], [middle_name], [last_name], [email], [preferences], [id_role]) VALUES (10, N'user9', N'pass444', N'Tatyana', NULL, N'Belova', N'tatyana@mail.com', N'paintings', 2)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__7838F27213B7FC4B]    Script Date: 19.05.2025 14:05:46 ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cart] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([id_item])
REFERENCES [dbo].[Items] ([id_item])
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([id_user])
REFERENCES [dbo].[Users] ([id_user])
GO
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD FOREIGN KEY([id_item])
REFERENCES [dbo].[Items] ([id_item])
GO
ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD FOREIGN KEY([id_user])
REFERENCES [dbo].[Users] ([id_user])
GO
ALTER TABLE [dbo].[Item_logs]  WITH CHECK ADD FOREIGN KEY([id_item])
REFERENCES [dbo].[Items] ([id_item])
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD FOREIGN KEY([id_category])
REFERENCES [dbo].[Categories] ([id_category])
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD FOREIGN KEY([id_material])
REFERENCES [dbo].[Materials] ([id_material])
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD FOREIGN KEY([id_origin_country])
REFERENCES [dbo].[Origin_countries] ([id_origin_country])
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD FOREIGN KEY([id_status])
REFERENCES [dbo].[Statuses] ([id_status])
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD FOREIGN KEY([id_supplier])
REFERENCES [dbo].[Suppliers] ([id_supplier])
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD FOREIGN KEY([id_item])
REFERENCES [dbo].[Items] ([id_item])
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD FOREIGN KEY([id_user])
REFERENCES [dbo].[Users] ([id_user])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([id_role])
REFERENCES [dbo].[Role] ([id_role])
GO
USE [master]
GO
ALTER DATABASE [AntiqueShop] SET  READ_WRITE 
GO
