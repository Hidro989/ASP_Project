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
    public class DeThiController : ControllerBase
    {
        private readonly TracNghiemContext _context;

        public DeThiController(TracNghiemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeThi>>> GetAll()
        {
            return Ok(await _context.DsDeThi.AsNoTracking().ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeThi>> GetById(int id)
        {
            var deThi = await _context.DsDeThi.FindAsync(id);
            if(deThi == null)
            {
                return NotFound();
            }

            return Ok(deThi);
        }

        [HttpPost]
        public async Task<ActionResult<DeThi>> Insert(DeThiVM model)
        {
            var deThi = new DeThi
            {
                TenDeThi = model.TenDeThi,
                MonThiID = model.MonThiID,
            };
            _context.DsDeThi.Add(deThi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),new {id = deThi.ID}, deThi);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DeThiVM model)
        {
            if(id != model.ID)
            {
                return BadRequest();
            }

            var deThi = await _context.DsDeThi.FindAsync(id);
            if(deThi == null)
            {
                return NotFound();
            }

            deThi.TenDeThi = model.TenDeThi;
            deThi.MonThiID = model.MonThiID;
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
            var deThi = await _context.DsDeThi.FindAsync(id);
            if(deThi == null)
            {
                return NotFound();
            }

            _context.DsDeThi.Remove(deThi);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
