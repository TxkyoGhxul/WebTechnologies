using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebTechnologies.Application.Commands.UserCommands.Create;
using WebTechnologies.Application.Commands.UserCommands.Delete;
using WebTechnologies.Application.Commands.UserCommands.Update;
using WebTechnologies.Application.Queries.UserQueries.GetRange;
using WebTechnologies.Application.Queries.UserQueries.GetSingle;
using WebTechnologies.Application.Sorters.Fields;
using WebTechnologies.Presentation.Dtos;

namespace WebTechnologies.Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UserController(ISender sender, IMapper mapper)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("id:guid")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new GetUserByIdQuery(id);
        var result = await _sender.Send(command, cancellationToken);

        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int pageNumber, 
        int pageSize,
        string searchText = "",
        UserSortField field = UserSortField.None,
        bool ascending = true,
        CancellationToken cancellationToken = default)
    {
        var command = new GetAllUsersQuery(searchText, field, ascending, pageNumber, pageSize);
        var result = await _sender.Send(command, cancellationToken);

        return result.Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto, CancellationToken cancellationToken = default)
    {
        var command = new CreateUserCommand(dto.Name, dto.Email, dto.BirthDate);
        var result = await _sender.Send(command, cancellationToken);

        return result
            .Map(_mapper.Map<SingleUserResponse>)
            .Match<IActionResult>(succ => CreatedAtAction(nameof(Create), succ), BadRequest);
    }

    [HttpDelete("id:guid")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteUserCommand(id);
        var result = await _sender.Send(command, cancellationToken);

        return result
            .Map(_mapper.Map<SingleUserResponse>)
            .Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPut("userId:guid")]
    public async Task<IActionResult> Update(UpdateUserDto dto, CancellationToken cancellationToken = default)
    {
        var command = new UpdateUserCommand(dto.UserId, dto.Name, dto.Email, dto.BirthDate);
        var result = await _sender.Send(command, cancellationToken);

        return result
            .Map(_mapper.Map<SingleUserResponse>)
            .Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPost]
    [Route("{userId:guid}/roles")]
    public async Task<IActionResult> AddRoles(AddUserRoleDto dto, CancellationToken cancellationToken = default)
    {
        var command = new AddUserRoleCommand(dto.UserId, dto.RoleId);
        var result = await _sender.Send(command, cancellationToken);

        return result
            .Map(_mapper.Map<SingleUserResponse>)
            .Match<IActionResult>(Ok, BadRequest);
    }

    [HttpPost]
    [Route("userId:guid/token")]
    public async Task<IActionResult> GenerateToken(Guid userId, CancellationToken cancellationToken = default)
    {
        var command = new GenerateTokenCommand(userId);
        var result = await _sender.Send(command, cancellationToken);

        return result.Match<IActionResult>(Ok, BadRequest);
    }
}
