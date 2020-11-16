USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='DvdLibrary')
DROP DATABASE DvdLibrary
GO

CREATE DATABASE DvdLibrary
GO

USE DvdLibrary
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='DvdItem')
	DROP TABLE DvdItem
GO

CREATE TABLE DvdItem (
	DvdId int identity(1,1) not null primary key,
	DvdTitle nvarchar(50) not null,
	ReleaseYear varchar(4) not null,
	Director nvarchar(50) not null,
	Rating char(5) not null,
	Notes nvarchar(500) null

)


