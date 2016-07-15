using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPLocator.Library
{
    public class CSVParser
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        string[] filePath = new string[2];
        DataTable dtIP4 = new DataTable();
        DataTable dtCityLoc = new DataTable();
        string dbConnString; int batchSize;
        public CSVParser(string[] filePath, string dbConnString,int batchSize)
        {
            this.filePath = filePath;
            this.dbConnString = dbConnString;
            this.batchSize = batchSize;

            dtIP4.Columns.Add("NetworkAddrRange", typeof(string));
            dtIP4.Columns.Add("MinIPAddress", typeof(Int64));
            dtIP4.Columns.Add("MaxIPAddress", typeof(Int64));
            dtIP4.Columns.Add("GeonameId", typeof(Int64));
            dtIP4.Columns.Add("CountryGeonameId", typeof(Int64));
            dtIP4.Columns.Add("PostalCode", typeof(string));
            dtIP4.Columns.Add("Latitude", typeof(string));
            dtIP4.Columns.Add("Longitude", typeof(string));
            dtIP4.Columns.Add("radius", typeof(int));

            dtCityLoc.Columns.Add("GeonameId", typeof(Int64));
            dtCityLoc.Columns.Add("LocaleCode", typeof(string));
            dtCityLoc.Columns.Add("ContinentCode", typeof(string));
            dtCityLoc.Columns.Add("SubdivisionISOCode1", typeof(string));
            dtCityLoc.Columns.Add("SubdivisionName1", typeof(string));
            dtCityLoc.Columns.Add("SubdivisionISOCode2", typeof(string));
            dtCityLoc.Columns.Add("SubdivisionName2", typeof(string));
            dtCityLoc.Columns.Add("City", typeof(string));
            dtCityLoc.Columns.Add("MetroCode", typeof(string));
            dtCityLoc.Columns.Add("TimeZone", typeof(string));            
            dtCityLoc.Columns.Add("CountryISOCode", typeof(string));
            dtCityLoc.Columns.Add("CountryName", typeof(string));
            dtCityLoc.Columns.Add("ContinentName", typeof(string));
        }

        public void ReadUploadInDB()
        {
            ReadCSV(filePath[0],filePath[1]);
        }

        private void ReadCSV(string ip4Path,string cityLocationPath)
        {
            string currentLine = string.Empty;
            SqlConnection conn = new SqlConnection(dbConnString);
            
            try
            {
                #region IP4 
                using (var stream = new FileStream(ip4Path, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var reader = new StreamReader(stream, Encoding.Default, true))
                {
                    string ntwkAddr, ipAddress = null, geonameId, countryGeonameId, representedGeonameId;
                    string isAnnonymous, satelliteProvider, postalCode, latitude, longitude, radius;
                    int cidr;//, batchSizeCounter = 0;
                    uint ntwkAddress, subnetMask, minIPAddress, maxIPAddress;

                    reader.ReadLine();
                    
                    while ((currentLine = reader.ReadLine()) != null)
                    {
                        
                        ntwkAddr = currentLine.Substring(0, currentLine.IndexOf(","));
                        cidr = Convert.ToInt32(ntwkAddr.Substring(ntwkAddr.IndexOf("/") + 1));
                        ipAddress = ntwkAddr.Substring(0, ntwkAddr.IndexOf("/"));
                        byte[] addr = IPAddress.Parse(ipAddress).GetAddressBytes();
                        Array.Reverse(addr);
                        ntwkAddress = BitConverter.ToUInt32(addr, 0);
                        subnetMask = GetMask(cidr);
                        minIPAddress = ntwkAddress;
                        maxIPAddress = GetMaxIPAddress(ntwkAddress, subnetMask);
                        
                        currentLine = currentLine.Remove(0, currentLine.IndexOf(",") + 1);
                        while (currentLine != "")
                        {
                            /*
                             * 1-network,
                             * 2-geoname_id,
                             * 3-registered_country_geoname_id,
                             * 4-represented_country_geoname_id,
                             * 5-is_anonymous_proxy,
                             * 6-is_satellite_provider,
                             * 7-postal_code,
                             * 8-latitude,
                             * 9-longitude,
                             * 10-accuracy_radius
                             */
                            geonameId = ExtractString(",", ref currentLine);
                            countryGeonameId = ExtractString(",", ref currentLine);
                            representedGeonameId = ExtractString(",", ref currentLine);
                            isAnnonymous = ExtractString(",", ref currentLine);
                            satelliteProvider = ExtractString(",", ref currentLine);
                            postalCode = ExtractString(",", ref currentLine);
                            latitude = ExtractString(",", ref currentLine);
                            longitude = ExtractString(",", ref currentLine);
                            radius = ExtractString(",", ref currentLine);


                            DataRow dr = dtIP4.NewRow();

                            dr["NetworkAddrRange"] = ntwkAddr;
                            dr["MinIPAddress"] = Convert.ToInt64(minIPAddress);
                            dr["MaxIPAddress"] = Convert.ToInt64(maxIPAddress);
                            dr["GeonameId"] = Convert.ToInt64(geonameId == "" ? "0" : geonameId);
                            dr["CountryGeonameId"] = Convert.ToInt64(countryGeonameId == "" ? "0" : countryGeonameId);
                            dr["PostalCode"] = postalCode == "" ? "0" : postalCode;
                            dr["Latitude"] = latitude;
                            dr["Longitude"] = longitude;
                            dr["radius"] = Convert.ToInt32(radius == "" ? "0" : radius);
                            dtIP4.Rows.Add(dr);
                            
                        }

                    }

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    using (SqlTransaction sqlTransaction = conn.BeginTransaction())
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, sqlTransaction))
                        {
                            //Set the database table name
                            sqlBulkCopy.DestinationTableName = "dbo.IPAddress";
                            sqlBulkCopy.BatchSize = batchSize;
                            try
                            {
                                sqlBulkCopy.WriteToServer(dtIP4);
                                sqlTransaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.ToString());
                                sqlTransaction.Rollback();
                            }
                        }
                    }

                    dtIP4.Clear();

                }
                #endregion

                #region City location
                using (var stream = new FileStream(cityLocationPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var reader = new StreamReader(stream, Encoding.Default, true))
                {
                    string geonameId, localCode, continentCode, continentName, countryISOCode, countryName, subdivisionISOCode1, subdivisionName1;
                    string subdivisionISOCode2, subdivisionName2, city, metroCode, timeZone;                   
                    reader.ReadLine();
                    while ((currentLine = reader.ReadLine()) != null)
                    {
                        while (currentLine != "")
                        {
                            /*0-geoname_id,
                         * 1-locale_code,
                         * 2-continent_code,
                         * 3-continent_name,
                         * 4-country_iso_code,
                         * 5-country_name,
                         * 6-subdivision_1_iso_code,
                         * 7-subdivision_1_name,
                        8-Subdivision_2_iso_code,
                        9-subdivision_2_name,
                        10-city_name,
                        11-metro_code,
                        12-time_zone*/
                            geonameId = ExtractString(",", ref currentLine);
                            localCode = ExtractString(",", ref currentLine);
                            continentCode = ExtractString(",", ref currentLine);
                            continentName = ExtractString(",", ref currentLine);
                            countryISOCode = ExtractString(",", ref currentLine);
                            countryName = ExtractString(",", ref currentLine);
                            subdivisionISOCode1 = ExtractString(",", ref currentLine);
                            subdivisionName1 = ExtractString(",", ref currentLine);
                            subdivisionISOCode2 = ExtractString(",", ref currentLine);
                            subdivisionName2 = ExtractString(",", ref currentLine);
                            city = ExtractString(",", ref currentLine);
                            metroCode = ExtractString(",", ref currentLine);
                            timeZone = ExtractString(",", ref currentLine);

                            DataRow dr = dtCityLoc.NewRow();

                            dr["GeonameId"] =Convert.ToInt64(geonameId);
                            dr["LocaleCode"] = localCode;
                            dr["ContinentCode"] = continentCode;
                            dr["SubdivisionISOCode1"] = subdivisionISOCode1;
                            dr["SubdivisionName1"] = subdivisionName1;
                            dr["SubdivisionISOCode2"] = subdivisionISOCode2;
                            dr["SubdivisionName2"] = subdivisionName2;
                            dr["City"] = city;
                            dr["MetroCode"] = metroCode;
                            dr["TimeZone"] = timeZone;
                            dr["CountryISOCode"] = countryISOCode;
                            dr["CountryName"] = countryName;
                            dr["ContinentName"] = continentName;
                           
                            dtCityLoc.Rows.Add(dr);
                            
                            }
                    }


                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    using (SqlTransaction sqlTransaction = conn.BeginTransaction())
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, sqlTransaction))
                        {
                            //Set the database table name
                            sqlBulkCopy.DestinationTableName = "dbo.CityLocation";
                            sqlBulkCopy.BatchSize = batchSize;
                            try
                            {
                                sqlBulkCopy.WriteToServer(dtCityLoc);
                                sqlTransaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.ToString());
                                sqlTransaction.Rollback();
                            }
                        }
                    }

                    dtCityLoc.Clear();

                }
                #endregion
            }
            finally { conn.Close();  }
           
        }

        private static string ExtractString(string separator, ref string line)
        {
            if (line == "")
            {
                return "";
            }
            string extracted = null;
            int index;
            if (line.Substring(0, 1) == "\"")
            {
                index = line.IndexOf("\"", 0);
                index = line.IndexOf("\",", index + 1);
                if (index == -1)
                {
                    extracted = line.Substring(1, line.Length - 2);
                }
                else
                {
                    extracted = line.Substring(1, index - 1);
                    index += 1;
                }
            }
            else
            {
                index = line.IndexOf(",");
                if (index == 0)
                {
                    extracted = "";
                }
                else if (index == -1)
                {
                    extracted = line;
                }
                else
                {
                    extracted = line.Substring(0, index);
                }
            }
            line = (index == -1) ? "" : line.Remove(0, index + 1);
            return extracted;
        }

        private static uint GetMaxIPAddress(uint ntwkAddress, uint subNetMask)
        {
            return (ntwkAddress | (uint)((~(ulong)subNetMask) & 0xFFFFFFFF));
        }

        private static uint GetMask(int cidr)
        {
            byte[] ip = new byte[4];

            if (cidr > 8)
            {
                ip[0] = 255;
            }
            else
            {
                byte temp = 128;
                for (int i = 0; i < cidr; i++)
                {
                    ip[0] |= temp;
                    temp = (byte)(temp >> 1);
                }
                Array.Reverse(ip);
                return BitConverter.ToUInt32(ip, 0);
            }

            if (cidr > 16)
            {
                ip[1] = 255;
            }
            else
            {
                byte temp = 128;
                for (int i = 0; i < (cidr - 8); i++)
                {
                    ip[1] |= temp;
                    temp = (byte)(temp >> 1);
                }
                Array.Reverse(ip);
                return BitConverter.ToUInt32(ip, 0);
            }

            if (cidr > 24)
            {
                ip[2] = 255;
            }
            else
            {
                byte temp = 128;
                for (int i = 0; i < (cidr - 16); i++)
                {
                    ip[2] |= temp;
                    temp = (byte)(temp >> 1);
                }
                Array.Reverse(ip);
                return BitConverter.ToUInt32(ip, 0);
            }

            {
                byte temp = 128;
                for (int i = 0; i < (cidr - 24); i++)
                {
                    ip[3] |= temp;
                    temp = (byte)(temp >> 1);
                }
                Array.Reverse(ip);
                return BitConverter.ToUInt32(ip, 0);
            }
        }

    }
}
