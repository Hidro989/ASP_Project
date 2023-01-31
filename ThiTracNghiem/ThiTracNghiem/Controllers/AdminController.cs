using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThiTracNghiem.Data;

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

        [HttpGet("{username}/{password}")]
        public bool Check(string username, string password)
        {
            var admin = _context.Admins.Where(a => (a.UserName == username && a.Password == password)).FirstOrDefault();
            if(admin == null)
            {
                return false;
            }

            return true;
        }

    }
}
