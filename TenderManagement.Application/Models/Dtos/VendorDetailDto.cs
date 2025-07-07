
namespace TenderManagement.Application.Models.Dtos;
public class VendorDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ContactInfo { get; set; }
    public List<VendorBidSummaryDto> Bids { get; set; } = new List<VendorBidSummaryDto>();
}