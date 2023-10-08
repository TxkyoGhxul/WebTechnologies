using MediatR;
using WebTechnologies.Application.Models;

namespace WebTechnologies.Application.Interfaces;

internal interface IQueryHandler<TCommand, TModel> : IRequestHandler<TCommand, Result<PagedList<TModel>>>
    where TCommand : class, IQuery<TModel>
{
}
