
using System.Security.Cryptography;

namespace TenderManagement.Domain.Entities;

public class Bid
{
    public int Id { get; set; }
    public int TenderId { get; set; }
    public int VendorId { get; set; }
    public decimal Amount { get; set; }
    public DateTime SubmissionDate { get; set; } = DateTime.Now;
    public required string Comments { get; set; }
    public int StatusId { get; set; }

    public Tender Tender { get; set; }
    public Vendor Vendor { get; set; }
    public Status Status { get; set; }
}
