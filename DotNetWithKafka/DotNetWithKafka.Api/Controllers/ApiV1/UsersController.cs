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
    private readonly IKafkaProducer _producer;
    
    public UsersController(IUsersRepository repository, IKafkaProducer producer)
    {
        _repository = repository;
        _producer = producer;
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

    [HttpGet("GetUsersWithId")]
    public async Task<ActionResult<Users>> GetUsersWithId(int id)
    {
        var user = await _repository.GetUserWithId(id);
        
        return  Ok(user);
    }

    [HttpPost("CreatedUser")]
    public async Task<ActionResult> CreatedUser(Users user)
    {
        if (user is null) return BadRequest("Category invalid");
        
        var newUser = new Users
        {
            CprOrCnpj = user.CprOrCnpj,
            Name = user.Name,
            Email = user.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var userCreadted = await _repository.CreateUser(newUser);
        
        await _producer.ProducerAsync("novo-user", userCreadted);
        
        return Ok(userCreadted);
    }
    
    [HttpPut("UpdateddUser")]
    public async Task<ActionResult> UpdateddUser(int id, Users user)
    {
        if (id != user.Id) return BadRequest("Dados invalidos");
        
        var userUpdeted = await _repository.UpdateUser(user);
        
        return  Ok(userUpdeted);
    }
    
    [HttpDelete("DeletedUser")]
    public async Task<ActionResult> DeletedUser(int id)
    {
        var user = await _repository.GetUserWithId(id);
        
        if (user is null) return NotFound("User not found");
        
        var userDelete =  await _repository.DeleteUser(user.Id);
        
        return Ok(userDelete);
    }
}