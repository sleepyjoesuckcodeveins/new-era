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

    [BindProperty]
    public string searchterm { get; set; }


    public IndexModel(NeweraProductService productService)
    {
        this.productService = productService;
    }
    public IndexModel()
    {
        searchterm = string.Empty;
    }

    public void OnGet()
    {
        Products = productService.GetAllProducts();
    }


    public void OnPost()
    {
        Products = productService.SearchProduct(searchterm ?? string.Empty);
    }
}
