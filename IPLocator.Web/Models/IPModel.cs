using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPLocator.Web.Models
{
    public class IPDetails
    {
        public string PostalCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int radius { get; set; }

        public int GeonameId { get; set; }
        public string LocaleCode { get; set; }
        public string ContinentCode { get; set; }
        public string ContinentName { get; set; }
        public string CountryISOCode { get; set; }
        public string CountryName { get; set; }
        public string SubdivisionISOCode1 { get; set; }
        public string SubdivisionName1 { get; set; }
        public string SubdivisionISOCode2 { get; set; }
        public string SubdivisionName2 { get; set; }
        public string City { get; set; }
        public string MetroCode { get; set; }
        public string TimeZone { get; set; }
    }
    public class PlacesInfo
    {
        public string rating { get; set; }
        public string name { get; set; }
        public string vicinity { get; set; }
    }
    public class Places
    {
        public List<PlacesInfo> results { get; set; }
        public string status { get; set; }
    }

    // add predefined list of supported search places by google
    public class PredefinedPlaceInfo
    {
        Dictionary<string, string> placeList = new Dictionary<string, string>();
        Dictionary<string, string> radiusList = new Dictionary<string, string>();//distance in meter
        public PredefinedPlaceInfo()
        {
            placeList.Add("accounting", "Accounting");
            placeList.Add("airport", "Airport");
            placeList.Add("atm", "ATM");
            placeList.Add("bakery", "Bakery");
            placeList.Add("bank", "Bank");
            placeList.Add("bar", "Bar");
            placeList.Add("book_store", "Book Store");
            placeList.Add("bus_station", "Bus Stop");
            placeList.Add("cafe", "Cafe");
            placeList.Add("car_dealer", "Car Dealer");
            placeList.Add("dentist", "Dentist");
            placeList.Add("hindu_temple", "Hindu Temple");
            placeList.Add("hospital", "Hospital");
            placeList.Add("jewelry_store", "Jewelry Store");
            placeList.Add("liquor_store", "Liquor Store");
            placeList.Add("movie_theater", "Movie Theater");
            placeList.Add("night_club", "Night Club");
            placeList.Add("pharmacy", "Pharmacy");
            placeList.Add("restaurant", "Restaurant");
            placeList.Add("school", "School");
            placeList.Add("shopping_mall", "Shopping Mall");
            placeList.Add("university", "University");

            radiusList.Add("200", "200 meters");
            radiusList.Add("400", "400 meters");
            radiusList.Add("600", "600 meters");
            radiusList.Add("800", "800 meters");
            radiusList.Add("1000", "1 kilo meters");
            radiusList.Add("10000", "10 kilo meters");
            radiusList.Add("20000", "20 kilo meters");
            radiusList.Add("50000", "50 kilo meters");

        }

        public Dictionary<string, string> SupportedPlacesList
        {
            get { return placeList; }
        }

        public Dictionary<string, string> RadiusList
        {

            get { return radiusList; }
        }

        public string PlaceKey { get; set; }
        public string RadiusKey { get; set; }

    }
}