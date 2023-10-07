using WebTechnologies.Application.Interfaces;
using WebTechnologies.Application.Models;
using WebTechnologies.Domain.Exceptions;
using WebTechnologies.Domain.Models;
using WebTechnologies.Domain.ValueObjects;

namespace WebTechnologies.Application.Commands.UserCommands.Update;
internal class UpdateUserCommandHandler : BaseCommandHandler<User>, IUpdateCommandHandler<UpdateUserCommand, User>
{
    public UpdateUserCommandHandler(IRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }

    public async Task<Result<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return new EntityNotFoundException(request.UserId);
        }

        if (user.Email != request.Email)
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

            user.Email = email;
        }

        user.Name = request.Name;
        user.BirthDate = request.BirthDate;

        _repository.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user;
    }
}