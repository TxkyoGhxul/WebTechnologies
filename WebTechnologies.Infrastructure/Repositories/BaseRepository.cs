using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebTechnologies.Application.Interfaces;
using WebTechnologies.Domain.Models.Base;
using WebTechnologies.Infrastructure.Data;

namespace WebTechnologies.Infrastructure.Repositories;
internal class BaseRepository<T> : IRepository<T> where T : Entity, new()
{
    private readonly UserDbContext _context;

    public BaseRepository(UserDbContext context)
    {
        _context = context;
    }

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().Where(entity => entity.Id == id).FirstOrDefaultAsync(cancellationToken);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
}
