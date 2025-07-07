using TenderManagement.Domain.Entities;

namespace TenderManagement.Application.Interfaces.Repositories;

public interface IStatusRepository : IGenericRepository<Status>
{
    Task<Status> GetStatusByNameAsync(string name);
}