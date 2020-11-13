USE master
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name='DvdLibrary')
DROP DATABASE DvdLibrary
GO

CREATE DATABASE DvdLibrary
GO

USE DvdLibrary
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Dvds')
	DROP TABLE Dvds
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='Ratings')
	DROP TABLE Ratings
GO

CREATE TABLE Ratings (
	RatingId int identity(1,1) not null primary key,
	Rating char(5) not null
	)

CREATE TABLE Dvds (
	DvdId int identity(1,1) not null primary key,
	DvdTitle nvarchar(50) not null,
	ReleaseYear varchar(4) not null,
	Director nvarchar(50) not null,
	RatingId int not null foreign key references Ratings(RatingId),
	Notes nvarchar(500) null

)


