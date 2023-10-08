using MediatR;
using WebTechnologies.Application.Models;

namespace WebTechnologies.Application.Interfaces;

internal interface IQuery<T> : IRequest<Result<PagedList<T>>>
{
}
