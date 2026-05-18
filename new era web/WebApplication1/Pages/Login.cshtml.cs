using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEra.Domain.Interface;
using NewEra.BLL;

namespace WebApplication1.Pages;

public class LoginModel : PageModel
{
    private readonly LoginService _loginService;
    public LoginModel(LoginService loginService)
    {
        _loginService = loginService;
     }


    [BindProperty]
    public InputModel Input { get; set; }
     public class InputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }   
    
    public void OnGet()
    {
    }
    public void OnPost()
    {
        // Handle login logic here
    }
}