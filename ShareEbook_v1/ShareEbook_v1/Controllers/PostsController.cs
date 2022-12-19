using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShareEbook_v1.Data;
using ShareEbook_v1.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ShareEbook_v1.Controllers
{
    public class PostsController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public PostsController(DataContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            webHostEnvironment = webHost;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Username"] = HttpContext.Session.GetString("_Username");
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.Include(p => p.DocumentInfor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["Username"] = HttpContext.Session.GetString("_Username");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            string username = HttpContext.Session.GetString("_Username");

            if (ModelState.IsValid)
            {
                string uniquePictureFileName = UploadedPicture(post);
                string uniqueFileName = UploadedFile(post);
                post.DocumentInfor.FileUrl = uniqueFileName;
                post.DocumentInfor.PictureUrl = uniquePictureFileName;
                var account = _context.Accounts.Where(a => a.Username == username).Include(a => a.User).AsNoTracking().FirstOrDefault();
                post.UserId = account.UserId;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return View("ConfirmMessage", "Đăng bài thành công,Index,Home");
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateSubmitted,Pending")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        private string UploadedPicture(Post post)
        {
            string uniqueFileName = null;
            if(post.DocumentInfor.Picture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + post.DocumentInfor.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    post.DocumentInfor.Picture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        private string UploadedFile(Post post)
        {
            string uniqueFileName = null;
            if (post.DocumentInfor.FileDocument != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "fileDocument");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + post.DocumentInfor.FileDocument.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    post.DocumentInfor.FileDocument.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
