using TenderManagement.Application.Models.Dtos;
using TenderManagement.Domain.Entities;

namespace TenderManagement.Application.Interfaces.Services;

public interface IBidService
{
    Task<Bid> CreateBidAsync(Bid bid);
    Task UpdateBidStatusAsync(int bidId, int newStatusId);
    Task<BidDetailDto> GetBidByIdAsync(int id);
    Task<IEnumerable<BidDetailDto>> GetBidsByTenderIdAsync(int tenderId); 
    Task<IEnumerable<BidDetailDto>> GetBidsByVendorIdAsync(int vendorId); 
}
