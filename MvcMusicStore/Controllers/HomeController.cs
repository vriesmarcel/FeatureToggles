using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcMusicStore.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using Microsoft.ApplicationInsights;
using MvcMusicStore.FeaturetoggleSwitches;


namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        MusicStoreEntities storeDB = new MusicStoreEntities();

        public ActionResult Index()
        {
            // Get most popular albums
            var albums = GetTopSellingAlbums(5);
            if (new HomePagefeatureToggle().FeatureEnabled)
            {
                var customerCountry = GetCountryFromClient(Request.UserHostAddress);
                ViewBag.CustomerCountry = customerCountry;
            }

            return View(albums);
        }

       

        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }
        private string GetCountryFromClient(string ip)
        {
            string result = null;
            if(new FeaturetoggleSwitches.ServiceAFeaturetoggle().FeatureEnabled)
            {
                result = GetCountryViaRemoteService(ip);
            }
            else
            {
                result = GetCountryFromLocalLookup(ip);
            }

            return result;
        }

        private string GetCountryFromLocalLookup(string ip)
        {
            var result = "Amsterdam";
            DoSomeSmartCalculation(ip);
            return result;
        }

        private void DoSomeSmartCalculation(string ip)
        {
            // do a bussy wait to simulate some heavy serverside stuff
            // to come up with a number.
            for (int x = 0; x < 1000000000; x++) ;

        }

        private string GetCountryViaRemoteService(string ip)
        {
            var result = "unknown location";
            try
            {
                var serviceUrl = "http://ipinfo.io/" + ip;
                HttpClient request = new HttpClient();
                var taskresult = request.GetStringAsync((new Uri(serviceUrl)));
                taskresult.Wait();

                var location = JsonConvert.DeserializeObject<Location>(taskresult.Result);
                if(location.city!=null)
                    result = location.city.ToString();
            }
            catch (Exception e)
            {
                TelemetryClient client = new TelemetryClient();
                client.TrackException(e);
            }
            return result;
        }
    }

    public class Location
    {
        public string hostname { get; set; }
        public object city { get; set; }
        public string country { get; set; }
        public string loc { get; set; }
        public string org { get; set; }
    }
}