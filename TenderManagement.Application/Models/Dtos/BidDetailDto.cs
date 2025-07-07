
namespace TenderManagement.Application.Models.Dtos;
public class BidDetailDto
{
    public int BidId { get; set; }
    public decimal Amount { get; set; }
    public DateTime SubmissionDate { get; set; }
    public int VendorId { get; set; }
    public string VendorName { get; set; }
    public int StatusId { get; set; }
    public string StatusName { get; set; }
}