using FluentValidation;

namespace WebTechnologies.Application.Queries.UserQueries.GetRange;

internal class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
{
    public GetAllUsersQueryValidator()
    {
    }
}
