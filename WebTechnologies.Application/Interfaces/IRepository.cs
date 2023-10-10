using System.Linq.Expressions;
using WebTechnologies.Domain.Models.Base;

namespace WebTechnologies.Application.Interfaces;
public interface IRepository<T> where T : Entity, new()
{
    IQueryable<T> GetAll();
    IQueryable<T> Get(Expression<Func<T, bool>> expression);
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}