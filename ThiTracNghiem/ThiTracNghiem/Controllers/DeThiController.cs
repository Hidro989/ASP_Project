﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.WebSockets;
using ThiTracNghiem.Data;
using ThiTracNghiem.Models;
using ThiTracNghiem.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            return Ok(await _context.DsDeThi.AsNoTracking().Include(d => d.MonThi).ToListAsync());
        }

        [HttpGet("Page")]
        public async Task<ActionResult<PagedList<DeThi>>> GetDeThis([FromQuery]PaginationParams @params)
        {
            IQueryable<DeThi> query = null;
            if (string.IsNullOrEmpty(@params.Text))
            {
                query = _context.DsDeThi.OrderBy(d => d.ID).AsNoTracking().Include(d => d.MonThi);
            }
            else
            {
                query = _context.DsDeThi.OrderBy(d => d.TenDeThi).AsNoTracking().Where(d => (d.MonThi.TenMonThi.ToLower().Contains(@params.Text.Trim().ToLower()) || d.TenDeThi.ToLower().Contains(@params.Text.Trim().ToLower()))).Include(d => d.MonThi);
            }
        
            var deThis = await PagedList<DeThi>.CreateAsync(query, @params.PageNumber, @params.PageSize);

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


        [HttpPost]
        public async Task<ActionResult<DeThi>> Insert(DeThiVM model)
        {
            var deThi = new DeThi
            {
                TenDeThi = model.TenDeThi,
                MonThiID = model.MonThiID,
            };          

            try
            {
                var monThi = await _context.Set<MonThi>().FirstOrDefaultAsync(m => m.ID == model.MonThiID);

                monThi.SoLuongDe +=  1;
                _context.DsDeThi.Add(deThi);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("Tạo đề thi thất bại");
            }
            

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
            var monThi = await _context.DsMonThi.FindAsync(deThi.MonThiID);
            monThi.SoLuongDe -= 1;
            _context.DsMonThi.Update(monThi);
            _context.DsDeThi.Remove(deThi);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
