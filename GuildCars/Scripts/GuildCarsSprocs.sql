IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'StatesSelectAll')
      DROP PROCEDURE StatesSelectAll
GO

CREATE PROCEDURE StatesSelectAll AS
BEGIN
	SELECT StateId, StateName
	FROM States
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ExteriorColorsSelectAll')
      DROP PROCEDURE ExteriorColorsSelectAll
GO

CREATE PROCEDURE ExteriorColorsSelectAll AS
BEGIN
	SELECT ExteriorColorId, ExteriorColor
	FROM ExteriorColors
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'InteriorColorsSelectAll')
      DROP PROCEDURE InteriorColorsSelectAll
GO

CREATE PROCEDURE InteriorColorsSelectAll AS
BEGIN
	SELECT InteriorColorId, InteriorColor
	FROM InteriorColors
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'BodyStylesSelectAll')
      DROP PROCEDURE BodyStylesSelectAll
GO

CREATE PROCEDURE BodyStylesSelectAll AS
BEGIN
	SELECT BodyStyleId, BodyStyle
	FROM BodyStyles
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakeTypesSelectAll')
      DROP PROCEDURE MakeTypesSelectAll
GO

CREATE PROCEDURE MakeTypesSelectAll AS
BEGIN
	SELECT MakeTypeId, MakeType
	FROM MakeTypes
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectModelTypesByMake')
      DROP PROCEDURE SelectModelTypesByMake
GO

CREATE PROCEDURE SelectModelTypesByMake (
	@MakeTypeId int
	) AS
BEGIN
	SELECT ModelTypeId, ModelType
	FROM ModelTypes
	WHERE MakeTypeId = @MakeTypeId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'NewOrUsedTypeOptionsSelectAll')
      DROP PROCEDURE NewOrUsedTypeOptionsSelectAll
GO

CREATE PROCEDURE NewOrUsedTypeOptionsSelectAll AS
BEGIN
	SELECT NewOrUsedTypeId, NeworUsedType
	FROM NewOrUsedTypes
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'PurchaseTypesSelectAll')
      DROP PROCEDURE PurchaseTypesSelectAll
GO

CREATE PROCEDURE PurchaseTypesSelectAll AS
BEGIN
	SELECT PurchaseTypeId, PurchaseType
	FROM PurchaseTypes
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'TransmissionTypesSelectAll')
      DROP PROCEDURE TransmissionTypesSelectAll
GO

CREATE PROCEDURE TransmissionTypesSelectAll AS
BEGIN
	SELECT TransmissionTypeId, TransmissionType
	FROM TransmissionTypes
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleInsert')
      DROP PROCEDURE VehicleInsert
GO

CREATE PROCEDURE VehicleInsert (
	@VinNumber varchar(17),
	@ModelTypeId int,
	@BodyStyleId int,
	@InteriorColorId int,
	@ExteriorColorId int,
	@TransmissionTypeId int,
	@NeworUsedTypeId int,
	@ImageFileName varchar(50),
	@MSRP decimal(8,2),
	@Mileage int,
	@SalePrice decimal(8,2),
	@Year int,
	@VehicleDescription nvarchar(500),
	@Sold bit,
	@Featured bit
	) AS
BEGIN
	INSERT INTO	Vehicles (VinNumber, ModelTypeId, BodyStyleId, InteriorColorId, ExteriorColorId, TransmissionTypeId, NeworUsedTypeId, ImageFileName, MSRP, Mileage, SalePrice, [Year], VehicleDescription, Sold, Feautured)
	VALUES (@VinNumber, @ModelTypeId, @BodyStyleId, @InteriorColorId, @ExteriorColorId, @TransmissionTypeId, @NeworUsedTypeId, @ImageFileName, @MSRP, @Mileage, @SalePrice, @Year, @VehicleDescription, @Sold, @Featured)
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'GetVehicleDetails')
      DROP PROCEDURE GetVehicleDetails
GO

