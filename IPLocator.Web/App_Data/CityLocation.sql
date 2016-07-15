USE [IPAddressMapping]
GO

/****** Object:  Table [dbo].[CityLocation]    Script Date: 12-07-2016 02:42:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CityLocation](
	[GeonameId ] [bigint] NULL,
	[LocaleCode ] [varchar](50) NULL,
	[ContinentCode ] [varchar](50) NULL,
	[SubdivisionISOCode1 ] [varchar](100) NULL,
	[SubdivisionName1 ] [varchar](100) NULL,
	[SubdivisionISOCode2 ] [varchar](100) NULL,
	[SubdivisionName2 ] [varchar](100) NULL,
	[City ] [varchar](500) NULL,
	[MetroCode ] [varchar](50) NULL,
	[TimeZone ] [varchar](100) NULL,
	[CountryISOCode] [varchar](50) NULL,
	[CountryName] [varchar](100) NULL,
	[ContinentName] [varchar](100) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

CREATE NONCLUSTERED INDEX INDEX_CITYLOC_GEONAMEID ON CITYLOCATION(GEONAMEID ASC)

