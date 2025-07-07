
namespace TenderManagement.Application.Models.Dtos;
public class TenderDetailDto : TenderListDto
{
    public List<BidDetailDto> Bids { get; set; } = new List<BidDetailDto>();
}