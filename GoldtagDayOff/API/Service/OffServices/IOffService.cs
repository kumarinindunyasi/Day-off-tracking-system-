using API.Entities;
using API.Models;

namespace API.Service.IzinServices
{
    public interface IOffService
    {
        Task<string> CreateIzin(OffModel model);
        Task<string> DeleteIzin(int Id);
        Task<List<Off>> GetAllIzin();
        Task<Off> GetByIdIzin(int Id);
        Task<Off?> UpdateApproveIzin(OffApproveModel izin);
        Task<string> UpdateIzin(Off izin);
    }
}
