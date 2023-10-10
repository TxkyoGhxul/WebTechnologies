using WebTechnologies.Application.Interfaces;
using WebTechnologies.Application.Models;
using WebTechnologies.Domain.Constants;
using WebTechnologies.Domain.Exceptions;
using WebTechnologies.Domain.Models;
using WebTechnologies.Domain.ValueObjects;

namespace WebTechnologies.Application.Commands.UserCommands.Create;
internal class CreateUserCommandHandler : BaseCommandHandler<User>, ICreateCommandHandler<CreateUserCommand, User>
{
    private readonly IRepository<Role> _roleRepository;

    public CreateUserCommandHandler(IRepository<User> repository, IUnitOfWork unitOfWork, IRepository<Role> roleRepository) : base(repository, unitOfWork)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var emailCreated = Email.TryFrom(request.Email, out var email);
        if (!emailCreated)
        {
            return new InvalidEmailException(request.Email);
        }

        var uniqueEmail = !_repository.Get(u => u.Email == request.Email).Any();
        if (!uniqueEmail)
        {
            return new EmailDublicateException(request.Email);
        }

        var userRole = _roleRepository.Get(r => r.Name == Roles.User.Name).FirstOrDefault();
        if (userRole == null)
        {
            return new EntityNotFoundException("User role not found");
        }

        var user = new User(request.BirthDate, request.Name, email);
        user.Roles.Add(userRole);

        _repository.Create(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user;
    }
}