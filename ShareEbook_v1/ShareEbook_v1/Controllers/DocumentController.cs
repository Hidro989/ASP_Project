using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareEbook_v1.Data;
using ShareEbook_v1.Models;
using System.IO;
using System.Linq;

namespace ShareEbook_v1.Controllers
{
    public class DocumentController : Controller
    {   

        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly DataContext _context;

        public DocumentController(IWebHostEnvironment webHostEnvironment, DataContext context)
        {
            this.webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        [HttpGet("Download")]
        public IActionResult Download(int? id)
        {   

            var document = _context.Documents.Where(d => d.Id == id).FirstOrDefault();
            var filepath = Path.Combine(webHostEnvironment.WebRootPath, "fileDocument", document.FileUrl);
            return File(System.IO.File.ReadAllBytes(filepath), "application/docx", $"{document.Name}{Path.GetExtension(document.FileUrl)}");
        }

        // GET: DocumentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DocumentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DocumentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentController/Create
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

        // GET: DocumentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DocumentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: DocumentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DocumentController/Delete/5
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
