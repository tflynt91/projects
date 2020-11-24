IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DbReset')
      DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN
DELETE FROM MakeTypes;
DELETE FROM ModelTypes;
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

INSERT INTO ExteriorColors (ExteriorColor)
	VALUES ('White'),
		   ('Black'),
		   ('Silver'),
		   ('Blue'),
		   ('Gray'),
		   ('Red'),
		   ('Beige');

INSERT INTO InteriorColors (InteriorColor)
	VALUES ('Black'),
		   ('Pale'),
		   ('Grey'),
		   ('Tan');

INSERT INTO BodyStyles (BodyStyle)
	VALUES ('Car'),
		   ('SUV'),
		   ('Truck'),
		   ('Van');

INSERT INTO MakeTypes (MakeType, UserId)
	VALUES ('Audi', '00000000-0000-0000-0000-000000000000'),
		   ('BMW', '00000000-0000-0000-0000-000000000000'),
		   ('Chevrolet', '00000000-0000-0000-0000-000000000000'),
		   ('Ford', '00000000-0000-0000-0000-000000000000'),
		   ('Kia', '00000000-0000-0000-0000-000000000000');

INSERT INTO ModelTypes (MakeTypeId, ModelType, UserId)
	VALUES (1, 'S3', '00000000-0000-0000-0000-000000000000'),
		   (1, 'A4', '00000000-0000-0000-0000-000000000000'),
		   (1, 'Q3', '00000000-0000-0000-0000-000000000000'),
		   (2, 'X5', '00000000-0000-0000-0000-000000000000'),
		   (2, 'M6', '00000000-0000-0000-0000-000000000000'),
		   (2, 'i8', '00000000-0000-0000-0000-000000000000'),
		   (3, 'Tahoe', '00000000-0000-0000-0000-000000000000'),
		   (3, 'Camaro', '00000000-0000-0000-0000-000000000000'),
		   (3, 'Impala', '00000000-0000-0000-0000-000000000000'),
		   (4, 'Fusion', '00000000-0000-0000-0000-000000000000'),
		   (4, 'Escape', '00000000-0000-0000-0000-000000000000'),
		   (5, 'Soul', '00000000-0000-0000-0000-000000000000'),
		   (5, 'Sedona', '00000000-0000-0000-0000-000000000000');

INSERT INTO NewOrUsedTypes (NeworUsedType)
	VALUES ('New'),
		   ('Used');

INSERT INTO PurchaseTypes (PurchaseType)
	VALUES ('Bank Finance'),
		   ('Cash'),
		   ('Dealer Finance');

INSERT INTO TransmissionTypes (TransmissionType)
	VALUES ('Automatic'),
		   ('Manual');
END