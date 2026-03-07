using DotNetWithKafka.Domain.Entities;
using DotNetWithKafka.Domain.Entities.Pagination;
using DotNetWithKafka.Domain.Interfaces;
using DotNetWithKafka.Infrastructure.Config;
using DotNetWithKafka.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DotNetWithKafka.Infrastructure.Repository;

public class UserRepository : IUsersRepository
{
    private readonly AppDbContext _dbContext;
    
    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IPagedList<Users>> GetUsersPagination(UsersParameters usersParameters)
    {
        var usersQuery = _dbContext.Users.AsNoTracking().OrderBy(n => n.Name).AsQueryable();

        var result = await usersQuery.ToPagedListAsync(usersParameters.PageNumber, usersParameters.PageSize);
        
        return  result;
    }

    public async Task<Users> GetUserWithId(int id)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Id == id);
    }

    public async Task<Users> CreateUser(Users user)
    {
        throw new NotImplementedException();
    }

    public Task<Users> UpdateUser(Users user)
    {
        throw new NotImplementedException();
    }

    public Task<Users> DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}