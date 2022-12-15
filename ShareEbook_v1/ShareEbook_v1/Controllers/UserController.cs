using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareEbook_v1.Data;
using ShareEbook_v1.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ShareEbook_v1.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }


        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(string username)
        {
            if (username == null)
            {
                return NotFound();
            }
            var account = await  _context.Accounts.Include(a => a.User).Where(a => a.Username == username).AsNoTracking().FirstOrDefaultAsync();
            var model = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == account.UserId);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(userToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<User>(
                userToUpdate,
                "",
                u => u.Name, u => u.Email, u => u.PhoneNumber, u => u.Birthday, u => u.Gender))
            {

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Không thể lưu thay đổi" +
                        "Thử lại, Nếu sự cố vấn tiếp diễn hãy gặp quản trị viên hệ thống");
                }
            }

            return View(userToUpdate);
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
