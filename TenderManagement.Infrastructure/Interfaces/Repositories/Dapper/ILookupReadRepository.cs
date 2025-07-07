using TenderManagement.Application.Models.Dtos;

namespace TenderManagement.Infrastructure.Interfaces.Repositories.Dapper;

public interface ILookupReadRepository
{
    Task<IEnumerable<LookupDto>> GetAllCategoriesAsync();
    Task<IEnumerable<LookupDto>> GetAllStatusesAsync();
    Task<LookupDto> GetCategoryByIdAsync(int id);
    Task<LookupDto> GetStatusByIdAsync(int id);
}