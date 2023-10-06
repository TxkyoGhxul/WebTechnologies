using Microsoft.EntityFrameworkCore;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Interfaces;
public interface IUserDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
}
