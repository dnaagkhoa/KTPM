create database [QL22]
go
USE [QL22]
GO
/****** Object:  Table [dbo].[ACCOUNT] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCOUNT](
	[USERNAME] [nvarchar](100) NOT NULL,
	[DISPLAYNAME] [nvarchar](100) NOT NULL,
	[PASSWORD] [nvarchar](1000) NOT NULL,
	[TYPEACCOUNT] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[USERNAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BILL] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BILL](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAMETABLE] [nvarchar](100) NOT NULL,
	[NAMEFOOD] [nvarchar](100) NOT NULL,
	[COUNTS] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CATEGORY]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CATEGORY](
	[NAME] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FOOD]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FOOD](
	[NAME] [nvarchar](100) NOT NULL,
	[NAMECATEGORY] [nvarchar](100) NOT NULL,
	[PRICE] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TABLEF]   ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLEF](
	[NAME] [nvarchar](100) NOT NULL,
	[STT] [nvarchar](10) NOT NULL,
	[TOTAL] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[ACCOUNT] ([USERNAME], [DISPLAYNAME], [PASSWORD], [TYPEACCOUNT])
VALUES 
    ('admin', N'Quản Lý 1', 'admin', 'ADMIN'),
    ('admin2', N'Quản Lý 2', 'admin', 'ADMIN'),
    ('kata', N'Thu ngân 2', 'kata', 'CASHIER'),
    ('user', N'Thu Ngân 1', 'fblc', 'CASHIER');

INSERT [dbo].[CATEGORY] ([NAME])
VALUES 
    (N'Cà Phê'),
    (N'Kem'),
    (N'Nước Ngọt'),
    (N'Sinh Tố'),
    
    (N'Các loại nước khác'),
    (N'Snacks và ăn vặt');

INSERT [dbo].[FOOD] ([NAME], [NAMECATEGORY], [PRICE])
VALUES 
    (N'Cà Phê Đen', N'Cà Phê', 25000),
    
    (N'Espresso', N'Cà Phê', 40000),
   


    (N'Trà Ổi Hồng', N'Trà', 20000),
    (N'Trà Lipton', N'Trà', 25000),
  

   
    (N'Kem Ly', N'Kem', 15000),
    (N'Kem Tươi', N'Kem', 30000),

    (N'Nước Chanh', N'Các loại nước khác', 9000),
    (N'Đá Me', N'Các loại nước khác', 8000),
    (N'Nước Chanh Dây', N'Các loại nước khác', 15000),
    (N'Nước Dừa', N'Các loại nước khác', 10000),

    (N'Pepsi', N'Nước Ngọt', 10000),
    (N'Sting', N'Nước Ngọt', 10000),
    (N'Bò húc', N'Nước Ngọt', 10000),
    (N'Coca', N'Nước Ngọt', 10000),
    (N'7 Up', N'Nước Ngọt', 10000),

    (N'Bánh Quy', N'Snacks và ăn vặt', 15000),
    (N'Snacks', N'Snacks và ăn vặt', 15000),
    (N'Bánh tráng trộn', N'Snacks và ăn vặt', 15000),

   
    (N'Sinh Tố Dâu', N'Sinh Tố', 4500),
    (N'Sinh Tố Mảng Cầu', N'Sinh Tố', 20500);

INSERT [dbo].[TABLEF] ([NAME], [STT], [TOTAL])
VALUES 
    (N'Bàn 01', N'TRONG', 0),
    (N'Bàn 02', N'TRONG', 0),
    (N'Bàn 03', N'TRONG', 0),
    (N'Bàn 04', N'TRONG', 0),
    (N'Bàn 05', N'TRONG', 0),
    (N'Bàn 06', N'TRONG', 0),
    (N'Bàn 07', N'TRONG', 0),
    (N'Bàn 08', N'TRONG', 0),
    (N'Bàn 09', N'TRONG', 0),
    (N'Bàn 10', N'TRONG', 0),
    (N'Bàn 11', N'TRONG', 0),
    (N'Bàn 12', N'TRONG', 0),
    (N'Bàn 13', N'TRONG', 0),
    (N'Bàn 14', N'TRONG', 0),
    (N'Bàn 15', N'TRONG', 0),
    (N'Bàn 16', N'TRONG', 0),
    (N'Bàn 17', N'TRONG', 0),
    (N'Bàn 18', N'TRONG', 0),
    (N'Bàn 19', N'TRONG', 0),
    (N'Bàn 20', N'TRONG', 0),
    (N'Bàn 21', N'TRONG', 0),
    (N'Bàn 22', N'TRONG', 0),
    (N'Bàn 23', N'TRONG', 0),
    (N'Bàn 24', N'TRONG', 0),
    (N'Bàn VIP 1', N'TRONG', 0),
    (N'Bàn VIP 2', N'TRONG', 0),
    (N'Bàn VIP 3', N'TRONG', 0),
    (N'Bàn VIP 4', N'TRONG', 0);

ALTER TABLE [dbo].[ACCOUNT] ADD  DEFAULT (N'NO NAME..') FOR [DISPLAYNAME]
GO
ALTER TABLE [dbo].[ACCOUNT] ADD  DEFAULT ('0000') FOR [PASSWORD]
GO
ALTER TABLE [dbo].[ACCOUNT] ADD  DEFAULT (N'CASHIER') FOR [TYPEACCOUNT]
GO
ALTER TABLE [dbo].[BILL] ADD  DEFAULT ((1)) FOR [COUNTS]
GO
ALTER TABLE [dbo].[FOOD] ADD  DEFAULT ((0)) FOR [PRICE]
GO
ALTER TABLE [dbo].[TABLEF] ADD  DEFAULT (N'TRONG') FOR [STT]
GO
ALTER TABLE [dbo].[TABLEF] ADD  DEFAULT ((0)) FOR [TOTAL]
GO
ALTER TABLE [dbo].[BILL]  WITH CHECK ADD FOREIGN KEY([NAMETABLE])
REFERENCES [dbo].[TABLEF] ([NAME])
GO
ALTER TABLE [dbo].[BILL]  WITH CHECK ADD FOREIGN KEY([NAMEFOOD])
REFERENCES [dbo].[FOOD] ([NAME])
GO
ALTER TABLE [dbo].[FOOD]  WITH CHECK ADD FOREIGN KEY([NAMECATEGORY])
REFERENCES [dbo].[CATEGORY] ([NAME])
Go
UPDATE [dbo].[FOOD]
SET [NAMECATEGORY] = N'Cà Phê'
WHERE [NAMECATEGORY] = N'Cà Phê';

UPDATE [dbo].[FOOD]
SET [NAMECATEGORY] = N'Trà'
WHERE [NAMECATEGORY] = N'Trà';





