using WebTechnologies.Application.Interfaces;
using WebTechnologies.Application.Models;
using WebTechnologies.Domain.Exceptions;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Commands.UserCommands.Create;
internal class AddUserRoleCommandHandler : BaseCommandHandler<User>, ICreateCommandHandler<AddUserRoleCommand, User>
{
    private readonly IRepository<Role> _roleRepository;

    public AddUserRoleCommandHandler(IRepository<Role> roleRepository, IRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Result<User>> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return new EntityNotFoundException(request.UserId);
        }

        var role = await _roleRepository.GetByIdAsync(request.RoleId, cancellationToken);
        if (role == null)
        {
            return new EntityNotFoundException(request.UserId);
        }

        var hasSameRole = user.Roles.Any(r => r.Equals(role));
        if (hasSameRole)
        {
            return new DublicateException("User is already have same role");
        }

        user.Roles.Add(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user;
    }
}