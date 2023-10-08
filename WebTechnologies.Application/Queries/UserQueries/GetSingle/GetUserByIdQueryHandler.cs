using AutoMapper;
using WebTechnologies.Application.Interfaces;
using WebTechnologies.Application.Models;
using WebTechnologies.Domain.Exceptions;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Queries.UserQueries.GetSingle;
public class GetUserByIdQueryHandler : BaseQueryHandler<User>, ISingleQueryHandler<GetUserByIdQuery, SingleUserResponse>
{
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IRepository<User> repository, IMapper mapper) : base(repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<SingleUserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return new EntityNotFoundException(request.UserId);
        }

        return _mapper.Map<SingleUserResponse>(user);
    }
}
