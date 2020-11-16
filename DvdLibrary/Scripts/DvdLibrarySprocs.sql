IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SearchByDirector')
      DROP PROCEDURE SearchByDirector
GO

CREATE PROCEDURE SearchByDirector (
	@Director nvarchar(50)
) AS
BEGIN
	SELECT DvdId, DvdTitle, ReleaseYear, Director, Rating, Notes
	FROM DvdItem
	WHERE Director = @Director
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SearchById')
      DROP PROCEDURE SearchById
GO

CREATE PROCEDURE SearchById (
	@DvdId int
) AS
BEGIN
	SELECT DvdId, DvdTitle, ReleaseYear, Director, Rating, Notes
	FROM DvdItem
	WHERE DvdId = @DvdId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SearchByRating')
      DROP PROCEDURE SearchByRating
GO

CREATE PROCEDURE SearchByRating (
	@Rating char(5)
) AS
BEGIN
	SELECT DvdId, DvdTitle, ReleaseYear, Director, Rating, Notes
	FROM DvdItem
	WHERE Rating = @Rating
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SearchByReleaseYear')
      DROP PROCEDURE SearchByReleaseYear
GO

CREATE PROCEDURE SearchByReleaseYear (
	@ReleaseYear varchar(4)
) AS
BEGIN
	SELECT DvdId, DvdTitle, ReleaseYear, Director, Rating, Notes
	FROM DvdItem
	WHERE ReleaseYear = @ReleaseYear
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SearchByTitle')
      DROP PROCEDURE SearchByTitle
GO

CREATE PROCEDURE SearchByTitle (
	@DvdTitle nvarchar(50)
) AS
BEGIN
	SELECT DvdId, DvdTitle, ReleaseYear, Director, Rating, Notes
	FROM DvdItem
	WHERE DvdTitle = @DvdTitle
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectAllDvds')
      DROP PROCEDURE SelectAllDvds
GO

CREATE PROCEDURE SelectAllDvds AS
BEGIN
	SELECT DvdId, DvdTitle, ReleaseYear, Director, Rating, Notes
	FROM DvdItem
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'CreateDvd')
      DROP PROCEDURE CreateDvd
GO

CREATE PROCEDURE CreateDvd (
	@DvdId int output,
	@DvdTitle nvarchar(50),
	@ReleaseYear varchar(4),
	@Director nvarchar(50),
	@Rating char(5),
	@Notes nvarchar(500)
) AS
BEGIN
	INSERT INTO DvdItem (DvdTitle, ReleaseYear, Director, Rating, Notes)
	VALUES (@DvdTitle, @ReleaseYear, @Director, @Rating, @Notes);

	SET @DvdId = SCOPE_IDENTITY();
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'UpdateDvd')
      DROP PROCEDURE UpdateDvd
GO

CREATE PROCEDURE UpdateDvd (
	@DvdId int,
	@DvdTitle nvarchar(50),
	@ReleaseYear varchar(4),
	@Director nvarchar(50),
	@Rating char(5),
	@Notes nvarchar(500)
) AS
BEGIN
	UPDATE DvdItem SET  
		DvdTitle = @DvdTitle, 
		ReleaseYear = @ReleaseYear,
		Director = @Director, 
		Rating = @Rating,
		Notes = @Notes
	WHERE DvdId = @DvdId
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DeleteDvd')
      DROP PROCEDURE DeleteDvd
GO

CREATE PROCEDURE DeleteDvd (
	@DvdId int
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM DvdItem WHERE DvdId = @DvdId;

	COMMIT TRANSACTION
END
GO