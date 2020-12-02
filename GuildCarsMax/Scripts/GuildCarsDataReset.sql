IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DbReset')
      DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN
DELETE FROM Vehicles;
DELETE FROM ModelTypes;
DELETE FROM MakeTypes;
DELETE FROM NewOrUsedTypes;
DELETE FROM PurchaseTypes;
DELETE FROM TransmissionTypes;
DELETE FROM States;
DELETE FROM ExteriorColors;
DELETE FROM InteriorColors;
DELETE FROM BodyStyles;

INSERT INTO States (StateId, StateName)
	VALUES ('AK', 'Alaska'),
		   ('AL', 'Alabama'),
		   ('AZ', 'Arizona'),
		   ('AR', 'Arkansas'),
		   ('CA', 'California'),
		   ('CO', 'Colorado'),
		   ('CT', 'Connecticut'),
		   ('DE', 'Delaware'),
		   ('DC', 'District of Columbia'),
		   ('FL', 'Florida'),
		   ('GA', 'Georgia'),
		   ('HI', 'Hawaii'),
		   ('ID', 'Idaho'),
		   ('IL', 'Illinois'),
		   ('IN', 'Indiana'),
		   ('IA', 'Iowa'),
		   ('KS', 'Kansas'),
		   ('KY', 'Kentucky'),
		   ('LA', 'Louisiana'),
		   ('ME', 'Maine'),
		   ('MD', 'Maryland'),
		   ('MA', 'Massachusetts'),
		   ('MI', 'Michigan'),
		   ('MN', 'Minnesota'),
		   ('MS', 'Mississippi'),
		   ('MO', 'Missouri'),
		   ('MT', 'Montana'),
		   ('NE', 'Nebraska'),
		   ('NV', 'Nevada'),
		   ('NH', 'New Hampshire'),
		   ('NJ', 'New Jersey'),
		   ('NM', 'New Mexico'),
		   ('NY', 'New York'),
		   ('NC', 'North Carolina'),
		   ('ND', 'North Dakota'),
		   ('OH', 'Ohio'),
		   ('OK', 'Oklahoma'),
		   ('OR', 'Oregon'),
		   ('PA', 'Pennsylvania'),
		   ('PR', 'Puerto Rico'),
		   ('RI', 'Rhode Island'),
		   ('SC', 'South Carolina'),
		   ('SD', 'South Dakota'),
		   ('TN', 'Tennessee'),
		   ('TX', 'Texas'),
		   ('UT', 'Utah'),
		   ('VT', 'Vermont'),
		   ('VA', 'Virginia'),
		   ('WA', 'Washington'),
		   ('WV', 'West Virginia'),
		   ('WI', 'Wisconsin'),
		   ('WY', 'Wyoming');

SET IDENTITY_INSERT ExteriorColors ON;

INSERT INTO ExteriorColors (ExteriorColorId, ExteriorColor)
	VALUES (1, 'White'),
		   (2, 'Black'),
		   (3, 'Silver'),
		   (4, 'Blue'),
		   (5, 'Gray'),
		   (6, 'Red'),
		   (7, 'Beige');

SET IDENTITY_INSERT ExteriorColors OFF;

SET IDENTITY_INSERT InteriorColors ON;

INSERT INTO InteriorColors (InteriorColorId, InteriorColor)
	VALUES (1, 'Black'),
		   (2, 'Pale'),
		   (3, 'Grey'),
		   (4, 'Tan'),
		   (5, 'Brown');

SET IDENTITY_INSERT InteriorColors OFF;

SET IDENTITY_INSERT BodyStyles ON;

INSERT INTO BodyStyles (BodyStyleId, BodyStyle)
	VALUES (1, 'Car'),
		   (2, 'SUV'),
		   (3, 'Truck'),
		   (4, 'Van');

SET IDENTITY_INSERT BodyStyles OFF;

SET IDENTITY_INSERT MakeTypes ON;

INSERT INTO MakeTypes (MakeTypeId, MakeType, UserId)
	VALUES (1, 'Audi', '00000000-0000-0000-0000-000000000000'),
		   (2, 'BMW', '00000000-0000-0000-0000-000000000000'),
		   (3, 'Chevrolet', '00000000-0000-0000-0000-000000000000'),
		   (4, 'Ford', '00000000-0000-0000-0000-000000000000'),
		   (5, 'Kia', '00000000-0000-0000-0000-000000000000');

SET IDENTITY_INSERT MakeTypes OFF;

SET IDENTITY_INSERT ModelTypes ON;

