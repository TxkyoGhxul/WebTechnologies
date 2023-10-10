using MediatR;
using WebTechnologies.Application.Models;

namespace WebTechnologies.Application.Interfaces;

internal interface ISingleQueryHandler<TCommand, TModel> : IRequestHandler<TCommand, Result<TModel>>
    where TCommand : class, ISingleQuery<TModel>
{
}
