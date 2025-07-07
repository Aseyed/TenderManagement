
namespace TenderManagement.Application.Models.Dtos;
public class TenderListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int StatusId { get; set; }
    public string StatusName { get; set; }
}