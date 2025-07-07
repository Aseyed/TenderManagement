
using System.Security.Cryptography;

namespace TenderManagement.Domain.Entities;

public class Tender
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime Deadline { get; set; }
    public int CategoryId { get; set; }
    public int StatusId { get; set; }


    public Category? Category { get; set; }
    public Status? Status { get; set; }
    public ICollection<Bid> Bids { get; set; } = new List<Bid>();
}
