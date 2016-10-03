# _Band Tracker_

#### _this application mimics a venue planner._

#### By _**ian Wilcox**_

## Description

_this application allows a person to mimic the way someone would look into local venues for the events they host and also what locations a band would be playing at. _


## Setup/Installation Requirements

* sqlcmd -S "(localdb)\mssqllocaldb"

* CREATE DATABASE BandTracker;
* GO
* USE BandTracker;
* GO
* CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255));
* CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255));
* CREATE TABLE venuesBands (id INT IDENTITY(1,1), bandId INT, venueId INT);
* GO


## Technologies Used

_HTML, C#, CSS, sql, and Nancy_

### License

*licensed under Mit*

Copyright (c) 2016 **_Ian Wilcox_**
