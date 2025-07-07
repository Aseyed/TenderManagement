using TenderManagement.Application.Models.Dtos;
using TenderManagement.Domain.Entities;

namespace TenderManagement.Application.Interfaces.Services;

public interface ITenderService
{
    Task<IEnumerable<TenderListDto>> GetAllTendersAsync();
    Task<TenderDetailDto> GetTenderByIdAsync(int id);
    Task<Tender> CreateTenderAsync(Tender tender);
    Task UpdateTenderAsync(Tender tender);
    Task DeleteTenderAsync(int id);
}
