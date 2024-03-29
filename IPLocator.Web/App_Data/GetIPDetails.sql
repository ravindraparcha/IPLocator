USE [IPAddressMapping]
GO
/****** Object:  StoredProcedure [dbo].[GetIPDetails]    Script Date: 12-07-2016 10:03:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
--EXEC GetIPDetails 1729978629


-- =============================================
ALTER PROCEDURE [dbo].[GetIPDetails]
	@IPAddress BIGINT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--print @IPAddress
    -- Insert statements for procedure here
	DECLARE @PostalCode VARCHAR(50)	
	DECLARE @Latitude VARCHAR(50) 	
	DECLARE @Longitude 	VARCHAR(50)
	DECLARE @radius INT
	DECLARE @Geonameid BIGINT

	 SELECT 
	 @PostalCode =[PostalCode ],
	 @Latitude = [Latitude ],
	 @Longitude=[Longitude ],
	 @radius = [radius ],
	 @Geonameid=[GeonameId ]
	 FROM IPADDRESS WHERE [MINIPADDRESS ]<=@IPAddress AND [MAXIPADDRESS ]>=@IPAddress

	  
	 SELECT 
	 [LocaleCode ],
	[ContinentCode ],
	[SubdivisionISOCode1 ],
	[SubdivisionName1 ],
	[SubdivisionISOCode2 ],
	[SubdivisionName2 ],
	[City ],
	[MetroCode ],
	[TIMEZONE ],
	 @PostalCode AS PostalCode,
	 @Latitude as Latitude,
	 @Longitude as Longitude,
	 @radius as Radius,
	 CountryName,
	 CountryISOCode,
	 ContinentName
	 
	 FROM CityLocation WHERE [GeonameId ]=@Geonameid
END
