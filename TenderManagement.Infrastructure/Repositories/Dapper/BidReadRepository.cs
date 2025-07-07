using Dapper;
using TenderManagement.Application.Models.Dtos;
using TenderManagement.Infrastructure.Data;
using TenderManagement.Infrastructure.Interfaces.Repositories.Dapper;

namespace TenderManagement.Infrastructure.Repositories
{
    public class BidReadRepository : IBidReadRepository
    {
        private readonly DapperContext _context;

        public BidReadRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<BidDetailDto> GetBidByIdAsync(int id)
        {
            var query = @"
                SELECT
                    B.Id AS BidId, B.Amount, B.SubmissionDate, B.Comments,
                    V.Id AS VendorId, V.Name AS VendorName,
                    S.Id AS StatusId, S.Name AS StatusName
                FROM Bids AS B
                JOIN Vendors AS V ON B.VendorId = V.Id
                JOIN Statuses AS S ON B.StatusId = S.Id
                WHERE B.Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<BidDetailDto>(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<BidDetailDto>> GetBidsByTenderIdAsync(int tenderId)
        {
            var query = @"
                SELECT
                    B.Id AS BidId, B.Amount, B.SubmissionDate, B.Comments,
                    V.Id AS VendorId, V.Name AS VendorName,
                    S.Id AS StatusId, S.Name AS StatusName
                FROM Bids AS B
                JOIN Vendors AS V ON B.VendorId = V.Id
                JOIN Statuses AS S ON B.StatusId = S.Id
                WHERE B.TenderId = @TenderId
                ORDER BY B.SubmissionDate DESC";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<BidDetailDto>(query, new { TenderId = tenderId });
            }
        }

        public async Task<IEnumerable<BidDetailDto>> GetBidsByVendorIdAsync(int vendorId)
        {
            var query = @"
                SELECT
                    B.Id AS BidId, B.Amount, B.SubmissionDate, B.Comments,
                    V.Id AS VendorId, V.Name AS VendorName,
                    S.Id AS StatusId, S.Name AS StatusName
                FROM Bids AS B
                JOIN Vendors AS V ON B.VendorId = V.Id
                JOIN Statuses AS S ON B.StatusId = S.Id
                WHERE B.VendorId = @VendorId
                ORDER BY B.SubmissionDate DESC";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<BidDetailDto>(query, new { VendorId = vendorId });
            }
        }
    }
}