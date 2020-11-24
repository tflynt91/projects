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
	SELECT v.[Year], mk.MakeType, mo.ModelType, COUNT(v.VinNumber) AS [Count], SUM(v.MSRP) AS [Stock Value] 
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
	SELECT v.[Year], mk.MakeType, mo.ModelType, COUNT(v.VinNumber) AS [Count], SUM(v.MSRP) AS [Stock Value] 
	FROM Vehicles v
		INNER JOIN ModelTypes mo ON v.ModelTypeId = mo.ModelTypeId
		INNER JOIN MakeTypes mk ON mo.MakeTypeId = mk.MakeTypeId
	WHERE v.NeworUsedTypeId = 2
	GROUP BY  v.[Year], mk.MakeType, mo.ModelType
END

GO
