using NewEra.Domain.Interface;
using NewEra.Domain.Models;
using System.Collections.Generic;
using System.Linq;


namespace NewEra.BLL
{
    public class CartSystem : IManageCartProduct
    {
        private ICart _cartRepository;
        public CartSystem(ICart cartRepository)
         {
            this._cartRepository = cartRepository;
         }
        public void FinalizeOrder(List<Cart> currentCart, int userId, bool transactionSuccess)
        {
            if (transactionSuccess)
            {
                _cartRepository.SaveOrder(currentCart, userId);
                Console.WriteLine($"Order finalized for user {userId}.");
            }
            else
            {
                Console.WriteLine("Transaction failed. Order not saved.");
            }
        }

       public List<Cart> addProduct(List<Cart> current, Product product, int quantity, int userId)
        {
            if (current == null)
            {
                current = new List<Cart>();
                Console.WriteLine("Cart was empty, created new cart.");
            }

            var existingCartItem = current.FirstOrDefault(c => c.ProductName == product.Name);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
                existingCartItem.TotalPrice += product.Price * quantity;
                Console.WriteLine($"Updated {product.Name} in cart. New quantity: {existingCartItem.Quantity}, New total price: {existingCartItem.TotalPrice}");
            }
            else
            {
                var newCartItem = new Cart
                {
                    ProductName = product.Name,
                    Quantity = quantity,
                    TotalPrice = product.Price * quantity,
                    UserId = userId
                };

                current.Add(newCartItem);
                Console.WriteLine($"Added {product.Name} to cart. Quantity: {quantity}, Total price: {newCartItem.TotalPrice}");
            }

            return current;
        }


      

    

        public void deleteProduct(int id, List<Cart> current, int quantity)
        {
            // Code to delete a product from the cart by ID
            var cartItem = current.FirstOrDefault(c => c.Id == id);
            if (cartItem != null)
            {
                current.Remove(cartItem);
                Console.WriteLine($"Deleted {cartItem.ProductName} from cart.");
            }
            else
            {
                Console.WriteLine($"Product with ID {id} not found in cart.");
            }
        }
    }

}