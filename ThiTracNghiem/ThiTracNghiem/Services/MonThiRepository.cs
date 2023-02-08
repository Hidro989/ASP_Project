using Microsoft.EntityFrameworkCore;
using System.Linq;
using ThiTracNghiem.Data;
using ThiTracNghiem.Models;
using ThiTracNghiem.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace ThiTracNghiem.Services
{
    public class MonThiRepository : IMonThiRepository
    {
        private readonly TracNghiemContext _context;

        public MonThiRepository(TracNghiemContext context)
        {
            _context = context;
        }


        private static MonThiVM ItemToMonThiVM(MonThi item) =>
            new MonThiVM
            {
                ID = item.ID,
                TenMonThi = item.TenMonThi
            };

        public async Task Delete(int id)
        {
            var monThi = await _context.DsMonThi.SingleOrDefaultAsync(m => m.ID == id);
            if(monThi != null)
            {
                _context.Remove(monThi);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<MonThi>> GetAll()
        {
            var dsMonThi = await _context.DsMonThi.AsNoTracking().ToListAsync();
            return dsMonThi;
        }

        public async Task<MonThi> GetById(int id)
        {
            var monThi = await _context.DsMonThi.AsNoTracking().SingleOrDefaultAsync(m => m.ID == id);
            if(monThi != null)
            {
                return monThi;
            }

            return null;
        }

        public async Task<PagedList<MonThi>> GetMonThis(PaginationParams @params)
        {

            IQueryable<MonThi> monThis= null;
            if ( string.IsNullOrEmpty(@params.Text))
            {
                monThis = _context.DsMonThi.OrderBy(m => m.ID).AsNoTracking();
            }
            else
            {
                
                monThis = _context.DsMonThi.OrderBy(m => m.TenMonThi).Where(m => m.TenMonThi.ToLower().Contains(@params.Text.Trim().ToLower())).AsNoTracking();
            }


            return await PagedList<MonThi>.CreateAsync(monThis, @params.PageNumber, @params.PageSize);
        }


        public async Task<MonThi> Insert(MonThiVM monThiVM)
        {
            var monThi = new MonThi
            {
                TenMonThi = monThiVM.TenMonThi
            };
            _context.Add(monThi);
            await _context.SaveChangesAsync();

            return monThi;
        }

        public async Task Update(MonThiVM monThiVM)
        {
            var monThi = await _context.DsMonThi.SingleOrDefaultAsync(m => m.ID == monThiVM.ID);
            if(monThi != null)
            {
                monThi.TenMonThi = monThiVM.TenMonThi;
                await _context.SaveChangesAsync();
            }
            
        }

        
    }
}
