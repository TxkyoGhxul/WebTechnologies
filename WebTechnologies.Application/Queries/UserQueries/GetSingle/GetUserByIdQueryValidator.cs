using FluentValidation;

namespace WebTechnologies.Application.Queries.UserQueries.GetSingle;
public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }
}
