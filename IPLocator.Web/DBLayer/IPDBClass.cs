using IPLocator.Web.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;

namespace IPLocator.Web.DBLayer
{
    public class IPDBClass
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        string connString;
        public IPDBClass(string connString)
        {
            this.connString = connString;
        }
        public IPDetails GetIPAddressDetails(string ipAddress)
        {
            IPDetails ipDetail = new IPDetails();
            string sqlSP = "GetIPDetails";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand(sqlSP, conn);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.Add("@IPAddress", SqlDbType.BigInt).Value = ConvertIPToInt(ipAddress);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        DataTable dt = ds.Tables[0];

                        foreach (DataRow row in dt.Rows)
                        {
                            ipDetail.LocaleCode = row[0].ToString();
                            ipDetail.ContinentCode = row[1].ToString();
                            ipDetail.SubdivisionISOCode1 =row[2].ToString();
                            ipDetail.SubdivisionName1 = row[3].ToString();
                            ipDetail.SubdivisionISOCode2 = row[4].ToString();
                            ipDetail.SubdivisionName2 = row[5].ToString();

                            ipDetail.City = row[6].ToString();
                            ipDetail.MetroCode = row[7].ToString();
                            ipDetail.TimeZone = row[8].ToString();
                            ipDetail.PostalCode = row[9].ToString();
                            ipDetail.Latitude = row[10].ToString();
                            ipDetail.Longitude = row[11].ToString();
                            ipDetail.radius = Convert.ToInt32(row[12]);
                            ipDetail.CountryName = row[13].ToString();
                            ipDetail.CountryISOCode = row[14].ToString();
                            ipDetail.ContinentName = row[15].ToString();
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e.ToString());
                }
            }

            return ipDetail;

        }
        private UInt32 ConvertIPToInt(string ipAddress)
        {
            byte[] addr = IPAddress.Parse(ipAddress).GetAddressBytes();
            Array.Reverse(addr);
            return BitConverter.ToUInt32(addr, 0);

        }
    }
}