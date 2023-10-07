using WebTechnologies.Application.Interfaces;
using WebTechnologies.Application.Models;
using WebTechnologies.Domain.Constants;
using WebTechnologies.Domain.Exceptions;
using WebTechnologies.Domain.Models;
using WebTechnologies.Domain.ValueObjects;

namespace WebTechnologies.Application.Commands.UserCommands.Create;
internal class CreateUserCommandHandler : BaseCommandHandler<User>, ICreateCommandHandler<CreateUserCommand, User>
{
    public CreateUserCommandHandler(IRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }

    public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var uniqueEmail = !_repository.Get(u => u.Email == request.Email).Any();
        if (!uniqueEmail)
        {
            return new EmailDublicateException(request.Email);
        }

        var emailCreated = Email.TryFrom(request.Email, out var email);
        if (!emailCreated)
        {
            return new InvalidEmailException(request.Email);
        }

        var user = new User(request.BirthDate, request.Name, email, new List<Role> { Roles.User });
        
        _repository.Create(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return user;
    }
}