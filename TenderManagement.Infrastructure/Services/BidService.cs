using TenderManagement.Application.Interfaces.Repositories;
using TenderManagement.Application.Interfaces.Services;
using TenderManagement.Application.Models.Dtos;
using TenderManagement.Domain.Entities;
using TenderManagement.Infrastructure.Data;
using TenderManagement.Infrastructure.Interfaces.Repositories.Dapper;

namespace TenderManagement.Infrastructure.Services
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        private readonly IBidReadRepository _bidReadRepository;
        private readonly ITenderRepository _tenderRepository; 
        private readonly IVendorRepository _vendorRepository; 
        private readonly IStatusRepository _statusRepository; 
        private readonly ApplicationDbContext _dbContext;

        public BidService(IBidRepository bidRepository,
                          IBidReadRepository bidReadRepository,
                          ITenderRepository tenderRepository,
                          IVendorRepository vendorRepository,
                          IStatusRepository statusRepository,
                          ApplicationDbContext dbContext)
        {
            _bidRepository = bidRepository;
            _bidReadRepository = bidReadRepository;
            _tenderRepository = tenderRepository;
            _vendorRepository = vendorRepository;
            _statusRepository = statusRepository;
            _dbContext = dbContext;
        }

        public async Task<Bid> CreateBidAsync(Bid bid)
        {
            var tender = await _tenderRepository.GetByIdAsync(bid.TenderId);
            if (tender == null || tender.StatusId != (await _statusRepository.GetStatusByNameAsync("Open")).Id)
            {
                throw new ArgumentException("Tender does not exist or is not open for bids.");
            }

            var vendor = await _vendorRepository.GetByIdAsync(bid.VendorId);
            if (vendor == null)
            {
                throw new ArgumentException("Invalid VendorId.");
            }

            var pendingStatus = await _statusRepository.GetStatusByNameAsync("Pending");
            if (pendingStatus == null)
            {
                throw new InvalidOperationException("Pending status not found in database.");
            }
            bid.StatusId = pendingStatus.Id;
            bid.SubmissionDate = DateTime.Now;

            await _bidRepository.AddAsync(bid);
            await _dbContext.SaveChangesAsync();
            return bid;
        }

        public async Task UpdateBidStatusAsync(int bidId, int newStatusId)
        {
            var bid = await _bidRepository.GetByIdAsync(bidId);
            if (bid == null)
            {
                throw new KeyNotFoundException("Bid not found.");
            }

            var newStatus = await _statusRepository.GetByIdAsync(newStatusId);
            if (newStatus == null)
            {
                throw new ArgumentException("Invalid StatusId.");
            }
          
            var currentStatus = await _statusRepository.GetByIdAsync(bid.StatusId);
            if (currentStatus.Name == "Approved" || currentStatus.Name == "Rejected")
            {
                throw new InvalidOperationException("Cannot change status of an already approved or rejected bid.");
            }

            if (newStatus.Name != "Approved" && newStatus.Name != "Rejected")
            {
                throw new ArgumentException("Bid status can only be updated to 'Approved' or 'Rejected'.");
            }


            bid.StatusId = newStatusId;
            await _bidRepository.UpdateAsync(bid);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BidDetailDto> GetBidByIdAsync(int id)
        {
            return await _bidReadRepository.GetBidByIdAsync(id);
        }

        public async Task<IEnumerable<BidDetailDto>> GetBidsByTenderIdAsync(int tenderId)
        {
            return await _bidReadRepository.GetBidsByTenderIdAsync(tenderId);
        }

        public async Task<IEnumerable<BidDetailDto>> GetBidsByVendorIdAsync(int vendorId)
        {
            return await _bidReadRepository.GetBidsByVendorIdAsync(vendorId);
        }
    }
}