using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebTechnologies.Application.Interfaces;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Infrastructure.Data;
public class UserDbContext : DbContext, IUserDbContext, IUnitOfWork
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Role>()
            .HasData(
                WebTechnologies.Domain.Constants.Roles.User,
                WebTechnologies.Domain.Constants.Roles.Support,
                WebTechnologies.Domain.Constants.Roles.Admin,
                WebTechnologies.Domain.Constants.Roles.SuperAdmin
            );
    }
}
