using WebTechnologies.Application.Interfaces;

namespace WebTechnologies.Infrastructure.Data;
public class UnitOfWork : IUnitOfWork
{
    private readonly UserDbContext _userDbContext;

    public UnitOfWork(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _userDbContext.SaveChangesAsync(cancellationToken);
    }
}
