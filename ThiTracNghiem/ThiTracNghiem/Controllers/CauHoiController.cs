using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using ThiTracNghiem.Data;
using ThiTracNghiem.Models;
using ThiTracNghiem.ViewModels;

namespace ThiTracNghiem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauHoiController : ControllerBase
    {
        private readonly TracNghiemContext _context;

        public CauHoiController(TracNghiemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CauHoi>>> GetAll()
        {
            return Ok(await _context.DsCauHoi.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CauHoi>> GetById(int id)
        {
            var cauHoi = await _context.DsCauHoi.FindAsync(id);

            if (cauHoi == null)
            {
                return NotFound();
            }

            return Ok(cauHoi);
        }

        [HttpGet("/GetByDeThiId/{deThiId}")]
        public async Task<ActionResult<IEnumerable<CauHoi>>> GetByDeThiId(int deThiId)
        {
            var cauHois = await _context.DsCauHoi.Where(c => c.DeThiID == deThiId).ToListAsync();
            if(cauHois == null)
            {
                return NotFound();
            }
            return Ok(cauHois);
        }


        [HttpPost]
        public async Task<ActionResult<CauHoi>> Insert(CauHoiVM model)
        {
            var cauHoi = new CauHoi
            {
                NoiDung = model.NoiDung,
                A = model.A,
                B = model.B,
                C = model.C,
                D = model.D,
                DapAnDung = model.DapAnDung,
                DeThiID = model.DethiID
            };
            _context.DsCauHoi.Add(cauHoi);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = cauHoi.ID }, cauHoi);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CauHoiVM model)
        {
            if(id != model.ID)
            {
                return BadRequest();
            }

            var cauHoi = await _context.DsCauHoi.FindAsync(id);
            if(cauHoi == null)
            {
                return NotFound();
            }
            cauHoi.NoiDung = model.NoiDung;
            cauHoi.A = model.A;
            cauHoi.B = model.B;
            cauHoi.C = model.C;
            cauHoi.D = model.D;
            cauHoi.DapAnDung = model.DapAnDung;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cauHoi = await _context.DsCauHoi.FindAsync(id);
            if(cauHoi == null)
            {
                return NotFound();
            }

            _context.DsCauHoi.Remove(cauHoi);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
