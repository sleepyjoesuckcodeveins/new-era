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

    public IndexModel()
    {
        string connectionString ="Data Source=mssqlstud.fhict.local;Persist Security Info=True;User ID=dbi578294_newworld;Password=newworld;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;";
        IProduct product = new NewEraProducts(connectionString);
        productService = new NeweraProductService(product);
        
    }
    public void OnGet()
    {
        productService.GetAllProducts();
        Products = productService.GetAllProducts();
    }


    public void OnPost()
    {

    }
}
