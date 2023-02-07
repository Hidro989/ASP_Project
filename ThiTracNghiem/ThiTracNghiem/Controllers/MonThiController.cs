using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ThiTracNghiem.Data;
using ThiTracNghiem.Models;
using ThiTracNghiem.Services;
using ThiTracNghiem.ViewModels;

namespace ThiTracNghiem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonThiController : ControllerBase
    {
        private readonly IMonThiRepository _repository;

        public MonThiController(IMonThiRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonThi>>> GetAll()
        {
            var dsMonThi = await _repository.GetAll();
            return Ok(dsMonThi);
        }

        [HttpGet("Page")]
        public async Task<ActionResult<IEnumerable<MonThi>>> GetMonThis([FromQuery] PaginationParams @params)
        {
            var monThis = await _repository.GetMonThis(@params);
            var metadata = new
            {
                monThis.TotalCount,
                monThis.PageSize,
                monThis.CurrentPage,
                monThis.TotalPages,
                monThis.HasNext,
                monThis.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(monThis);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MonThi>> GetById(int id)
        {
            var monThiItem = await _repository.GetById(id);
            if(monThiItem == null)
            {
                return NotFound();
            }

            return Ok(monThiItem);
        }

        [HttpPost]
        public async Task<ActionResult<MonThi>> PostMonThiItem(MonThiVM model)
        {
            var monThi = await _repository.Insert(model);
            return CreatedAtAction(nameof(GetById), new {id = monThi.ID}, monThi);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonThiItem(int id, MonThiVM model)
        {
            if(id != model.ID)
            {
                return BadRequest();
            }

            try
            {
                await _repository.Update(model);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonThiItem(int id)
        {
            try
            {
               await _repository.Delete(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
