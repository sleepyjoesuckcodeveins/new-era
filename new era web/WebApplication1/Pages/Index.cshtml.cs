using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEra.Domain.Interface;
using NewEra.Domain.Models;
using NewEra.BLL;
using NewEra.Dal;


namespace WebApplication1.Pages;

public class IndexModel : PageModel
{
     private NeweraProductService productService;
     public List<Product> Products { get; set; }

    [BindProperty(SupportsGet = true)]
    public string searchterm { get; set; } = string.Empty;


    public IndexModel(NeweraProductService productService)
    {
        this.productService = productService;
    }
   

    public void OnGet()
    {
            Products = string.IsNullOrWhiteSpace(searchterm)
            ? productService.GetAllProducts()
            : productService.SearchProduct(searchterm);
    }


    public void OnPost()
    {
        Products = productService.SearchProduct(searchterm ?? string.Empty);
    }
}
