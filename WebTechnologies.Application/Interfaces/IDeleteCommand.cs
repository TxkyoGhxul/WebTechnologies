using MediatR;
using WebTechnologies.Application.Models;

namespace WebTechnologies.Application.Interfaces;

internal interface IDeleteCommand<T> : IRequest<Result<T>>
{
}
