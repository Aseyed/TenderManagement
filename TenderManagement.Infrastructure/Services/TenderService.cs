using TenderManagement.Application.Interfaces.Repositories;
using TenderManagement.Application.Interfaces.Services;
using TenderManagement.Application.Models.Dtos;
using TenderManagement.Domain.Entities;
using TenderManagement.Infrastructure.Data;
using TenderManagement.Infrastructure.Interfaces.Repositories.Dapper;

namespace TenderManagement.Infrastructure.Services;

public class TenderService : ITenderService
{
    private readonly ITenderRepository _tenderRepository;
    private readonly ITenderReadRepository _tenderReadRepository;
    private readonly ApplicationDbContext _dbContext;

    public TenderService(ITenderRepository tenderRepository, ITenderReadRepository tenderReadRepository, ApplicationDbContext dbContext)
    {
        _tenderRepository = tenderRepository;
        _tenderReadRepository = tenderReadRepository;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TenderListDto>> GetAllTendersAsync()
    {
        return await _tenderReadRepository.GetAllTendersAsync();
    }

    public async Task<TenderDetailDto> GetTenderByIdAsync(int id)
    {
        return await _tenderReadRepository.GetTenderByIdAsync(id);
    }

    public async Task<Tender> CreateTenderAsync(Tender tender)
    {
        await _tenderRepository.AddAsync(tender);
        await _dbContext.SaveChangesAsync();
        return tender;
    }

    public async Task UpdateTenderAsync(Tender tender)
    {
        var existingTender = await _tenderRepository.GetByIdAsync(tender.Id);
        if (existingTender == null)
        {
            throw new Exception("Tender not found.");
        }

        existingTender.Title = tender.Title;
        existingTender.Description = tender.Description;
        existingTender.Deadline = tender.Deadline;
        existingTender.CategoryId = tender.CategoryId;
        existingTender.StatusId = tender.StatusId;

        await _tenderRepository.UpdateAsync(existingTender);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTenderAsync(int id)
    {
        var tender = await _tenderRepository.GetByIdAsync(id);
        if (tender == null)
        {
            throw new Exception("Tender not found.");
        }
        await _tenderRepository.DeleteAsync(tender);
        await _dbContext.SaveChangesAsync();
    }
}
