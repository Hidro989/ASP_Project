using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.WebSockets;
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

        [HttpGet("Page")]
        public async Task<ActionResult<PagedList<DeThi>>> GetDeThis([FromQuery]PaginationParams @params)
        {
            var deThis = await PagedList<DeThi>.CreateAsync(_context.DsDeThi.OrderBy(d => d.ID), @params.PageNumber, @params.PageSize);

            var metadata = new
            {
                deThis.TotalCount,
                deThis.PageSize,
                deThis.CurrentPage,
                deThis.TotalPages,
                deThis.HasNext,
                deThis.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(deThis);

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

        [HttpGet("GetByMonThiId/{monThiID}")]
        public async Task<ActionResult<IEnumerable<DeThi>>> GetByMonThiId(int monThiID)
        {
            var deThis = await _context.DsDeThi.Where(d => d.MonThiID == monThiID).ToListAsync();
            if(deThis == null)
            {
                return NotFound();
            }

            return Ok(deThis);
        }

        [HttpGet("GetDeThiWithName")]
        public async Task<ActionResult<IEnumerable<DeThiWithNameVM>>> GetDeThiWithName()
        {
            var deThis = await _context.DsDeThi.Include(d => d.MonThi).AsNoTracking().ToListAsync();
            var listDeThiWithName = new List<DeThiWithNameVM>();

            for(int i = 0; i < deThis.Count; ++i)
            {
                DeThiWithNameVM item = new()
                {
                    ID = deThis[i].ID,
                    TenDeThi = deThis[i].TenDeThi,
                    SoLuongCauHoi = deThis[i].SoLuongCauHoi,
                    MonThiID = deThis[i].MonThiID,
                    TenMonThi = deThis[i].MonThi.TenMonThi
                };

                listDeThiWithName.Add(item);
            }

            return Ok(listDeThiWithName);
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
