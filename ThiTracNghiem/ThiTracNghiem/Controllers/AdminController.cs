using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThiTracNghiem.Data;
using ThiTracNghiem.Models;
using ThiTracNghiem.ViewModels;

namespace ThiTracNghiem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly TracNghiemContext _context;

        public AdminController (TracNghiemContext context)
        {
            _context = context;
        }

        [HttpPost]
        public bool Check(QuanLy model)
        {
            var admin = _context.QuanLys.Where(a => (a.UserName == model.UserName && a.Password == model.Password)).FirstOrDefault();
            if (admin == null)
            {
                return false;
            }

            return true;
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] QuanLyVM model)
        {
            var admin = await _context.QuanLys.FirstOrDefaultAsync(a => (a.UserName == model.username && a.Password == model.password));
            if(admin == null)
            {
                return NotFound();
            }
            admin.Password = model.newPassword;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        

    }
}
