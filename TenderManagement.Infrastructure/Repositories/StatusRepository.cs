using Microsoft.EntityFrameworkCore;
using TenderManagement.Application.Interfaces.Repositories;
using TenderManagement.Domain.Entities;
using TenderManagement.Infrastructure.Data;

namespace TenderManagement.Infrastructure.Repositories;

public class StatusRepository : GenericRepository<Status>, IStatusRepository
{
    public StatusRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Status> GetStatusByNameAsync(string name)
    {
        return await _context.Statuses.FirstOrDefaultAsync(s => s.Name == name);
    }
}