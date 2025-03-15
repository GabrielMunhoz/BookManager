using BookManager.Domain.Entity;
using BookManager.Infra.Extensions;
using BookManager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Infra.Data;

public class BookManagerDbContext(DbContextOptions options) : DbContext(options)
{
    DbSet<Book> Books { get; set; }
    DbSet<Loan> Loans { get; set; }
    DbSet<UserBook> UserBooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookMap());
        modelBuilder.ApplyConfiguration(new UserBookMap());
        modelBuilder.ApplyConfiguration(new LoanMap());

        modelBuilder.ApplyGlobalConfiguration();
        modelBuilder.SeedData();

        base.OnModelCreating(modelBuilder);
    }
}
