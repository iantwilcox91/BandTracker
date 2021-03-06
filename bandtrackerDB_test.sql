USE [BandTracker_test]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 10/2/2016 10:18:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venues]    Script Date: 10/2/2016 10:18:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venuesBands]    Script Date: 10/2/2016 10:18:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venuesBands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bandId] [int] NULL,
	[venueId] [int] NULL
) ON [PRIMARY]

GO
