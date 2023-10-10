using MediatR;
using WebTechnologies.Application.Models;

namespace WebTechnologies.Application.Interfaces;

internal interface ICreateCommandHandler<TCommand, TModel> : IRequestHandler<TCommand, Result<TModel>>
    where TCommand : class, ICreateCommand<TModel>
{
}
