using MediatR;
using WebTechnologies.Application.Models;

namespace WebTechnologies.Application.Interfaces;

internal interface IDeleteCommandHandler<TCommand, TModel> : IRequestHandler<TCommand, Result<TModel>>
    where TCommand : class, IDeleteCommand<TModel>
{
}
