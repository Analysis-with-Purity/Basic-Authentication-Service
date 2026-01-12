using System.Linq.Expressions;
using API.Entities;
using API.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{

    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public User GetUserById(int UserId)
    {
        try
        {
            return _db.Users.Find(UserId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetUserById: {ex.Message}");
            return null;
        }
    }

    public User GetUserByEmail(string email)
    {
        try
        {
            return _db.Users.Find(email); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetUserByEmail: {ex.Message}");
            return null;

        }
    }

    public EntityEntry<User> AddUser(User user)
    {
        try
        {
            return _db.Users.Add(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AddUser: {ex.Message}");
            return null;
        }

    }

    public bool UpdateUser(User user)
    {
        try
        {
            _db.Users.Update(user);
            return SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in UpdateUser: {ex.Message}");
            return false;
        }
    }

    public bool DeleteUser(int id)
    {
        try
        {
            var user = GetUserById(id);
            if (user == null)
            {
                return false;
            }

            _db.Users.Remove(user);
            return SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DeleteUser: {ex.Message}");
            return false;
        }
    }

    public bool SaveChanges()
    {
        try
        {
            return _db.SaveChanges() >= 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in SaveChanges: {ex.Message}");
            return false;
        }
    }

    public User GetBy(Expression<Func<User, bool>> predicate)
    {
        try
        {
            return _db.Users.FirstOrDefault(predicate);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetBy: {ex.Message}");
            return null;
        }
    }
}