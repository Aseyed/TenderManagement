using TenderManagement.Application.Models.Dtos;

namespace TenderManagement.Infrastructure.Interfaces.Repositories.Dapper;

public interface IBidReadRepository
{
    Task<BidDetailDto> GetBidByIdAsync(int id);
    Task<IEnumerable<BidDetailDto>> GetBidsByTenderIdAsync(int tenderId);
    Task<IEnumerable<BidDetailDto>> GetBidsByVendorIdAsync(int vendorId);
}