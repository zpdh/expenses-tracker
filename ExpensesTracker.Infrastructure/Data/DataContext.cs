using ExpensesTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(user => user.Expenses)
            .WithOne(exp => exp.User)
            .HasForeignKey(exp => exp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>()
            .HasMany(cat => cat.Expenses)
            .WithOne(exp => exp.Category)
            .HasForeignKey(exp => exp.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}