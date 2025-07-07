using TenderManagement.Application.Interfaces.Repositories;
using TenderManagement.Domain.Entities;
using TenderManagement.Infrastructure.Data;

namespace TenderManagement.Infrastructure.Repositories;

public class BidRepository : GenericRepository<Bid>, IBidRepository
{
    public BidRepository(ApplicationDbContext context) : base(context) { }
}