using WebTechnologies.Application.Interfaces;
using WebTechnologies.Domain.Models.Base;

namespace WebTechnologies.Application.Commands;
public abstract class BaseCommandHandler<TModel> where TModel : Entity, new()
{
    protected readonly IRepository<TModel> _repository;
    protected readonly IUnitOfWork _unitOfWork;

    protected BaseCommandHandler(IRepository<TModel> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
}
