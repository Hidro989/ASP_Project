using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
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

        [HttpGet("Page")]
        public async Task<ActionResult<PagedList<CauHoi>>> GetCauHoi([FromQuery] PaginationParams @params)
        {   

            IQueryable<CauHoi> query = null;
            if (string.IsNullOrEmpty(@params.Text))
            {
                query =  _context.DsCauHoi.OrderBy(c => c.ID).AsNoTracking().Include(c => c.DeThi);
            }
            else
            {
                var text = @params.Text.Trim().ToLower();
                query = _context.DsCauHoi.OrderBy(c => c.ID).AsNoTracking().Where(c => c.NoiDung.ToLower().Contains(text) || c.DeThi.TenDeThi.ToLower().Contains(text)).Include(c => c.DeThi);
            }

            var cauHois = await PagedList<CauHoi>.CreateAsync(query, @params.PageNumber, @params.PageSize);

            var metadata = new
            {
                cauHois.TotalCount,
                cauHois.PageSize,
                cauHois.CurrentPage,
                cauHois.TotalPages,
                cauHois.HasNext,
                cauHois.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(cauHois);
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

        [HttpGet("GetByDeThiId/{deThiId}")]
        public async Task<ActionResult<IEnumerable<CauHoi>>> GetByDeThiId(int deThiId)
        {
            var cauHois = await _context.DsCauHoi.Where(c => c.DeThiID == deThiId).ToListAsync();
            
            if(cauHois == null)
            {
                return NotFound();
            }
            if(cauHois.Count <= 50)
            {
                return Ok(cauHois);
            }
            else
            {
                List<CauHoi> rdCauhois = new();
                List<int> listNumber = new();
                Random rnd = new Random();
                
                for(int i = 0; i < 50; ++i)
                {
                    int j = rnd.Next(cauHois.Count);
                    if(listNumber.Count == 0)
                    {
                        listNumber.Add(j);
                        rdCauhois.Add(cauHois[j]);
                    }
                    else
                    {
                        bool check = listNumber.Contains(j);
                        while (check)
                        {
                            j = rnd.Next(cauHois.Count);
                            check = listNumber.Contains(j);
                        }
                        listNumber.Add(j);
                        rdCauhois.Add(cauHois[j]);
                    }
                    

                }
                return Ok(rdCauhois);
            }
            
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
            try
            {
                var deThis = await _context.DsDeThi.FindAsync(model.DethiID);
                deThis.SoLuongCauHoi += 1;

                _context.DsCauHoi.Add(cauHoi);
                await _context.SaveChangesAsync();
            }
            catch
            {
                BadRequest("Tạo câu hỏi thất bại");
            }
            
            return CreatedAtAction(nameof(GetById), new { id = cauHoi.ID }, cauHoi);
        }

        [HttpPost(nameof(PostListQuestion))]
        public async Task<IActionResult> PostListQuestion(List<CauHoiVM> model)
        {
            if(model == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            foreach(var ques in model)
            {
                var newQues = new CauHoi
                {
                    NoiDung = ques.NoiDung,
                    A = ques.A,
                    B = ques.B,
                    C = ques.C,
                    D = ques.D,
                    DapAnDung = ques.DapAnDung,
                    DeThiID = ques.DethiID
                };

                _context.DsCauHoi.Add(newQues);
            }

            try
            {
                var deThis = await _context.DsDeThi.FindAsync(model[0].DethiID);
                deThis.SoLuongCauHoi += model.Count;
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
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
            var deThi = await _context.DsDeThi.FindAsync(cauHoi.DeThiID);
            deThi.SoLuongCauHoi -= 1;
            _context.DsDeThi.Update(deThi);
            _context.DsCauHoi.Remove(cauHoi);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
