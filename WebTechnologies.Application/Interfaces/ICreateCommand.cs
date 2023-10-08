using MediatR;
using WebTechnologies.Application.Models;

namespace WebTechnologies.Application.Interfaces;
internal interface ICreateCommand<T> : IRequest<Result<T>>
{
}

internal interface IUpdateCommand<T> : IRequest<Result<T>>
{
}

internal interface IDeleteCommand<T> : IRequest<Result<T>>
{
}

internal interface ICreateCommandHandler<TCommand, TModel> : IRequestHandler<TCommand, Result<TModel>>
    where TCommand : class, ICreateCommand<TModel>
{
}

internal interface IUpdateCommandHandler<TCommand, TModel> : IRequestHandler<TCommand, Result<TModel>>
    where TCommand : class, IUpdateCommand<TModel>
{
}

internal interface IDeleteCommandHandler<TCommand, TModel> : IRequestHandler<TCommand, Result<TModel>>
    where TCommand : class, IDeleteCommand<TModel>
{
}

public interface ISingleQuery<T> : IRequest<Result<T>>
{
}

internal interface ISingleQueryHandler<TCommand, TModel> : IRequestHandler<TCommand, Result<TModel>>
    where TCommand : class, ISingleQuery<TModel>
{
}

internal interface IQuery<T> : IRequest<Result<PagedList<T>>>
{
}

internal interface IQueryHandler<TCommand, TModel> : IRequestHandler<TCommand, Result<PagedList<TModel>>>
    where TCommand : class, IQuery<TModel>
{
}
