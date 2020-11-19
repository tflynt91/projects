USE  GuildCars
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Sales')
	DROP TABLE Sales
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Vehicle')
	DROP TABLE Vehicle
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='NeworUsedType')
	DROP TABLE NeworUsedType
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ExteriorColor')
	DROP TABLE ExteriorColor
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='InteriorColor')
	DROP TABLE InteriorColor
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BodyStyle')
	DROP TABLE BodyStyle
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='TransmissionType')
	DROP TABLE TransmissionType
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Contacts')
	DROP TABLE Contacts
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='States')
	DROP TABLE States
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ModelType')
	DROP TABLE ModelType
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='MakeType')
	DROP TABLE MakeType
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Specials')
	DROP TABLE Specials
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchaseType')
	DROP TABLE PurchaseType
GO

CREATE TABLE PurchaseType (
	PurchaseTypeId int identity(1,1) not null primary key,
	PurchaseType varchar(15) not null
)

CREATE TABLE Specials (
	SpecialId int identity(1,1) not null primary key,
	Title nvarchar(30) not null,
	UserId nvarchar(128) not null,
	SpecialDescription nvarchar(500) not null
)

CREATE TABLE MakeType (
	MakeTypeId int identity(1,1) not null primary key,
	MakeType nvarchar(30) not null,
	DateAdded datetime2 not null default(getdate()),
	UserId nvarchar(128) not null
)

CREATE TABLE ModelType (
	ModelTypeId int identity(1,1) not null primary key,
	MakeTypeId int not null foreign key references MakeType(MakeTypeId),	
	ModelType nvarchar(50) not null,
	DateAdded datetime2 not null default(getdate()),
	UserId nvarchar(128) not null
)

CREATE TABLE States (
	StateId char(2) not null primary key,
	StateName varchar(15) not null
)

CREATE TABLE Contacts (
	ContactId int identity(1,1) not null primary key,
	FirstName nvarchar(30) not null,
	LastName nvarchar(50) not null,
	Email nvarchar(30) null,
	Phone nvarchar(24) null,
	[Message] nvarchar(500) not null
)

CREATE TABLE TransmissionType (
	TransmissionTypeId int identity(1,1) not null primary key,
	TransmissionType nvarchar(30) not null
)

CREATE TABLE BodyStyle (
	BodyStyleId int identity(1,1) not null primary key,
	BodyStyle nvarchar(40) not null
)

CREATE TABLE InteriorColor (
	InteriorColorId int identity(1,1) not null primary key,
	InteriorColor nvarchar(15) not null
)

CREATE TABLE ExteriorColor (
	ExteriorColorId int identity(1,1) not null primary key,
	ExteriorColor nvarchar(15) not null
)

CREATE TABLE NewOrUsedType (
	NewOrUsedTypeId int identity(1,1) not null primary key,
	NeworUsedType varchar(4) not null
)

CREATE TABLE Vehicle (
	VinNumber int not null primary key,
	ModelTypeId int not null foreign key references ModelType(ModelTypeId),
	BodyStyleId int null foreign key references BodyStyle(BodyStyleId),
	InteriorColorId int null foreign key references InteriorColor(InteriorColorId),
	ExteriorColorId int null foreign key references ExteriorColor(ExteriorColorId),
	TransmissionTypeId int null foreign key references TransmissionType(TransmissionTypeId),
	NeworUsedTypeId int not null foreign key references NewOrUsedType(NewOrUsedTypeId),
	ImageFileName varchar(50) not null,
	MSRP decimal(8,2) null,
	Mileage int null,
	SalePrice decimal(8,2) null,
	[Year] datetime2 null,
	VehicleDescription nvarchar(500) not null,
	Sold bit not null
)

CREATE TABLE Sales (
	SalesId int identity(1,1) not null primary key,
	UserId nvarchar(128) not null,
	VinNumber int not null foreign key references Vehicle(VinNumber),
	PurchasePrice decimal(8,2) not null,
	PurchaseDate datetime2 not null,
	FirstName nvarchar(30) not null,
	LastName nvarchar(50) not null,
	Email nvarchar(30) null,
	Phone nvarchar(24) null,
	Street1 nvarchar(40) not null,
	Street2 nvarchar(40) null,
	City nvarchar(30) not null,
	StateId char(2) not null foreign key references States(StateId),
	ZipCode varchar(10) not null,
	PurchaseTypeId int not null foreign key references PurchaseType(PurchaseTypeId)
)











