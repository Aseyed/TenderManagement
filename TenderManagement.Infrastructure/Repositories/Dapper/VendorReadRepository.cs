using Dapper;
using TenderManagement.Application.Models.Dtos;
using TenderManagement.Infrastructure.Data;

namespace TenderManagement.Infrastructure.Interfaces.Repositories.Dapper;

public class VendorReadRepository : IVendorReadRepository
{
    private readonly DapperContext _context;

    public VendorReadRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VendorDetailDto>> GetAllVendorsAsync()
    {
        var query = "SELECT Id, Name, ContactInfo FROM Vendors";
        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryAsync<VendorDetailDto>(query);
        }
    }

    public async Task<VendorDetailDto> GetVendorByIdAsync(int id)
    {
        var query = @"
            SELECT
                V.Id, V.Name, V.ContactInfo,
                B.Id AS BidId, T.Title AS TenderTitle, B.Amount, BS.Name AS BidStatusName
            FROM Vendors AS V
            LEFT JOIN Bids AS B ON V.Id = B.VendorId
            LEFT JOIN Tenders AS T ON B.TenderId = T.Id
            LEFT JOIN Statuses AS BS ON B.StatusId = BS.Id
            WHERE V.Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var vendorDictionary = new Dictionary<int, VendorDetailDto>();

            var result = await connection.QueryAsync<VendorDetailDto, VendorBidSummaryDto, VendorDetailDto>(
                query,
                (vendor, bid) =>
                {
                    if (!vendorDictionary.TryGetValue(vendor.Id, out var currentVendor))
                    {
                        currentVendor = vendor;
                        vendorDictionary.Add(vendor.Id, currentVendor);
                    }

                    if (bid != null && bid.BidId != 0) // Check for actual bid data
                    {
                        currentVendor.Bids.Add(bid);
                    }
                    return currentVendor;
                },
                new { Id = id },
                splitOn: "BidId"
            );

            return result.FirstOrDefault();
        }
    }
}