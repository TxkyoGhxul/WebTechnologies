using AutoMapper;
using FluentValidation;
using WebTechnologies.Application.Extensions;
using WebTechnologies.Application.Interfaces;
using WebTechnologies.Application.Models;
using WebTechnologies.Application.Queries.UserQueries.GetSingle;
using WebTechnologies.Application.Sorters.Base;
using WebTechnologies.Application.Sorters.Fields;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Queries.UserQueries.GetRange;

internal class GetAllUsersQueryHandler : BaseQueryHandler<User>, IQueryHandler<GetAllUsersQuery, SingleUserResponse>
{
    private readonly ISorter<User, UserSortField> _sorter;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IRepository<User> repository, ISorter<User, UserSortField> sorter, IMapper mapper)
        : base(repository)
    {
        _sorter = sorter ?? throw new ArgumentNullException(nameof(sorter));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<PagedList<SingleUserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _repository.GetAll();

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            users = Filter(request.SearchText, users);
        }

        users = _sorter.Sort(users, request.Field, request.Ascending);

        return await _mapper.Map<IQueryable<SingleUserResponse>>(users)
            .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }

    private static IQueryable<User> Filter(string filterText, IQueryable<User> users)
    {
        return users.Where(x => x.Name.Contains(filterText) ||
                        x.Age.ToString() == filterText ||
                        x.BirthDate.ToString() == filterText ||
                        x.Email.Value.Contains(filterText));
    }
}