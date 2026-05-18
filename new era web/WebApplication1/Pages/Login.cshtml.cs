using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            public string Email { get; set; }
            public string Password { get; set; }
        }   
    
    public void OnGet()
    {
    }
  public async Task<IActionResult> OnPostAsync() // Renamed to OnPostAsync for clarity
{
    if (!ModelState.IsValid)
    {
        return Page();
    }

    bool loginSuccess = _loginService.Login(Input.Email, Input.Password);

    if (!loginSuccess)
    {
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }

    // Create claims
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, Input.Email),
        // Add the role claim here
        new Claim(ClaimTypes.Role, "User") 
    };

    var claimsIdentity = new ClaimsIdentity(
        claims, CookieAuthenticationDefaults.AuthenticationScheme);

    

    // Sign in user
    await HttpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(claimsIdentity));

    return RedirectToPage("/Index");
}

}