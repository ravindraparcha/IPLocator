USE [IPAddressMapping]
GO

/****** Object:  Table [dbo].[IPAddress]    Script Date: 12-07-2016 02:43:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IPAddress](
	[NetworkAddrRange ] [varchar](50) NULL,
	[MinIPAddress ] [bigint] NULL,
	[MaxIPAddress ] [bigint] NULL,
	[GeonameId ] [bigint] NULL,
	[CountryGeonameId ] [bigint] NULL,
	[PostalCode ] [varchar](50) NULL,
	[Latitude ] [varchar](50) NULL,
	[Longitude ] [varchar](50) NULL,
	[radius ] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


Create NonClustered Index IPAddr_MinMaxIPAddr On IPAddress
([MinIPAddress ] Asc, [MaxIPAddress ] Asc)