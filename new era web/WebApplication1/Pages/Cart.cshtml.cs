using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEra.BLL;
using NewEra.Domain.Interface;
using NewEra.Domain.Models;
using WebApplication1.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace WebApplication1.Pages
{
    public class CartModel : PageModel
    {
        private readonly IManageCartProduct _cartService;
        private readonly NeweraProductService _productService;

        public CartModel(IManageCartProduct cartService, NeweraProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        public List<Cart> Cart { get; set; } = new List<Cart>();

        public void OnGet()
        {
            Cart = HttpContext.Session.Get<List<Cart>>("Cart") ?? new List<Cart>();
        }

        public IActionResult OnPostAddToCart(int productId, int quantity = 1)
        {
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.Get<List<Cart>>("Cart") ?? new List<Cart>();
            
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Challenge(); 
            }

            cart = _cartService.addProduct(cart, product, quantity, userId);

            HttpContext.Session.Set("Cart", cart);

            return RedirectToPage("/Index");
        }

        // FIX: Use productName to identify the item to remove
        public IActionResult OnPostRemoveFromCart(string productName)
        {
            var cart = HttpContext.Session.Get<List<Cart>>("Cart") ?? new List<Cart>();

            var itemToRemove = cart.FirstOrDefault(c => c.ProductName == productName);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.Set("Cart", cart);
            }

            return RedirectToPage();
        }

        // FIX: Use productName to identify the item to update
        public IActionResult OnPostUpdateQuantity(string productName, int quantity)
        {
            var cart = HttpContext.Session.Get<List<Cart>>("Cart") ?? new List<Cart>();
            var itemToUpdate = cart.FirstOrDefault(c => c.ProductName == productName);

            if (itemToUpdate != null)
            {
                if (quantity > 0)
                {
                    itemToUpdate.Quantity = quantity;
                    // It's also good practice to recalculate the total price here
                }
                else
                {
                    // If quantity is 0 or less, remove the item
                    cart.Remove(itemToUpdate);
                }
                HttpContext.Session.Set("Cart", cart);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostCheckout()
        {
            var cart = HttpContext.Session.Get<List<Cart>>("Cart") ?? new List<Cart>();
            if (!cart.Any())
            {
                return RedirectToPage();
            }

            // FIX: Use the same safe method to get the user ID
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Challenge();
            }
            
            bool transactionSuccess = true;

            if (transactionSuccess)
            {
                // FIX: Use the correct method name 'FinalizeOrder'
                _cartService.FinalizeOrder(cart, userId, transactionSuccess);
                HttpContext.Session.Remove("Cart");
            }

            return RedirectToPage("OrderConfirmation");
        }
    }
}