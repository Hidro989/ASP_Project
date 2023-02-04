using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThiTracNghiem.Data;
using ThiTracNghiem.Models;

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
        public bool Check(Admin model)
        {
            var admin = _context.Admins.Where(a => (a.UserName == model.UserName && a.Password == model.Password)).FirstOrDefault();
            if (admin == null)
            {
                return false;
            }

            return true;
        }


    }
}
