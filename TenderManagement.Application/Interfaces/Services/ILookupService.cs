using TenderManagement.Application.Models.Dtos;

namespace TenderManagement.Application.Interfaces.Services;

public interface ILookupService
{
    Task<IEnumerable<LookupDto>> GetAllCategoriesAsync();
    Task<IEnumerable<LookupDto>> GetAllStatusesAsync();
}
