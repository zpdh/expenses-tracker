using ExpensesTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Permission = ExpensesTracker.Domain.Enums.Permission;

namespace ExpensesTracker.Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Domain.Entities.Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(user => user.Expenses)
            .WithOne(exp => exp.User)
            .HasForeignKey(exp => exp.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users);

        modelBuilder.Entity<Category>()
            .HasMany(cat => cat.Expenses)
            .WithOne(exp => exp.Category)
            .HasForeignKey(exp => exp.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Role>()
            .HasMany(role => role.Permissions)
            .WithMany()
            .UsingEntity<PermissionRole>();

        modelBuilder.Entity<Role>()
            .HasData(Role.GetValues());

        var permissions = Enum.GetValues<Permission>()
            .Select(perm => new Domain.Entities.Permission
            {
                Id = (int)perm,
                Name = perm.ToString()
            });

        modelBuilder.Entity<PermissionRole>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        modelBuilder.Entity<PermissionRole>()
            .HasData(PermissionRole.Create(Role.Registered, Permission.Registered),
                PermissionRole.Create(Role.Administrator, Permission.Administrator));

        modelBuilder.Entity<Domain.Entities.Permission>()
            .HasData(permissions);
    }
}