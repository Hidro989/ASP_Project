using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using ThiTracNghiem.Data;
using ThiTracNghiem.Models;
using ThiTracNghiem.ViewModels;

namespace ThiTracNghiem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaThiController : ControllerBase
    {
        private readonly TracNghiemContext _context;

        public MaThiController(TracNghiemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaThi>>> GetAll()
        {
            return Ok(await _context.DsMaThi.AsNoTracking().ToListAsync());
        }

        [HttpGet("{ma}")]
        public async Task<ActionResult<MaThi>> GetById(string ma)
        {
            var maThi = await _context.DsMaThi.AsNoTracking().FirstOrDefaultAsync(m => m.Ma == ma);
            if(maThi == null)
            {
                return NotFound();
            }

            return Ok(maThi);
        }

        [HttpPost]
        public async Task<ActionResult<MaThi>> Create()
        {
            var check = false;
            int num = -1;
            while (check == false)
            {
                Random rd = new Random();
                num = rd.Next(100000, 1000000);

                var item = _context.DsMaThi.Where(m => m.Ma == num.ToString()).FirstOrDefault();
                if(item == null)
                {
                    check = true;
                }
            }

            if(num != -1 && check == true)
            {
                var maThi = new MaThi
                {
                    Ma = num.ToString(),
                    SLSD = 10
                };

                _context.DsMaThi.Add(maThi);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { ma = maThi.Ma }, maThi);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
