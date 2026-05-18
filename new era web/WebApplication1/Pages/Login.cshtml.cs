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
    public async Task<IActionResult> OnPost()
    {
        // Handle login logic here
        if (ModelState.IsValid)
        {
            bool loginSuccess = _loginService.Login(Input.Username, Input.Password);
            if (loginSuccess)
            {
                // Redirect to a secure page or dashboard
                return RedirectToPage("/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }
        return Page();
    }
}