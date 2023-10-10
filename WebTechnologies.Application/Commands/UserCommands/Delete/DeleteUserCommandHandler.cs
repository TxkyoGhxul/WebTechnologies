using WebTechnologies.Application.Interfaces;
using WebTechnologies.Application.Models;
using WebTechnologies.Domain.Exceptions;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Commands.UserCommands.Delete;
internal class DeleteUserCommandHandler : BaseCommandHandler<User>, IDeleteCommandHandler<DeleteUserCommand, User>
{
    public DeleteUserCommandHandler(IRepository<User> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }

    public async Task<Result<User>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userToDelete = await _repository.GetByIdAsync(request.UserId, cancellationToken);
        if (userToDelete == null)
        {
            return new EntityNotFoundException(request.UserId);
        }

        _repository.Delete(userToDelete);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return userToDelete;
    }
}
