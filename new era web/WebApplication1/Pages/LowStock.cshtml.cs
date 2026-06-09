using Microsoft.AspNetCore.Mvc.RazorPages;
using NewEra.BLL;
using NewEra.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Pages
{
    [Authorize(Roles = "Admin")]
    public class LowStockModel : PageModel
    {
        private readonly Adminservice _adminService;

        public List<Product> LowStockProducts { get; private set; }

        public LowStockModel(Adminservice adminService)
        {
            _adminService = adminService;
        }

        public void OnGet()
        {
            LowStockProducts = _adminService.getLowestStockProducts();
        }
    }
}