CREATE PROCEDURE GetVehicleDetails (@VinNumber VARCHAR(17)) AS
BEGIN
	SELECT v.VinNumber, mo.ModelTypeId, mo.ModelType, mk.MakeTypeId, mk.MakeType, b.BodyStyleId, b.BodyStyle, i.InteriorColorId, i.InteriorColor, e.ExteriorColorId,
		   e.ExteriorColor, t.TransmissionTypeId, t.TransmissionType, v.ImageFileName, v.MSRP, v.Mileage, v.SalePrice, v.[Year], v.VehicleDescription
	FROM Vehicles v
		INNER JOIN	ModelTypes mo ON v.ModelTypeId = mo.ModelTypeId
		INNER JOIN  MakeTypes mk ON mk.MakeTypeId = mo.MakeTypeId
		INNER JOIN BodyStyles b ON v.BodyStyleId = b.BodyStyleId
		INNER JOIN  InteriorColors i ON v.InteriorColorId = i.InteriorColorId
		INNER JOIN ExteriorColors e ON v.ExteriorColorId = e.ExteriorColorId
		INNER JOIN TransmissionTypes t ON v.TransmissionTypeId = t.TransmissionTypeId
	WHERE v.VinNumber = @VinNumber;
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleUpdate')
      DROP PROCEDURE VehicleUpdate
GO

CREATE PROCEDURE VehicleUpdate (
	@VinNumber varchar(17),
	@ModelTypeId int,
	@BodyStyleId int,
	@InteriorColorId int,
	@ExteriorColorId int,
	@TransmissionTypeId int,
	@NeworUsedTypeId int,
	@ImageFileName varchar(50),
	@MSRP decimal(8,2),
	@Mileage int,
	@SalePrice decimal(8,2),
	@Year int,
	@VehicleDescription nvarchar(500),
	@Sold bit,
	@Featured bit
	) AS
BEGIN
	UPDATE Vehicles SET
		ModelTypeId = @ModelTypeId, 
		BodyStyleId = @BodyStyleId, 
		InteriorColorId = @InteriorColorId, 
		ExteriorColorId = @ExteriorColorId, 
		TransmissionTypeId = @TransmissionTypeId, 
		NeworUsedTypeId = @NeworUsedTypeId, 
		ImageFileName = @ImageFileName, 
		MSRP = @MSRP, 
		Mileage = @Mileage, 
		SalePrice = @SalePrice, 
		[Year] = @Year, 
		VehicleDescription = @VehicleDescription, 
		Sold = @Sold, 
		Feautured = @Featured
	WHERE VinNumber = @VinNumber
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'InventoryReportNewVehicles')
      DROP PROCEDURE InventoryReportNewVehicles
GO

CREATE PROCEDURE InventoryReportNewVehicles AS
BEGIN
	SELECT v.[Year], mk.MakeType, mo.ModelType, COUNT(v.VinNumber) AS [Count], SUM(v.MSRP) AS [StockValue] 
	FROM Vehicles v
		INNER JOIN ModelTypes mo ON v.ModelTypeId = mo.ModelTypeId
		INNER JOIN MakeTypes mk ON mo.MakeTypeId = mk.MakeTypeId
	WHERE v.NeworUsedTypeId = 1
	GROUP BY  v.[Year], mk.MakeType, mo.ModelType
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'InventoryReportUsedVehicles')
      DROP PROCEDURE InventoryReportUsedVehicles
GO

CREATE PROCEDURE InventoryReportUsedVehicles AS
BEGIN
	SELECT v.[Year], mk.MakeType, mo.ModelType, COUNT(v.VinNumber) AS [Count], SUM(v.MSRP) AS [StockValue] 
	FROM Vehicles v
		INNER JOIN ModelTypes mo ON v.ModelTypeId = mo.ModelTypeId
		INNER JOIN MakeTypes mk ON mo.MakeTypeId = mk.MakeTypeId
	WHERE v.NeworUsedTypeId = 2
	GROUP BY  v.[Year], mk.MakeType, mo.ModelType
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleDelete')
      DROP PROCEDURE VehicleDelete
GO

