using MvcMusicStore.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    public class BaseController : Controller
    {

        // Helper method to simplify shopping cart calls
        public ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = GetCartId(context);
            return cart;
        }

        public  string GetCartId(HttpContextBase context)
        {
            if (context.Session[ShoppingCart.CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[ShoppingCart.CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();

                    // Send tempCartId back to client as a cookie
                    context.Session[ShoppingCart.CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[ShoppingCart.CartSessionKey].ToString();
        }

    }
}