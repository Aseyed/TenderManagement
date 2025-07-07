using TenderManagement.Application.Models.Dtos;

namespace TenderManagement.Infrastructure.Interfaces.Repositories.Dapper;

public interface ITenderReadRepository
{
    Task<IEnumerable<TenderListDto>> GetAllTendersAsync();
    Task<TenderDetailDto> GetTenderByIdAsync(int id);
}