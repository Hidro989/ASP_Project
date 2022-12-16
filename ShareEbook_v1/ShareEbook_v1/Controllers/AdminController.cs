using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using ShareEbook_v1.Data;
using ShareEbook_v1.Models;
using System.Threading.Tasks;

namespace ShareEbook_v1.Controllers
{
    public class AdminController : Controller
    {
        private readonly DataContext _context;
        public AdminController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            string name = HttpContext.Session.GetString("_Username");
            ViewData["Username"] = name;
            if (!string.IsNullOrEmpty(name))
            {
                Account loginAccount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Username == name);
                if (loginAccount.Type == TypeAccount.User)
                {
                    return NotFound();
                }

            }
            return View();
        }
    }
}
