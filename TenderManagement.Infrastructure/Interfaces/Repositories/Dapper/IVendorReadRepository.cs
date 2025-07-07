using TenderManagement.Application.Models.Dtos;

namespace TenderManagement.Infrastructure.Interfaces.Repositories.Dapper;

public interface IVendorReadRepository
{
    Task<IEnumerable<VendorDetailDto>> GetAllVendorsAsync();
    Task<VendorDetailDto> GetVendorByIdAsync(int id);
}