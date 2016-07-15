using IPLocator.Web.DBLayer;
using IPLocator.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPLocator.Web.Controllers
{
    public class HomeController : Controller
    {
        private static  string dbConnString = ConfigurationManager.ConnectionStrings["DBConnString"].ToString();
        IPDBClass ipDBClass = new IPDBClass(dbConnString);
        public ActionResult Index()
        {
            return View("Index", new PredefinedPlaceInfo());
        }

        public PartialViewResult GetIPDetails(string IPAddress)
        {
               
            return PartialView("~/Views/Home/_IPDetail.cshtml", ipDBClass.GetIPAddressDetails(IPAddress));
        }

        public PartialViewResult GetNearbyPlaces(string lat, string longi, string place, string radius,string ipAddress)
        {
            if (!string.IsNullOrEmpty(ipAddress))
            {
                IPDetails ipDetails = ipDBClass.GetIPAddressDetails(ipAddress);
                lat = ipDetails.Latitude;
                longi = ipDetails.Longitude;
                ViewBag.InfoMsg = string.Format("Near by places for \"{0}\"- ",ipAddress);
            }
            else
            {
                ViewBag.InfoMsg = "Near by places from current location- ";
            }

            //http://stackoverflow.com/questions/26694049/how-to-use-google-maps-simple-api-on-localhost
            // link for google maps in mvc5 http://www.c-sharpcorner.com/article/integrating-google-maps-places-and-geocode-apis-with-asp-net-mvc-5/
            //create the key of type- Google Places API Web Service
            //string placeApiUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input={0}&types=geocode&language=en&key={1}";
            //string placeApiUrl = "https://maps.googleapis.com/maps/api/place/textsearch/xml?query=restaurants+in+Chembur&key={1}";
            //https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=-33.8670,151.1957&radius=500&types=food&name=cruise&key=YOUR_API_KEY
            //string placeApiUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=-33.8670,151.1957&radius=500&types=food&key={1}";

            string placeApiUrl = ConfigurationManager.AppSettings["GoogleApiUrl"].Replace("@:", "&");
            List<PlacesInfo> placesInfo = new List<PlacesInfo>();
           
            placeApiUrl = string.Format(placeApiUrl, lat, longi, radius, place, ConfigurationManager.AppSettings["GoogleAPIServerKey"]);

            var result = new System.Net.WebClient().DownloadString(placeApiUrl);
            placesInfo = JsonConvert.DeserializeObject<Places>(result).results;
               
            return PartialView("~/Views/Home/_NearbySearch.cshtml", placesInfo);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}