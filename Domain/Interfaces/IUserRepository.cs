using System.Linq.Expressions;
using API.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Interfaces;

public interface IUserRepository
{
    User GetUserById(int id);
    User GetUserByEmail(string email);
    EntityEntry<User> AddUser(User user);
    bool UpdateUser(User user);
    bool DeleteUser(int id);
    bool SaveChanges();
        
    //User GetBy(Expression<Func<User, bool>> predicate);
}