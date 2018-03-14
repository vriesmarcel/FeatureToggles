using System;
using System.Linq;
using System.Web.Mvc;
using MvcMusicStore.Models;
using MvcMusicStore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.ApplicationInsights;

namespace MvcMusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        MusicStoreEntities storeDB = new MusicStoreEntities();

        //
        // GET: /ShoppingCart/

        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            // Return the view
            return View(viewModel);
        }

        //
        // GET: /Store/AddToCart/5

        public ActionResult AddToCart(int id)
        {

            // Retrieve the album from the database
            var addedAlbum = storeDB.Albums
                .Single(album => album.AlbumId == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedAlbum);

           
            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

      

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            var album = storeDB.Carts
                .Single(item => item.RecordId == id).Album;
            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);
            // log telemetry data to understand when people remove items from the basket
            LogTelemetryEvent(cart, album);
            //if (cart.GetTotal() > 30)
            //    throw new InvalidOperationException("Value is > 30, not allowed to remove items....:-)");

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(album.Title) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            

            return Json(results);
        }


 
        private double GetShoppingbasketTotalRange(ShoppingCart cart)
        {
            return Convert.ToDouble(cart.GetTotal());
        }
        //
        // GET: /ShoppingCart/CartSummary

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartSummary");
        }


        private void LogTelemetryEvent(ShoppingCart cart, Album album)
        {
            var basketValue = GetShoppingbasketTotalRange(cart);
            TelemetryClient client = new TelemetryClient();
            var properties = new Dictionary<string, string>();
            properties.Add("Amount segment", GetShoppingbasketTotalRangeSegment(basketValue).ToString());
            properties.Add("Genre", album.Genre.Name);
            properties.Add("Artist Name", album.Artist.Name);
            var measurements = new Dictionary<string, double>();
            measurements.Add("TotalAmount", basketValue);

            client.TrackEvent("Item removed", properties, measurements);
        }

        private double GetShoppingbasketTotalRangeSegment(double basketValue)
        {

            if (basketValue <= 100)
                return 100;
            if (basketValue <= 500)
                return 500;
            if (basketValue <= 1000)
                return 1000;
            return 10000;
        }
    }


}