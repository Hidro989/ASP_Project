using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using ShareEbook_v1.Data;
using ShareEbook_v1.Models;
using System.Linq;
using System.Collections.Generic;
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
        public async Task<IActionResult> Post([FromQuery] string type,[FromRoute] int? id)
        {
            string name = HttpContext.Session.GetString("_Username");
            ViewData["Username"] = name;
            ViewData["action"] = "Post";
            ViewData["Title"] = "Quản lý bài đăng";
            var posts = await _context.Posts.Include(p => p.DocumentInfor).Include(p => p.UserInfor).AsNoTracking().ToListAsync();
            if(id!= null){
                var postDetail = posts.Find(post => post.Id == id);
                return View("Detail",postDetail);
            }
            if(type== "approved"){
               posts = (from post in posts where post.Pending == false orderby post.DateSubmitted select post).ToList();
            }
            else if(type =="pending"){
                posts = (from post in posts where post.Pending == true orderby post.DateSubmitted select post).ToList();
            }
            if (!string.IsNullOrEmpty(name))
            {
                Account loginAccount = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Username == name);
                if (loginAccount.Type == TypeAccount.User)
                {
                    return NotFound();
                }

            }
            return View(posts);
        }
        public  IActionResult User(){
            ViewData["action"] = "User";
            ViewData["Title"] = "Quản lý người dùng";
            var users = _context.Accounts.Include(acc => acc.User).ToList();
            return View(users);
        }
        public IActionResult Stat(){
            ViewData["action"] = "Stat";
            ViewData["Title"] = "Thống kê";
            return View();
        }
    }
}
