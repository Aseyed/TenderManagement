namespace TenderManagement.Domain.Entities;

public class Status
{
    public int Id { get; set; }
    public required string Name { get; set; } // e.g., "Open", "Closed", "Pending", "Approved", "Rejected"
}
