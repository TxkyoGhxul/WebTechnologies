using Microsoft.EntityFrameworkCore;
using WebTechnologies.Application.Interfaces;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Infrastructure.Data;
internal class UserDbContext : DbContext, IUserDbContext, IUnitOfWork
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
}
