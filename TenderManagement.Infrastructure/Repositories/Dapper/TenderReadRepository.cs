using Dapper;
using TenderManagement.Application.Models.Dtos;
using TenderManagement.Infrastructure.Data;

namespace TenderManagement.Infrastructure.Interfaces.Repositories.Dapper;

public class TenderReadRepository : ITenderReadRepository
{
    private readonly DapperContext _context;

    public TenderReadRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TenderListDto>> GetAllTendersAsync()
    {
        var query = @"
            SELECT
                T.Id, T.Title, T.Description, T.Deadline,
                C.Id AS CategoryId, C.Name AS CategoryName,
                S.Id AS StatusId, S.Name AS StatusName
            FROM Tenders AS T
            JOIN Categories AS C ON T.CategoryId = C.Id
            JOIN Statuses AS S ON T.StatusId = S.Id";

        using (var connection = _context.CreateConnection())
        {
            return await connection.QueryAsync<TenderListDto>(query);
        }
    }

    public async Task<TenderDetailDto> GetTenderByIdAsync(int id)
    {
        var query = @"
            SELECT
                T.Id, T.Title, T.Description, T.Deadline,
                C.Id AS CategoryId, C.Name AS CategoryName,
                S.Id AS StatusId, S.Name AS StatusName,
                B.Id AS BidId, B.Amount, B.SubmissionDate,
                V.Id AS VendorId, V.Name AS VendorName,
                BS.Id AS StatusId, BS.Name AS StatusName -- For Bid Status
            FROM Tenders AS T
            JOIN Categories AS C ON T.CategoryId = C.Id
            JOIN Statuses AS S ON T.StatusId = S.Id
            LEFT JOIN Bids AS B ON T.Id = B.TenderId
            LEFT JOIN Vendors AS V ON B.VendorId = V.Id
            LEFT JOIN Statuses AS BS ON B.StatusId = BS.Id
            WHERE T.Id = @Id";

        using (var connection = _context.CreateConnection())
        {
            var tenderDictionary = new Dictionary<int, TenderDetailDto>();

            var result = await connection.QueryAsync<TenderDetailDto, BidDetailDto, TenderDetailDto>(
                query,
                (tender, bid) =>
                {
                    if (!tenderDictionary.TryGetValue(tender.Id, out var currentTender))
                    {
                        currentTender = tender;
                        tenderDictionary.Add(tender.Id, currentTender);
                    }

                    if (bid != null)
                    {
                        currentTender.Bids.Add(bid);
                    }
                    return currentTender;
                },
                new { Id = id },
                splitOn: "BidId"
            );

            return result.FirstOrDefault();
        }
    }
}
