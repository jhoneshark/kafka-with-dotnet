using DotNetWithKafka.Domain.Entities;
using DotNetWithKafka.Domain.Entities.Pagination;
using X.PagedList;

namespace DotNetWithKafka.Domain.Interfaces;

public interface IUsersRepository
{
    Task<IPagedList<Users>> GetUsersPagination(UsersParameters usersParameters);
    Task<Users> GetUserWithId(int id);
    Task<Users> CreateUser(Users user);
    Task<Users> UpdateUser(Users user);
    Task<Users> DeleteUser(int id);
}