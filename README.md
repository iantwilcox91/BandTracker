sqlcmd -S "(localdb)\mssqllocaldb"

CREATE DATABASE BandTracker;
GO
USE BandTracker;
GO
CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255));
CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255));
CREATE TABLE venuesBands (id INT IDENTITY(1,1), bandId INT, venueId INT);
GO
