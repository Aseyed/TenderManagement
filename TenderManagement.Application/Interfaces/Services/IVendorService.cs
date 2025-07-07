using TenderManagement.Application.Models.Dtos;
using TenderManagement.Domain.Entities;

namespace TenderManagement.Application.Interfaces.Services;

public interface IVendorService
{
    Task<IEnumerable<VendorDetailDto>> GetAllVendorsAsync();
    Task<VendorDetailDto> GetVendorByIdAsync(int id);
    Task<Vendor> CreateVendorAsync(Vendor vendor);
    Task UpdateVendorAsync(Vendor vendor);
    Task DeleteVendorAsync(int id);
}
