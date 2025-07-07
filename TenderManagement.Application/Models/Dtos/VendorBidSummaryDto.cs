
namespace TenderManagement.Application.Models.Dtos;
public class VendorBidSummaryDto
{
    public int BidId { get; set; }
    public string TenderTitle { get; set; }
    public decimal Amount { get; set; }
    public string BidStatusName { get; set; }
}