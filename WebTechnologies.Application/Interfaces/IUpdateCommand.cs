using MediatR;
using WebTechnologies.Application.Models;

namespace WebTechnologies.Application.Interfaces;

internal interface IUpdateCommand<T> : IRequest<Result<T>>
{
}
