USE [master]
GO
/****** Object:  Database [HotelReservationSystem]    Script Date: 10/30/2020 3:12:12 PM ******/
CREATE DATABASE [HotelReservationSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HotelReservationSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\HotelReservationSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HotelReservationSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\HotelReservationSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [HotelReservationSystem] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HotelReservationSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HotelReservationSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HotelReservationSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HotelReservationSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HotelReservationSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HotelReservationSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [HotelReservationSystem] SET  MULTI_USER 
GO
ALTER DATABASE [HotelReservationSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HotelReservationSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HotelReservationSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HotelReservationSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HotelReservationSystem] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'HotelReservationSystem', N'ON'
GO
ALTER DATABASE [HotelReservationSystem] SET QUERY_STORE = OFF
GO
USE [HotelReservationSystem]
GO
/****** Object:  Table [dbo].[RoomAmenity]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomAmenity](
	[RoomAmenityID] [int] IDENTITY(1,1) NOT NULL,
	[AmenityType] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomAmenityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomNumberAmenityBridgeTable]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomNumberAmenityBridgeTable](
	[RoomNumberID] [int] NOT NULL,
	[RoomAmenityID] [int] NOT NULL,
 CONSTRAINT [Pk_RoomNumnberAmenityBridgeTable] PRIMARY KEY CLUSTERED 
(
	[RoomNumberID] ASC,
	[RoomAmenityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[RoomAmenities]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[RoomAmenities] AS
SELECT n.RoomNumberID, a.AmenityType
FROM RoomNumberAmenityBridgeTable n
		INNER JOIN RoomAmenity a ON
   n.RoomAmenityID = a.RoomAmenityID
GO
/****** Object:  Table [dbo].[RoomRate]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomRate](
	[RoomRateID] [int] IDENTITY(1,1) NOT NULL,
	[RoomTypeID] [int] NOT NULL,
	[RatePerDay] [real] NOT NULL,
	[FromDate] [date] NOT NULL,
	[ToDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomRateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomNumber]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomNumber](
	[RoomNumberID] [int] IDENTITY(100,1) NOT NULL,
	[Floor] [smallint] NOT NULL,
	[OccupancyLimit] [smallint] NOT NULL,
	[RoomTypeID] [int] NOT NULL,
	[RoomRateID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomNumberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[ReservationID] [int] IDENTITY(1,1) NOT NULL,
	[PromoFlatRateID] [int] NULL,
	[PromoPercentDiscountID] [int] NULL,
	[CreditCardNumber] [varchar](30) NULL,
	[CustomerGuestID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReservationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservationDetails]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationDetails](
	[ReservationID] [int] NOT NULL,
	[RoomNumberID] [int] NOT NULL,
	[FromDate] [date] NOT NULL,
	[ToDate] [date] NOT NULL,
 CONSTRAINT [Pk_ReservationDetails] PRIMARY KEY CLUSTERED 
(
	[ReservationID] ASC,
	[RoomNumberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TotalBeforePromo]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[TotalBeforePromo] AS
SELECT r.ReservationID, DATEDIFF(day, rd.FromDate, rd.ToDate) AS LengthofStayinDays, (SELECT rr.RatePerDay * DATEDIFF(day, rd.FromDate, rd.ToDate)) AS TotalBeforePromo
FROM Reservation r
	INNER JOIN ReservationDetails rd ON r.ReservationID = rd.ReservationID
	INNER JOIN RoomNumber rn ON rd.RoomNumberID = rn.RoomNumberID
	INNER JOIN RoomRate rr ON rn.RoomRateID = rr.RoomRateID
GO
/****** Object:  Table [dbo].[AddOn]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddOn](
	[AddOnID] [int] IDENTITY(1,1) NOT NULL,
	[AddOnDescription] [nvarchar](75) NULL,
	[AddOnFee] [real] NOT NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[AddOnID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Billing]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Billing](
	[BillingID] [int] IDENTITY(1,1) NOT NULL,
	[ReservationID] [int] NULL,
	[Tax] [real] NULL,
	[Total] [real] NULL,
	[Subtotal] [real] NULL,
PRIMARY KEY CLUSTERED 
(
	[BillingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillingDetails]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillingDetails](
	[BillingID] [int] NOT NULL,
	[AddOnID] [int] NOT NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [Pk_BillingDetails] PRIMARY KEY CLUSTERED 
(
	[BillingID] ASC,
	[AddOnID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AddOnTotals]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[AddOnTotals] AS
SELECT b.ReservationID,
SUM(ao.AddOnFee) AS AddOnFeeTotal
FROM Billing b
	INNER JOIN BillingDetails bd ON b.BillingID = bd.BillingID
	INNER JOIN AddOn ao	ON bd.AddOnID = ao.AddOnID
GROUP BY b.ReservationID
GO
/****** Object:  Table [dbo].[RoomType]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomType](
	[RoomTypeID] [int] IDENTITY(1,1) NOT NULL,
	[RoomTypeName] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[HotelRoomReservationDetails]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[HotelRoomReservationDetails] AS
SELECT r.ReservationID, rd.RoomNumberID, r.CustomerID, c.FirstName + c.LastName AS CustomerName, c.PhoneNumber, c.Email, rd.FromDate, rd.ToDate, hr.[Floor], hr.OccupancyLimit, rt.RoomTypeName
FROM Reservation r
	INNER JOIN Customer c ON r.CustomerID = c.CustomerID
	INNER JOIN ReservationDetails rd ON r.ReservationID = rd.ReservationID
	INNER JOIN RoomNumber hr ON rd.RoomNumberID = hr.RoomNumberID
	INNER JOIN RoomType rt ON hr.RoomTypeID = rt.RoomTypeID
GROUP BY r.ReservationID, rd.RoomNumberID, r.CustomerID, c.FirstName + c.LastName, c.PhoneNumber, c.Email, rd.FromDate, rd.ToDate, hr.[Floor], hr.OccupancyLimit, rt.RoomTypeName
GO
/****** Object:  Table [dbo].[PromoPercentDiscount]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PromoPercentDiscount](
	[PromoPercentDiscountID] [int] IDENTITY(1,1) NOT NULL,
	[PromoDescription] [nvarchar](100) NOT NULL,
	[PercentRateAmount] [real] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[PromoPercentDiscountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TotalBeforeAndAfterPromoPercentDiscount]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[TotalBeforeAndAfterPromoPercentDiscount] AS
SELECT r.ReservationID, rd.RoomNumberID, DATEDIFF(day, rd.FromDate, rd.ToDate) AS LengthofStayinDays,
             (SELECT SUM(rr.RatePerDay * DATEDIFF(day, rd.FromDate, rd.ToDate)) AS Expr1) AS TotalBeforePromo,
             (SELECT SUM(((rr.RatePerDay * DATEDIFF(day, rd.FromDate, rd.ToDate)) * p.PercentRateAmount) + (rr.RatePerDay * DATEDIFF(day, rd.FromDate, rd.ToDate)))) AS TotalAfterPromo
FROM  dbo.Reservation AS r INNER JOIN
         dbo.ReservationDetails AS rd ON r.ReservationID = rd.ReservationID INNER JOIN
         dbo.RoomNumber AS rn ON rd.RoomNumberID = rn.RoomNumberID INNER JOIN
         dbo.RoomRate AS rr ON rn.RoomRateID = rr.RoomRateID INNER JOIN
         dbo.PromoPercentDiscount AS p ON r.PromoPercentDiscountID = p.PromoPercentDiscountID
GROUP BY r.ReservationID, rd.RoomNumberID, DATEDIFF(day, rd.FromDate, rd.ToDate)
GO
/****** Object:  View [dbo].[SubtotalAndTotalWithPromoPercentDiscount]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[SubtotalAndTotalWithPromoPercentDiscount] AS
SELECT b.BillingID, b.ReservationID,
             (SELECT SUM(aot.AddOnFeeTotal + tp.TotalAfterPromo) AS Expr1) AS Subtotal,
             (SELECT SUM((aot.AddOnFeeTotal + tp.TotalAfterPromo) * b.Tax + (aot.AddOnFeeTotal + tp.TotalAfterPromo)) AS Expr1) AS Total
FROM  dbo.Billing AS b INNER JOIN
         dbo.AddOnTotals AS aot ON b.ReservationID = aot.ReservationID INNER JOIN
         dbo.TotalBeforeAndAfterPromoPercentDiscount AS tp ON b.ReservationID = tp.ReservationID
GROUP BY b.BillingID, b.ReservationID
GO
/****** Object:  Table [dbo].[PromoFlatRate]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PromoFlatRate](
	[PromoFlatRateID] [int] IDENTITY(1,1) NOT NULL,
	[PromoDescription] [nvarchar](100) NOT NULL,
	[FlatRateAmount] [real] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[PromoFlatRateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TotalBeforeAndAfterPromoFlatRate]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[TotalBeforeAndAfterPromoFlatRate] AS
SELECT r.ReservationID, rd.RoomNumberID, DATEDIFF(day, rd.FromDate, rd.ToDate) AS LengthofStayinDays,
             (SELECT SUM(rr.RatePerDay * DATEDIFF(day, rd.FromDate, rd.ToDate)) AS Expr1) AS TotalBeforePromo,
             (SELECT SUM(rr.RatePerDay * DATEDIFF(day, rd.FromDate, rd.ToDate) + p.FlatRateAmount) AS Expr1) AS TotalAfterPromo
FROM  dbo.Reservation AS r INNER JOIN
         dbo.ReservationDetails AS rd ON r.ReservationID = rd.ReservationID INNER JOIN
         dbo.RoomNumber AS rn ON rd.RoomNumberID = rn.RoomNumberID INNER JOIN
         dbo.RoomRate AS rr ON rn.RoomRateID = rr.RoomRateID INNER JOIN
         dbo.PromoFlatRate AS p ON r.PromoFlatRateID = p.PromoFlatRateID
GROUP BY r.ReservationID, rd.RoomNumberID, DATEDIFF(day, rd.FromDate, rd.ToDate)

GO
/****** Object:  View [dbo].[SubtotalAndTotalWithFlatRatePromo]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[SubtotalAndTotalWithFlatRatePromo] AS
SELECT b.BillingID, b.ReservationID,
             (SELECT SUM(aot.AddOnFeeTotal + tp.TotalAfterPromo) AS Expr1) AS Subtotal,
             (SELECT SUM((aot.AddOnFeeTotal + tp.TotalAfterPromo) * b.Tax + (aot.AddOnFeeTotal + tp.TotalAfterPromo)) AS Expr1) AS Total
FROM  dbo.Billing AS b INNER JOIN
         dbo.AddOnTotals AS aot ON b.ReservationID = aot.ReservationID INNER JOIN
         dbo.TotalBeforeAndAfterPromoFlatRate AS tp ON b.ReservationID = tp.ReservationID
GROUP BY	b.BillingID, b.ReservationID
GO
/****** Object:  View [dbo].[SubtotalAndTotalWithoutPromo]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[SubtotalAndTotalWithoutPromo] AS
SELECT b.BillingID, b.ReservationID,
             (SELECT SUM(aot.AddOnFeeTotal + tbp.TotalBeforePromo) AS Expr1) AS Subtotal,
             (SELECT SUM((aot.AddOnFeeTotal + tbp.TotalBeforePromo) * b.Tax + (aot.AddOnFeeTotal + tbp.TotalBeforePromo)) AS Expr1) AS Total
FROM  dbo.Billing AS b INNER JOIN
         dbo.AddOnTotals AS aot ON b.ReservationID = aot.ReservationID INNER JOIN
         dbo.TotalBeforePromo AS tbp ON b.ReservationID = tbp.ReservationID
GROUP BY b.BillingID, b.ReservationID

GO
/****** Object:  View [dbo].[AddOnDetails]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[AddOnDetails] AS
SELECT b.ReservationID, bd.BillingID, bd.AddOnID, ao.AddOnDescription, ao.AddOnFee * bd.Quantity AS FeeTotal
FROM Billing b
	INNER JOIN BillingDetails bd ON b.BillingID = bd.BillingID
	INNER JOIN AddOn ao ON bd.AddOnID = ao.AddOnID
GROUP BY b.ReservationID, bd.BillingID, bd.AddOnID, ao.AddOnDescription, ao.AddOnFee * bd.Quantity
GO
/****** Object:  Table [dbo].[Guest]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guest](
	[GuestID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Age] [smallint] NULL,
	[PhoneNumber] [nvarchar](24) NULL,
	[Email] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[GuestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GuestReservationBridgeTable]    Script Date: 10/30/2020 3:12:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GuestReservationBridgeTable](
	[GuestID] [int] NOT NULL,
	[ReservationID] [int] NOT NULL,
 CONSTRAINT [pk_GuestReservation] PRIMARY KEY CLUSTERED 
(
	[GuestID] ASC,
	[ReservationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AddOn] ON 

INSERT [dbo].[AddOn] ([AddOnID], [AddOnDescription], [AddOnFee], [FromDate], [ToDate]) VALUES (1, N'Movie', 5, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[AddOn] ([AddOnID], [AddOnDescription], [AddOnFee], [FromDate], [ToDate]) VALUES (2, N'Breakfast Room Service', 10, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[AddOn] ([AddOnID], [AddOnDescription], [AddOnFee], [FromDate], [ToDate]) VALUES (3, N'Lunch Room Service', 15, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[AddOn] ([AddOnID], [AddOnDescription], [AddOnFee], [FromDate], [ToDate]) VALUES (4, N'Dinner Room Service', 20, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[AddOn] ([AddOnID], [AddOnDescription], [AddOnFee], [FromDate], [ToDate]) VALUES (5, N'Robe', 15, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[AddOn] ([AddOnID], [AddOnDescription], [AddOnFee], [FromDate], [ToDate]) VALUES (6, N'Newspaper Delivery', 2, CAST(N'2020-01-01' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[AddOn] OFF
GO
SET IDENTITY_INSERT [dbo].[PromoFlatRate] ON 

INSERT [dbo].[PromoFlatRate] ([PromoFlatRateID], [PromoDescription], [FlatRateAmount], [StartDate], [EndDate]) VALUES (1, N'$50 off Promo', -50, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[PromoFlatRate] ([PromoFlatRateID], [PromoDescription], [FlatRateAmount], [StartDate], [EndDate]) VALUES (2, N'$20 off Promo', -20, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[PromoFlatRate] ([PromoFlatRateID], [PromoDescription], [FlatRateAmount], [StartDate], [EndDate]) VALUES (3, N'$30 off Promo', -30, CAST(N'2020-01-01' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[PromoFlatRate] OFF
GO
SET IDENTITY_INSERT [dbo].[PromoPercentDiscount] ON 

INSERT [dbo].[PromoPercentDiscount] ([PromoPercentDiscountID], [PromoDescription], [PercentRateAmount], [StartDate], [EndDate]) VALUES (1, N'10% off', 0.1, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[PromoPercentDiscount] ([PromoPercentDiscountID], [PromoDescription], [PercentRateAmount], [StartDate], [EndDate]) VALUES (2, N'15% off', 0.15, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[PromoPercentDiscount] ([PromoPercentDiscountID], [PromoDescription], [PercentRateAmount], [StartDate], [EndDate]) VALUES (3, N'20% off', 0.2, CAST(N'2020-01-01' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[PromoPercentDiscount] OFF
GO
SET IDENTITY_INSERT [dbo].[RoomAmenity] ON 

INSERT [dbo].[RoomAmenity] ([RoomAmenityID], [AmenityType]) VALUES (1, N'WiFi')
INSERT [dbo].[RoomAmenity] ([RoomAmenityID], [AmenityType]) VALUES (2, N'Fridge')
INSERT [dbo].[RoomAmenity] ([RoomAmenityID], [AmenityType]) VALUES (3, N'TV')
INSERT [dbo].[RoomAmenity] ([RoomAmenityID], [AmenityType]) VALUES (4, N'Microwave')
INSERT [dbo].[RoomAmenity] ([RoomAmenityID], [AmenityType]) VALUES (5, N'Washer/Driver')
INSERT [dbo].[RoomAmenity] ([RoomAmenityID], [AmenityType]) VALUES (6, N'Hot Tub')
INSERT [dbo].[RoomAmenity] ([RoomAmenityID], [AmenityType]) VALUES (7, N'DVD Player')
INSERT [dbo].[RoomAmenity] ([RoomAmenityID], [AmenityType]) VALUES (8, N'Swimming Pool Access')
INSERT [dbo].[RoomAmenity] ([RoomAmenityID], [AmenityType]) VALUES (9, N'Deck')
INSERT [dbo].[RoomAmenity] ([RoomAmenityID], [AmenityType]) VALUES (10, N'Pool Table')
INSERT [dbo].[RoomAmenity] ([RoomAmenityID], [AmenityType]) VALUES (11, N'Premium Cable(HBO)')
SET IDENTITY_INSERT [dbo].[RoomAmenity] OFF
GO
SET IDENTITY_INSERT [dbo].[RoomNumber] ON 

INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (100, 1, 20, 1, 1)
INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (101, 1, 20, 1, 1)
INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (102, 1, 40, 2, 2)
INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (103, 1, 40, 3, 3)
INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (104, 1, 40, 7, 7)
INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (105, 2, 20, 1, 1)
INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (106, 2, 20, 3, 3)
INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (107, 2, 40, 4, 4)
INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (108, 2, 25, 5, 5)
INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (109, 2, 40, 6, 6)
INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (110, 3, 40, 7, 7)
INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (111, 3, 40, 8, 8)
INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (112, 3, 20, 1, 1)
INSERT [dbo].[RoomNumber] ([RoomNumberID], [Floor], [OccupancyLimit], [RoomTypeID], [RoomRateID]) VALUES (113, 3, 20, 2, 2)
SET IDENTITY_INSERT [dbo].[RoomNumber] OFF
GO
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (100, 1)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (100, 3)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (101, 1)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (101, 3)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (102, 1)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (102, 3)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (103, 1)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (103, 2)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (103, 3)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (104, 1)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (104, 2)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (104, 3)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (104, 4)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (104, 5)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (104, 6)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (104, 7)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (104, 8)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (104, 9)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (104, 11)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (105, 1)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (105, 3)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (106, 1)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (107, 3)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (108, 1)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (108, 3)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (108, 5)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (108, 10)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (108, 11)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (109, 1)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (109, 2)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (109, 3)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (109, 4)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (109, 5)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (109, 8)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (109, 9)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (110, 1)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (110, 2)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (110, 3)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (110, 4)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (110, 5)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (110, 6)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (110, 7)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (110, 8)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (110, 9)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (110, 11)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (111, 1)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (111, 2)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (111, 3)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (111, 4)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (111, 5)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (111, 6)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (111, 7)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (111, 8)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (111, 9)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (111, 10)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (111, 11)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (112, 1)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (112, 3)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (113, 1)
INSERT [dbo].[RoomNumberAmenityBridgeTable] ([RoomNumberID], [RoomAmenityID]) VALUES (113, 3)
GO
SET IDENTITY_INSERT [dbo].[RoomRate] ON 

INSERT [dbo].[RoomRate] ([RoomRateID], [RoomTypeID], [RatePerDay], [FromDate], [ToDate]) VALUES (1, 1, 75, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[RoomRate] ([RoomRateID], [RoomTypeID], [RatePerDay], [FromDate], [ToDate]) VALUES (2, 2, 100, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[RoomRate] ([RoomRateID], [RoomTypeID], [RatePerDay], [FromDate], [ToDate]) VALUES (3, 3, 100, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[RoomRate] ([RoomRateID], [RoomTypeID], [RatePerDay], [FromDate], [ToDate]) VALUES (4, 4, 150, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[RoomRate] ([RoomRateID], [RoomTypeID], [RatePerDay], [FromDate], [ToDate]) VALUES (5, 5, 200, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[RoomRate] ([RoomRateID], [RoomTypeID], [RatePerDay], [FromDate], [ToDate]) VALUES (6, 6, 250, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[RoomRate] ([RoomRateID], [RoomTypeID], [RatePerDay], [FromDate], [ToDate]) VALUES (7, 7, 300, CAST(N'2020-01-01' AS Date), NULL)
INSERT [dbo].[RoomRate] ([RoomRateID], [RoomTypeID], [RatePerDay], [FromDate], [ToDate]) VALUES (8, 8, 350, CAST(N'2020-01-01' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[RoomRate] OFF
GO
SET IDENTITY_INSERT [dbo].[RoomType] ON 

INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName]) VALUES (1, N'Single')
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName]) VALUES (2, N'Double')
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName]) VALUES (3, N'Twin')
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName]) VALUES (4, N'Interconnected')
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName]) VALUES (5, N'Triple')
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName]) VALUES (6, N'Quad')
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName]) VALUES (7, N'Double-Double')
INSERT [dbo].[RoomType] ([RoomTypeID], [RoomTypeName]) VALUES (8, N'Suite')
SET IDENTITY_INSERT [dbo].[RoomType] OFF
GO
ALTER TABLE [dbo].[BillingDetails] ADD  CONSTRAINT [quantity_default]  DEFAULT ((1)) FOR [Quantity]
GO
ALTER TABLE [dbo].[PromoFlatRate] ADD  DEFAULT ((0)) FOR [FlatRateAmount]
GO
ALTER TABLE [dbo].[PromoPercentDiscount] ADD  DEFAULT ((1)) FOR [PercentRateAmount]
GO
ALTER TABLE [dbo].[Billing]  WITH CHECK ADD FOREIGN KEY([ReservationID])
REFERENCES [dbo].[Reservation] ([ReservationID])
GO
ALTER TABLE [dbo].[BillingDetails]  WITH CHECK ADD FOREIGN KEY([AddOnID])
REFERENCES [dbo].[AddOn] ([AddOnID])
GO
ALTER TABLE [dbo].[BillingDetails]  WITH CHECK ADD FOREIGN KEY([BillingID])
REFERENCES [dbo].[Billing] ([BillingID])
GO
ALTER TABLE [dbo].[GuestReservationBridgeTable]  WITH CHECK ADD FOREIGN KEY([GuestID])
REFERENCES [dbo].[Guest] ([GuestID])
GO
ALTER TABLE [dbo].[GuestReservationBridgeTable]  WITH CHECK ADD FOREIGN KEY([ReservationID])
REFERENCES [dbo].[Reservation] ([ReservationID])
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD FOREIGN KEY([CustomerGuestID])
REFERENCES [dbo].[Guest] ([GuestID])
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD FOREIGN KEY([PromoFlatRateID])
REFERENCES [dbo].[PromoFlatRate] ([PromoFlatRateID])
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD FOREIGN KEY([PromoPercentDiscountID])
REFERENCES [dbo].[PromoPercentDiscount] ([PromoPercentDiscountID])
GO
ALTER TABLE [dbo].[ReservationDetails]  WITH CHECK ADD FOREIGN KEY([ReservationID])
REFERENCES [dbo].[Reservation] ([ReservationID])
GO
ALTER TABLE [dbo].[ReservationDetails]  WITH CHECK ADD FOREIGN KEY([RoomNumberID])
REFERENCES [dbo].[RoomNumber] ([RoomNumberID])
GO
ALTER TABLE [dbo].[RoomNumber]  WITH CHECK ADD FOREIGN KEY([RoomRateID])
REFERENCES [dbo].[RoomRate] ([RoomRateID])
GO
ALTER TABLE [dbo].[RoomNumber]  WITH CHECK ADD FOREIGN KEY([RoomTypeID])
REFERENCES [dbo].[RoomType] ([RoomTypeID])
GO
ALTER TABLE [dbo].[RoomNumberAmenityBridgeTable]  WITH CHECK ADD FOREIGN KEY([RoomAmenityID])
REFERENCES [dbo].[RoomAmenity] ([RoomAmenityID])
GO
ALTER TABLE [dbo].[RoomNumberAmenityBridgeTable]  WITH CHECK ADD FOREIGN KEY([RoomNumberID])
REFERENCES [dbo].[RoomNumber] ([RoomNumberID])
GO
ALTER TABLE [dbo].[RoomRate]  WITH CHECK ADD FOREIGN KEY([RoomTypeID])
REFERENCES [dbo].[RoomType] ([RoomTypeID])
GO
USE [master]
GO
ALTER DATABASE [HotelReservationSystem] SET  READ_WRITE 
GO
