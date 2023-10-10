using MediatR;
using WebTechnologies.Application.Models;

namespace WebTechnologies.Application.Interfaces;
internal interface ICreateCommand<T> : IRequest<Result<T>>
{
}
