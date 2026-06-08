using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.ComponentModel.DataAnnotations;
using System.Net;
using NewEra.BLL;
using NewEra.Domain.Models;

namespace WebApplication1.Pages;

public class LoginModel : PageModel
{
    private readonly LoginService _loginService;
    
    public LoginModel(LoginService loginService)
    {
        _loginService = loginService;
     }


    [BindProperty(SupportsGet = true)]
    public string ReturnUrl { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }
     public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }   
    
    public void OnGet()
    {
        // The ReturnUrl is now automatically bound from the query string
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        User user = _loginService.Login(Input.Email, Input.Password);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }

        // Create claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, Input.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, char.ToUpper(user.Role[0]) + user.Role.Substring(1).ToLower()) 
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        // Sign in user
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
        {
            return LocalRedirect(ReturnUrl);
        }
        if(user.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            return RedirectToPage("/Privacy");
            
        }else if(user.Role.Equals("User", StringComparison.OrdinalIgnoreCase))
        {
            return RedirectToPage("/Index");
        }
        return Page();
    }
}