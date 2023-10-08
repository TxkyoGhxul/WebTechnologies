using WebTechnologies.Application.Interfaces;
using WebTechnologies.Domain.Models.Base;

namespace WebTechnologies.Application.Queries;

public abstract class BaseQueryHandler<TModel> where TModel : Entity, new()
{
    protected readonly IRepository<TModel> _repository;

    protected BaseQueryHandler(IRepository<TModel> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
}