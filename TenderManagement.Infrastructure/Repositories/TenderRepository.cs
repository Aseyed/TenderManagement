using TenderManagement.Application.Interfaces.Repositories;
using TenderManagement.Domain.Entities;
using TenderManagement.Infrastructure.Data;

namespace TenderManagement.Infrastructure.Repositories;

public class TenderRepository : GenericRepository<Tender>, ITenderRepository
{
    public TenderRepository(ApplicationDbContext context) : base(context) { }
}