using MediatR;
using WebTechnologies.Application.Models;

namespace WebTechnologies.Application.Interfaces;

public interface ISingleQuery<T> : IRequest<Result<T>>
{
}
