using ThiTracNghiem.Models;
using ThiTracNghiem.ViewModels;

namespace ThiTracNghiem.Services
{
    public interface IMonThiRepository
    {
        Task<List<MonThi>> GetAll();
        Task<PagedList<MonThi>> GetMonThis(PaginationParams @params);
        Task<MonThi> GetById(int id);
        Task<MonThi> Insert(MonThiVM monThiVM);
        Task Update(MonThiVM monThiVM);
        Task Delete(int id);
    }
}
