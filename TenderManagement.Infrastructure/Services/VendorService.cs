using TenderManagement.Application.Interfaces.Repositories;
using TenderManagement.Application.Interfaces.Services;
using TenderManagement.Application.Models.Dtos;
using TenderManagement.Domain.Entities;
using TenderManagement.Infrastructure.Data;
using TenderManagement.Infrastructure.Interfaces.Repositories.Dapper;

namespace TenderManagement.Infrastructure.Services
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IVendorReadRepository _vendorReadRepository;
        private readonly ApplicationDbContext _dbContext;

        public VendorService(IVendorRepository vendorRepository, IVendorReadRepository vendorReadRepository, ApplicationDbContext dbContext)
        {
            _vendorRepository = vendorRepository;
            _vendorReadRepository = vendorReadRepository;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<VendorDetailDto>> GetAllVendorsAsync()
        {
            return await _vendorReadRepository.GetAllVendorsAsync();
        }

        public async Task<VendorDetailDto> GetVendorByIdAsync(int id)
        {
            return await _vendorReadRepository.GetVendorByIdAsync(id);
        }

        public async Task<Vendor> CreateVendorAsync(Vendor vendor)
        {
            await _vendorRepository.AddAsync(vendor);
            await _dbContext.SaveChangesAsync();
            return vendor;
        }

        public async Task UpdateVendorAsync(Vendor vendor)
        {
            var existingVendor = await _vendorRepository.GetByIdAsync(vendor.Id);
            if (existingVendor == null)
            {
                throw new KeyNotFoundException("Vendor not found.");
            }

            existingVendor.Name = vendor.Name;
            existingVendor.ContactInfo = vendor.ContactInfo;

            await _vendorRepository.UpdateAsync(existingVendor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteVendorAsync(int id)
        {
            var vendor = await _vendorRepository.GetByIdAsync(id);
            if (vendor == null)
            {
                throw new KeyNotFoundException("Vendor not found.");
            }
            await _vendorRepository.DeleteAsync(vendor);
            await _dbContext.SaveChangesAsync();
        }
    }
}