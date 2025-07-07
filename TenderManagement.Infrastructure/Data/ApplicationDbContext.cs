using Microsoft.EntityFrameworkCore;
using TenderManagement.Domain.Entities;

namespace TenderManagement.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Tender> Tenders { get; set; }
    public DbSet<Bid> Bids { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<Tender>()
            .HasOne(t => t.Category)
            .WithMany()
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Tender>()
            .HasOne(t => t.Status)
            .WithMany()
            .HasForeignKey(t => t.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Tender)
            .WithMany(t => t.Bids)
            .HasForeignKey(b => b.TenderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Vendor)
            .WithMany()
            .HasForeignKey(b => b.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Status)
            .WithMany()
            .HasForeignKey(b => b.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed initial statuses
        modelBuilder.Entity<Status>().HasData(
            new Status { Id = 1, Name = "Open" },
            new Status { Id = 2, Name = "Closed" },
            new Status { Id = 3, Name = "Pending" },
            new Status { Id = 4, Name = "Approved" },
            new Status { Id = 5, Name = "Rejected" }
        );

        // Seed initial categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "IT Services" },
            new Category { Id = 2, Name = "Construction" },
            new Category { Id = 3, Name = "Consulting" },
            new Category { Id = 4, Name = "Marketing" }
        );

        // Seed an admin user for testing
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123@$"), Role = "Admin" },
            new User { Id = 2, Username = "vendor1", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Vendor123@$"), Role = "Vendor" }
        );

        // Seed an Vendor for testing
        modelBuilder.Entity<Vendor>().HasData(
            new Vendor { Id = 1, Name = "Tech Solutions Inc." },
            new Vendor { Id = 2, Name = "BuildCo Group" }
        );

        // Seed an Tender for testing
        modelBuilder.Entity<Tender>().HasData(
            new Tender
            {
                Id = 1,
                Title = "Software Development Project",
                Description = "Development of a new web application.",
                Deadline = DateTime.UtcNow.AddDays(30),
                CategoryId = 1,
                StatusId = 1
            }
        );

        // Seed an Bid for testing
        modelBuilder.Entity<Bid>().HasData(
            new Bid
            {
                Id = 1,
                TenderId = 1,
                VendorId = 1, 
                Amount = 150000.00M,
                SubmissionDate = DateTime.UtcNow.AddDays(-5),
                Comments = "Experienced team ready to start.",
                StatusId = 3
            }
        );
    }
}