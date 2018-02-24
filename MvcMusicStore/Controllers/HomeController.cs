using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        // comment feature 1

        MusicStoreEntities storeDB = null;


        public HomeController()
        {
             storeDB = new MusicStoreEntities();
        }

        public HomeController(MusicStoreEntities storeEntities)
        {
             storeDB = storeEntities;
        }

        public ActionResult Index()
        {
            // Get most popular albums
            var albums = GetTopSellingAlbums(5);
          
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