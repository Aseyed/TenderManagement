namespace TenderManagement.Domain.Entities;

public class Vendor
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? ContactInfo { get; set; }
}
