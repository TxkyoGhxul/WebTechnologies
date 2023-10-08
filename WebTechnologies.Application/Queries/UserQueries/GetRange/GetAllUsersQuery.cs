using WebTechnologies.Application.Interfaces;
using WebTechnologies.Application.Queries.UserQueries.GetSingle;
using WebTechnologies.Application.Sorters.Fields;

namespace WebTechnologies.Application.Queries.UserQueries.GetRange;
public record GetAllUsersQuery(
    string SearchText,
    UserSortField Field,
    bool Ascending,
    int PageNumber,
    int PageSize) : IQuery<SingleUserResponse>;
