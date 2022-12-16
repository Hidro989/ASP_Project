using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareEbook_v1.Data;
using ShareEbook_v1.Models;
using ShareEbook_v1.ViewModels;
using ShareEbook_v1.Views.Account;
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
        public async Task<IActionResult> Edit(string username)
        {   
            if(username == null)
            {
                return NotFound();
            }
            var model = await _context.Accounts.FirstOrDefaultAsync(a => a.Username == username);
            if(model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: AccountController/Edit/5
        [HttpPost(Name = "Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(string username)
        {   
            if(string.IsNullOrEmpty(username))
            {
                return NotFound();
            }
            var accounttoUpdate = await _context.Accounts.FirstOrDefaultAsync(a => a.Username == username);

            if (await TryUpdateModelAsync<Account>(
                accounttoUpdate,
                "",
                a => a.Password))
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
            return View(accounttoUpdate);
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

        [HttpGet]
        public IActionResult ChangePassword(bool? currentPasswordError = false)
        {
            if (currentPasswordError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Mật khẩu hiện tại không chính xác";
            }
            ViewData["Username"] = HttpContext.Session.GetString("_Username");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Username == HttpContext.Session.GetString("_Username"));
                if(user.Password == model.CurrentPassword)
                {
                    user.Password = model.NewPassword;
                    try
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                        return View("ChangePasswordConfirmation", "Mật khẩu của bạn đã được thay đổi thành công");
                    }
                    catch (DbUpdateException)
                    {
                        ModelState.AddModelError("", "Không thể lưu thay đổi. Thử lại, nếu sự cố vấn tiếp tục vui lòng liên hệ quản trị viên");
                    }
                }
                else
                {
                    return RedirectToAction(nameof(ChangePassword), new { currentPasswordError = true });
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ActionName("Register")]
        public async Task<IActionResult> RegisterPost(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var ac = _context.Accounts.FirstOrDefault(a => a.Username == model.Username);
                if(ac != null)
                {
                    ViewData["ErrorMessage"] = "Tên người dùng đã tồn tại";
                    return View(model);
                }
                
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.Phonenumber,
                    Gender = model.Gender,
                    Birthday = model.Birthday
                };

                _context.Users.Add(user);
                _context.SaveChanges();
                int id = _context.Users.Max(u => u.Id);
                var account = new Account
                {
                    Username = model.Username,
                    Password = model.Password,
                    UserId = id,
                    User = null
                };

                try
                {
                    _context.Accounts.Add(account);
                    _context.SaveChanges();
                    return View("ChangePasswordConfirmation", "Đăng ký tài khoản thành công");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Không thể lưu thay đổi. Thử lại, nếu sự cố vấn tiếp tục vui lòng liên hệ quản trị viên");
                    _context.Users.Remove(_context.Users.FirstOrDefault(u => u.Id == id));
                    _context.SaveChanges();

                }
            }
            return View(model);
        }
    }
}
