using WebTechnologies.Application.Interfaces;

namespace WebTechnologies.Application.Queries.UserQueries.GetSingle;
public record GetUserByIdQuery(Guid UserId) : ISingleQuery<SingleUserResponse>;