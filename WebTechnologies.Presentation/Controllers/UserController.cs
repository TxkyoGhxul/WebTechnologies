using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebTechnologies.Application.Commands.UserCommands.Create;
using WebTechnologies.Application.Commands.UserCommands.Delete;
using WebTechnologies.Application.Commands.UserCommands.Update;
using WebTechnologies.Application.Models;
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

    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User if true, otherwise BadRequest with error message</returns>
    [HttpGet("id:guid")]
    [ProducesResponseType(typeof(SingleUserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new GetUserByIdQuery(id);
        var result = await _sender.Send(command, cancellationToken);

        return result.Match<IActionResult>(Ok, BadRequest);
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="searchText">Text to search</param>
    /// <param name="field">Field to order by</param>
    /// <param name="ascending">Is order ascending</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>All users if true, otherwise BadRequest with error message</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedList<SingleUserResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
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

    /// <summary>
    /// Create user
    /// </summary>
    /// <param name="dto">Dto for creating user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created user if true, otherwise BadRequest with error message</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SingleUserResponse), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create(CreateUserDto dto, CancellationToken cancellationToken = default)
    {
        var command = new CreateUserCommand(dto.Name, dto.Email, dto.BirthDate);
        var result = await _sender.Send(command, cancellationToken);

        return result
            .Map(_mapper.Map<SingleUserResponse>)
            .Match<IActionResult>(succ => CreatedAtAction(nameof(Create), succ), BadRequest);
    }

    /// <summary>
    /// Delete user
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Deleted user if true, otherwise BadRequest with error message</returns>
    [HttpDelete("id:guid")]
    [ProducesResponseType(typeof(SingleUserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteUserCommand(id);
        var result = await _sender.Send(command, cancellationToken);

        return result
            .Map(_mapper.Map<SingleUserResponse>)
            .Match<IActionResult>(Ok, BadRequest);
    }

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="dto">Dto for update user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated user if true, otherwise BadRequest with error message</returns>
    [HttpPut("userId:guid")]
    [ProducesResponseType(typeof(SingleUserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update(UpdateUserDto dto, CancellationToken cancellationToken = default)
    {
        var command = new UpdateUserCommand(dto.UserId, dto.Name, dto.Email, dto.BirthDate);
        var result = await _sender.Send(command, cancellationToken);

        return result
            .Map(_mapper.Map<SingleUserResponse>)
            .Match<IActionResult>(Ok, BadRequest);
    }

    /// <summary>
    /// Add role
    /// </summary>
    /// <param name="dto">Dto for add user role</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated user if true, otherwise BadRequest with error message</returns>
    [HttpPost]
    [Route("{userId:guid}/roles")]
    [ProducesResponseType(typeof(SingleUserResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddRoles(AddUserRoleDto dto, CancellationToken cancellationToken = default)
    {
        var command = new AddUserRoleCommand(dto.UserId, dto.RoleId);
        var result = await _sender.Send(command, cancellationToken);

        return result
            .Map(_mapper.Map<SingleUserResponse>)
            .Match<IActionResult>(Ok, BadRequest);
    }

    /// <summary>
    /// Get user token
    /// </summary>
    /// <param name="userId">User id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User token</returns>
    [HttpPost]
    [Route("userId:guid/token")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GenerateToken(Guid userId, CancellationToken cancellationToken = default)
    {
        var command = new GenerateTokenCommand(userId);
        var result = await _sender.Send(command, cancellationToken);

        return result.Match<IActionResult>(Ok, BadRequest);
    }
}
