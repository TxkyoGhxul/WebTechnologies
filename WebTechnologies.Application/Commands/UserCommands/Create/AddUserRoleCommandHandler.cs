using WebTechnologies.Application.Interfaces;
using WebTechnologies.Application.Models;
using WebTechnologies.Domain.Exceptions;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Commands.UserCommands.Create;
internal class AddUserRoleCommandHandler : BaseCommandHandler<User>, ICreateCommandHandler<AddUserRoleCommand, User>
{
    public AddUserRoleCommandHandler(IRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }

    public async Task<Result<User>> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return new EntityNotFoundException(request.UserId);
        }

        user.AddRoles(request.RolesToAdd);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user;
    }
}