using MediatR;
using WebTechnologies.Application.Models;

namespace WebTechnologies.Application.Interfaces;

internal interface IUpdateCommandHandler<TCommand, TModel> : IRequestHandler<TCommand, Result<TModel>>
    where TCommand : class, IUpdateCommand<TModel>
{
}
