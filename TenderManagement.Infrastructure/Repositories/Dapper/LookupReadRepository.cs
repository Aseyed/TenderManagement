using System.Data;
using Dapper;
using TenderManagement.Application.Models.Dtos;
using TenderManagement.Infrastructure.Data;
using TenderManagement.Infrastructure.Interfaces.Repositories.Dapper;

namespace TenderManagement.Infrastructure.Repositories
{
    public class LookupReadRepository : ILookupReadRepository
    {
        private readonly DapperContext _context;

        public LookupReadRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LookupDto>> GetAllCategoriesAsync()
        {
            var query = "SELECT Id, Name FROM Categories ORDER BY Name";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<LookupDto>(query);
            }
        }

        public async Task<IEnumerable<LookupDto>> GetAllStatusesAsync()
        {
            var query = "SELECT Id, Name FROM Statuses ORDER BY Name";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<LookupDto>(query);
            }
        }

        public async Task<LookupDto> GetCategoryByIdAsync(int id)
        {
            var query = "SELECT Id, Name FROM Categories WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<LookupDto>(query, new { Id = id });
            }
        }

        public async Task<LookupDto> GetStatusByIdAsync(int id)
        {
            var query = "SELECT Id, Name FROM Statuses WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<LookupDto>(query, new { Id = id });
            }
        }
    }
}