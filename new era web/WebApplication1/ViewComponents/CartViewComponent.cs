using Microsoft.AspNetCore.Mvc;
using NewEra.Domain.Models;
using WebApplication1.Helpers;

namespace WebApplication1.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<Cart>>("Cart") ?? new List<Cart>();
            return View(cart);
        }
    }
}
