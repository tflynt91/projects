USE master
GO
 
CREATE LOGIN DvdLibraryApp WITH PASSWORD='testing123'
GO

USE DvdLibrary
GO
 
CREATE USER DvdLibraryApp FOR LOGIN DvdLibraryApp
GO

CREATE ROLE db_executor

GRANT EXECUTE TO  db_executor

ALTER ROLE db_executor ADD MEMBER DvdLibraryApp
GO

GRANT SELECT ON DvdItem TO DvdLibraryApp
GRANT INSERT ON DvdItem TO DvdLibraryApp
GRANT UPDATE ON DvdItem TO DvdLibraryApp
GRANT DELETE ON DvdItem TO DvdLibraryApp
GO