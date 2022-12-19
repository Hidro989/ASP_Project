using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShareEbook_v1.Data;
using ShareEbook_v1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace ShareEbook_v1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private const string SessionKeyUser = "_Username";
        private const string SessionKeyPass = "_Password";

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {   
            var model =  await _context.Posts.Where(p => p.Pending == false).Include(p => p.DocumentInfor).AsNoTracking().ToListAsync();
            string name = HttpContext.Session.GetString(SessionKeyUser);
            ViewData["Username"] = name;
            if(!string.IsNullOrEmpty(name))
            {
                Account loginAccount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Username == name);
                if(loginAccount.Type == TypeAccount.Admin)
                {
                    return NotFound();
                }

            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        public IActionResult LoginPost(string user, string password)
        {
            Account account = _context.Accounts.Where(a => (a.Username == user && a.Password == password)).FirstOrDefault();
            if(account != null)
            {
                HttpContext.Session.SetString(SessionKeyUser, account.Username);
                if (account.Type == TypeAccount.Admin)
                {
                    return RedirectToAction("Post","Admin");
                }
                else
                {
                    
                    return RedirectToAction("Index","Home");
                }
                
            }
            ViewData["error"] = "Thông tin tài khoản hoặc mật khẩu không chính xác";
            return View();
        }

        public IActionResult Logout()
        {   
            HttpContext.Session.Remove(SessionKeyUser);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
