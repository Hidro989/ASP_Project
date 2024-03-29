﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.WebSockets;
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


        [HttpGet("Page")]
        public async Task<ActionResult<PagedList<MaThi>>> GetMaThis([FromQuery] PaginationParams @params)
        {
            IQueryable<MaThi> query = null;
            if (string.IsNullOrEmpty(@params.Text))
            {
                query = _context.DsMaThi.AsNoTracking();
            }
            else
            {
                query = _context.DsMaThi.AsNoTracking().Where(ma => ma.SLSD == Convert.ToInt32(@params.Text));
            }

            var maThis = await PagedList<MaThi>.CreateAsync(query, @params.PageNumber, @params.PageSize);

            var metadata = new
            {
                maThis.TotalCount,
                maThis.PageSize,
                maThis.CurrentPage,
                maThis.TotalPages,
                maThis.HasNext,
                maThis.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(maThis);
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
                    SLSD = 5
                };

                _context.DsMaThi.Add(maThi);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { ma = maThi.Ma }, maThi);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut("{ma}")]
        public async Task<IActionResult> Update(string ma)
        {
            var maThi = await _context.DsMaThi.FindAsync(ma);
            if(maThi == null)
            {
                return NotFound();
            }

            if(maThi.SLSD > 1)
            {
                maThi.SLSD = maThi.SLSD - 1;
            }
            else
            {
                _context.DsMaThi.Remove(maThi);
            }

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{ma}")]
        public async Task<IActionResult> Delete(string ma)
        {
            var maThis = await _context.DsMaThi.FindAsync(ma);
            if(maThis == null)
            {
                return NotFound();
            }

            _context.DsMaThi.Remove(maThis);
            await _context.SaveChangesAsync();
            return NoContent();

        }



    }
}


