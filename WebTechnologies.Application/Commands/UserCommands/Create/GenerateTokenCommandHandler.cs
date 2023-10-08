using WebTechnologies.Application.Interfaces;
using WebTechnologies.Application.Models;
using WebTechnologies.Domain.Exceptions;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Commands.UserCommands.Create;

internal class GenerateTokenCommandHandler : BaseCommandHandler<User>, ICreateCommandHandler<GenerateTokenCommand, string>
{
    private readonly IJwtTokenProvider _tokenProvider;

    public GenerateTokenCommandHandler(IRepository<User> repository, IUnitOfWork unitOfWork, IJwtTokenProvider tokenProvider) : base(repository, unitOfWork)
    {
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<string>> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return new EntityNotFoundException(request.UserId);
        }

        return Result<string>.From(_tokenProvider.GetJwtToken(user));
    }
}