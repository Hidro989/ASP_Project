using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareEbook_v1.Data;
using ShareEbook_v1.Models;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ShareEbook_v1.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        public AccountController(DataContext context)
        {
            _context = context;
        }

        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountController/Details/5
        public async Task<IActionResult> Details(string username)
        {
            var model = await _context.Accounts.Include(a => a.User).Where(a => a.Username == username).FirstOrDefaultAsync();
            return View(model);
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public async Task<IActionResult> Edit(string username, bool? passwordError = false)
        {   
            if(username == null)
            {
                return NotFound();
            }
            var model = await _context.Accounts.Include(a => a.User).Where(a => a.Username == username).FirstOrDefaultAsync();
            if(model == null)
            {
                return NotFound();
            }

            if (passwordError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Mật khẩu không chính xác!\nVui lòng thử lại!";
            }
            return View(model);
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string username)
        {   
            if(string.IsNullOrEmpty(username))
            {
                return NotFound();
            }
            var account = await _context.Accounts.Include(a => a.User).FirstOrDefaultAsync(a => a.Username == username);
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == account.UserId);

            if (await TryUpdateModelAsync<User>(
                userToUpdate,
                "",
                u => u.Name, u => u.Email, u => u.PhoneNumber, u=> u.Birthday, u=> u.Gender))
            {
                
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }catch(DbUpdateException)
                {
                    ModelState.AddModelError("", "Không thể lưu thay đổi" +
                        "Thử lại, Nếu sự cố vấn tiếp diễn hãy gặp quản trị viên hệ thống");
                }
            }
            return View(account);
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