INSERT INTO ModelTypes (ModelTypeId, MakeTypeId, ModelType, UserId)
	VALUES (1, 1, 'S3', '00000000-0000-0000-0000-000000000000'),
		   (2, 1, 'A4', '00000000-0000-0000-0000-000000000000'),
		   (3, 1, 'Q3', '00000000-0000-0000-0000-000000000000'),
		   (4, 2, 'X5', '00000000-0000-0000-0000-000000000000'),
		   (5, 2, 'M6', '00000000-0000-0000-0000-000000000000'),
		   (6, 2, 'i8', '00000000-0000-0000-0000-000000000000'),
		   (7, 3, 'Tahoe', '00000000-0000-0000-0000-000000000000'),
		   (8, 3, 'Camaro', '00000000-0000-0000-0000-000000000000'),
		   (9, 3, 'Impala', '00000000-0000-0000-0000-000000000000'),
		   (10, 4, 'Fusion', '00000000-0000-0000-0000-000000000000'),
		   (11, 4, 'Escape', '00000000-0000-0000-0000-000000000000'),
		   (12, 5, 'Soul', '00000000-0000-0000-0000-000000000000'),
		   (13, 5, 'Sedona', '00000000-0000-0000-0000-000000000000');

SET IDENTITY_INSERT ModelTypes OFF;

SET IDENTITY_INSERT NewOrUsedTypes ON;

INSERT INTO NewOrUsedTypes (NewOrUsedTypeId, NeworUsedType)
	VALUES (1, 'New'),
		   (2, 'Used');

SET IDENTITY_INSERT NewOrUsedTypes OFF;

SET IDENTITY_INSERT PurchaseTypes ON;

INSERT INTO PurchaseTypes (PurchaseTypeId, PurchaseType)
	VALUES (1, 'Bank Finance'),
		   (2, 'Cash'),
		   (3, 'Dealer Finance');

SET IDENTITY_INSERT PurchaseTypes OFF;

SET IDENTITY_INSERT TransmissionTypes ON;

INSERT INTO TransmissionTypes (TransmissionTypeId, TransmissionType)
	VALUES (1, 'Automatic'),
		   (2, 'Manual');

SET IDENTITY_INSERT TransmissionTypes OFF;

INSERT INTO Vehicles (VinNumber, ModelTypeId, BodyStyleId, InteriorColorId, ExteriorColorId, TransmissionTypeId, NeworUsedTypeId,
					  ImageFileName, MSRP, Mileage, SalePrice, [Year], VehicleDescription, Sold, Feautured)
	VALUES ('YV1AH852071023377', 3, 1, 2, 3, 1, 1, 'placeholder.png', 15000, 1000, 13000, 2020, 'Brand new car!', 0, 0),
		   ('2D4FV48V05H529506', 6, 1, 2, 3, 1, 1, 'placeholder.png', 16000, 1000, 15000, 2020, 'Description', 0, 0),
		   ('JH4KA7680RC011845', 3, 1, 3, 4, 1, 1, 'placeholder.png', 15000, 1000, 14000, 2020, 'Description', 0, 0),
		   ('JN1CA21DXXT805880', 4, 2, 3, 4, 2, 2, 'placeholder.png', 13000, 1500, 11000, 2019, 'Great car!', 0, 0),
		   ('1FTRW12507FB38262', 4, 2, 3, 2, 1, 2, 'placeholder.png', 13000, 1500, 12000, 2019, 'Description', 0, 0),
		   ('2FTDF18W5VCA88039', 5, 1, 4, 2, 1, 2, 'placeholder.png', 12000, 1000, 11000, 2018, 'Desciription', 0, 1),
		   ('WBAFR9C59BC270614', 5, 2, 5, 2, 1, 2, 'placeholder.png', 14000, 1000, 11000, 2017, 'Desciription', 0, 1),
		   ('2FAFP73W1WX172908', 4, 1, 3, 2, 1, 2, 'placeholder.png', 13000, 4000, 12000, 2018, 'Description', 0, 1);

SET IDENTITY_INSERT Specials ON;

INSERT INTO Specials (SpecialId, Title, UserId, SpecialDescription)
	VALUES (1, 'Weekend Deal', '00000000-0000-0000-0000-000000000000', 'A Great deal on all used cars this weekend only!'),
		   (2, 'Black Friday Deal', '00000000-0000-0000-0000-000000000000', 'This black friday 10% off on all new cars!'),
		   (3, 'Used Honda Accura', '00000000-0000-0000-0000-000000000000', '$3,000 this used Honda Accura!'),
		   (4, 'New Mustang!', '00000000-0000-0000-0000-000000000000', '$500 off this brand new Mustang! Get it while its still here!');

SET IDENTITY_INSERT Specials OFF;


END