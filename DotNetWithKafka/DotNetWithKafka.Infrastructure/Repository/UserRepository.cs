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
        if (user is null)
            throw new ArgumentNullException(nameof(user));
        
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<Users> UpdateUser(Users user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));
        
        var userDb = await _dbContext.Users.FindAsync(user.Id);

        if (userDb is null) return null;
        
        _dbContext.Entry(userDb).CurrentValues.SetValues(user);

        await _dbContext.SaveChangesAsync();

        return userDb;
    }

    public async Task<Users> DeleteUser(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        
        if (user is null)
            throw new ArgumentNullException(nameof(user));
        
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }
}