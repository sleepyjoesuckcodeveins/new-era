using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEra.BLL;
using NewEra.Domain.Models;

namespace WebApplication1.Pages;

[Authorize(Roles = "Admin")]
public class PrivacyModel : PageModel
{
    private readonly Adminservice _adminService;

    public List<Product> AllProducts { get; private set; }
    [BindProperty]
    public Product NewProduct { get; set; }

    public PrivacyModel(Adminservice adminService)
    {
        _adminService = adminService;
       
    }

    public void OnGet()
    {
        AllProducts = _adminService.getAllProducts();
    }

    public IActionResult OnPostAddProduct()
    {
        if (NewProduct.Quantity < 0 || NewProduct.Price < 0)
        {
            ModelState.AddModelError(string.Empty, "Quantity and Price must be non-negative.");
            return Page();
        }
        if (ModelState.IsValid)
        {
            _adminService.addProduct(NewProduct);
            return RedirectToPage();
        }
        return Page();
    }
    
    public IActionResult OnPostDeleteProduct(int productId)
    {
        _adminService.deleteProduct(productId);
        return RedirectToPage();
    }
    public IActionResult OnPostUpdateStock(int productId, int newStock)
    {
        _adminService.updateStock(productId, newStock);
        return RedirectToPage();
    }

    public void OnPost()
    {
        // Handle form submission for admin services here
    }
}

