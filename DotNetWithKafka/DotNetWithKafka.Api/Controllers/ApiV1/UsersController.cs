using Asp.Versioning;
using DotNetWithKafka.Domain.Entities;
using DotNetWithKafka.Domain.Entities.Pagination;
using DotNetWithKafka.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNetWithKafka.Api.Controllers.Apiv1;

[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _repository;
    
    public UsersController(IUsersRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("GetUsers")]
    public async Task<ActionResult<IEnumerable<Users>>> GetUsers([FromQuery] UsersParameters usersParameters)
    {
        var users = await _repository.GetUsersPagination(usersParameters);

        var metaData = new
        {
            users.Count,
            users.PageSize,
            users.PageCount,
            users.TotalItemCount,
            users.HasNextPage,
            users.HasPreviousPage,
        };

        var response = new
        {
            users = users,
            paginate = metaData

        };
        
        return Ok(response);
    }
}