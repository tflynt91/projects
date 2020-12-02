USE  GuildCars
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Sales')
	DROP TABLE Sales
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Vehicles')
	DROP TABLE Vehicles
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='NeworUsedTypes')
	DROP TABLE NeworUsedTypes
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ExteriorColors')
	DROP TABLE ExteriorColors
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='InteriorColors')
	DROP TABLE InteriorColors
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='BodyStyles')
	DROP TABLE BodyStyles
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='TransmissionTypes')
	DROP TABLE TransmissionTypes
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Contacts')
	DROP TABLE Contacts
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='States')
	DROP TABLE States
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='ModelTypes')
	DROP TABLE ModelTypes
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='MakeTypes')
	DROP TABLE MakeTypes
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Specials')
	DROP TABLE Specials
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='PurchaseTypes')
	DROP TABLE PurchaseTypes
GO

CREATE TABLE PurchaseTypes (
	PurchaseTypeId int identity(1,1) not null primary key,
	PurchaseType varchar(15) not null
)

CREATE TABLE Specials (
	SpecialId int identity(1,1) not null primary key,
	Title nvarchar(30) not null,
	UserId nvarchar(128) not null,
	SpecialDescription nvarchar(500) not null
)

CREATE TABLE MakeTypes (
	MakeTypeId int identity(1,1) not null primary key,
	MakeType nvarchar(30) not null,
	DateAdded datetime2 not null default(getdate()),
	UserId nvarchar(128) not null
)

CREATE TABLE ModelTypes (
	ModelTypeId int identity(1,1) not null primary key,
	MakeTypeId int not null foreign key references MakeTypes(MakeTypeId),	
	ModelType nvarchar(50) not null,
	DateAdded datetime2 not null default(getdate()),
	UserId nvarchar(128) not null
)

CREATE TABLE States (
	StateId char(2) not null primary key,
	StateName varchar(20) not null
)

CREATE TABLE Contacts (
	ContactId int identity(1,1) not null primary key,
	Name nvarchar(60) not null,
	Email nvarchar(30) null,
	Phone nvarchar(24) null,
	[Message] nvarchar(500) not null
)

CREATE TABLE TransmissionTypes (
	TransmissionTypeId int identity(1,1) not null primary key,
	TransmissionType nvarchar(30) not null
)

CREATE TABLE BodyStyles (
	BodyStyleId int identity(1,1) not null primary key,
	BodyStyle nvarchar(40) not null
)

CREATE TABLE InteriorColors (
	InteriorColorId int identity(1,1) not null primary key,
	InteriorColor nvarchar(15) not null
)

CREATE TABLE ExteriorColors (
	ExteriorColorId int identity(1,1) not null primary key,
	ExteriorColor nvarchar(15) not null
)

CREATE TABLE NewOrUsedTypes (
	NewOrUsedTypeId int identity(1,1) not null primary key,
	NeworUsedType varchar(4) not null
)

CREATE TABLE Vehicles (
	VinNumber varchar(17) not null primary key,
	ModelTypeId int not null foreign key references ModelTypes(ModelTypeId),
	BodyStyleId int null foreign key references BodyStyles(BodyStyleId),
	InteriorColorId int null foreign key references InteriorColors(InteriorColorId),
	ExteriorColorId int null foreign key references ExteriorColors(ExteriorColorId),
	TransmissionTypeId int null foreign key references TransmissionTypes(TransmissionTypeId),
	NeworUsedTypeId int not null foreign key references NewOrUsedTypes(NewOrUsedTypeId),
	ImageFileName varchar(50) not null,
	MSRP decimal(8,2) null,
	Mileage int null,
	SalePrice decimal(8,2) null,
	[Year] int null,
	VehicleDescription nvarchar(500) not null,
	Sold bit not null,
	Feautured bit not null
)

CREATE TABLE Sales (
	SalesId int identity(1,1) not null primary key,
	UserId nvarchar(128) not null,
	VinNumber varchar(17) not null foreign key references Vehicles(VinNumber),
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
	PurchaseTypeId int not null foreign key references PurchaseTypes(PurchaseTypeId)
)











