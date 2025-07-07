using TenderManagement.Application.Interfaces.Services;
using TenderManagement.Application.Models.Dtos;

using TenderManagement.Infrastructure.Interfaces.Repositories.Dapper;

namespace TenderManagement.Infrastructure.Services
{
    public class LookupService : ILookupService
    {
        private readonly ILookupReadRepository _lookupReadRepository;

        public LookupService(ILookupReadRepository lookupReadRepository)
        {
            _lookupReadRepository = lookupReadRepository;
        }

        public async Task<IEnumerable<LookupDto>> GetAllCategoriesAsync()
        {
            return await _lookupReadRepository.GetAllCategoriesAsync();
        }

        public async Task<IEnumerable<LookupDto>> GetAllStatusesAsync()
        {
            return await _lookupReadRepository.GetAllStatusesAsync();
        }
    }
}