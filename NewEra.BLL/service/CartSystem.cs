using NewEra.Domain.Interface;
using NewEra.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace NewEra.BLL
{
    public class CartSystem : IManageCartProduct
    {
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


        public Cart SaveOrder(List<Cart> CurrentCart, int userId)
        {
            // Code to process the order for the products in the cart
            return new Cart(); // Placeholder return statement
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