CREATE PROCEDURE VehicleDelete (
	@VinNumber varchar(17)
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM Sales WHERE VinNumber = @VinNumber;
	DELETE FROM Vehicles WHERE VinNumber = @VinNumber;

	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SaleInsert')
      DROP PROCEDURE SaleInsert
GO

CREATE PROCEDURE SaleInsert (
	@SalesId int output,
	@UserId nvarchar(128),
	@VinNumber varchar(17),
	@PurchasePrice decimal(8,2),
	@PurchaseDate datetime2(7),
	@FirstName nvarchar(30),
	@LastName nvarchar(50),
	@Email nvarchar(30),
	@Phone nvarchar(24),
	@Street1 nvarchar(40),
	@Street2 nvarchar(40),
	@City nvarchar(30),
	@StateId char(2),
	@ZipCode varchar(10),
	@PurchaseTypeId int
	) AS
BEGIN
	INSERT INTO	Sales (UserId, VinNumber, PurchasePrice, PurchaseDate, FirstName, LastName, Email, Phone, Street1, Street2, City, StateId, ZipCode, PurchaseTypeId)
	VALUES (@UserId, @VinNumber, @PurchasePrice, @PurchaseDate, @FirstName, @LastName, @Email, @Phone, @Street1, @Street2, @City, @StateId, @ZipCode, @PurchaseTypeId)

	SET @SalesId = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakeTypeInsert')
      DROP PROCEDURE MakeTypeInsert
GO

CREATE PROCEDURE MakeTypeInsert (
	@MakeTypeId int output,
	@MakeType nvarchar(30),
	@UserId nvarchar(128)	
	) AS
BEGIN
	INSERT INTO	MakeTypes(MakeType, UserId)
	VALUES (@MakeType, @UserId)

	SET @MakeTypeId = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'GetMakeTypeDetails')
      DROP PROCEDURE GetMakeTypeDetails
GO

CREATE PROCEDURE GetMakeTypeDetails AS
BEGIN
	SELECT mt.MakeType, mt.DateAdded, u.Email
	FROM MakeTypes mt
		INNER JOIN AspNetUsers u ON mt.UserId = u.Id
	ORDER BY mt.DateAdded DESC
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelTypeInsert')
      DROP PROCEDURE ModelTypeInsert
GO

CREATE PROCEDURE ModelTypeInsert (
	@ModelTypeId int output,
	@MakeTypeId int,
	@ModelType nvarchar(50),
	@UserId nvarchar(128)	
	) AS
BEGIN
	INSERT INTO	ModelTypes(ModelType, MakeTypeId, UserId)
	VALUES (@ModelType, @MakeTypeId, @UserId)

	SET @ModelTypeId = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'GetModelTypeDetails')
      DROP PROCEDURE GetModelTypeDetails
GO

CREATE PROCEDURE GetModelTypeDetails AS
BEGIN
	SELECT mk.MakeType, mo.ModelType, mo.DateAdded, u.Email
	FROM ModelTypes mo
		INNER JOIN MakeTypes mk ON mo.MakeTypeId = mk.MakeTypeId
		INNER JOIN AspNetUsers u ON mo.UserId = u.Id
	ORDER BY mo.DateAdded DESC
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'GetFeaturedVehicles')
      DROP PROCEDURE GetFeaturedVehicles
GO

CREATE PROCEDURE GetFeaturedVehicles AS
BEGIN
	SELECT v.VinNumber, mo.ModelType, mk.MakeType, v.ImageFileName, v.SalePrice, v.[Year]
	FROM Vehicles v 
		INNER JOIN ModelTypes mo ON v.ModelTypeId = mo.ModelTypeId
		INNER JOIN MakeTypes mk ON mo.MakeTypeId = mk.MakeTypeId
	WHERE v.Feautured = 1

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialInsert')
      DROP PROCEDURE SpecialInsert
GO

CREATE PROCEDURE SpecialInsert (
	@SpecialId int output,
	@Title nvarchar(30),
	@UserId nvarchar(128),
	@SpecialDescription nvarchar(500)
	) AS
BEGIN
	INSERT INTO	Specials(Title, UserId, SpecialDescription)
	VALUES (@Title, @UserId, @SpecialDescription)

	SET @SpecialId = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'GetSpecials')
      DROP PROCEDURE GetSpecials
GO

CREATE PROCEDURE GetSpecials AS
BEGIN
	SELECT Title, SpecialDescription
	FROM Specials
END
GO
