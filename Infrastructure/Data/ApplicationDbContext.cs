using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext: DbContext

{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    } 
    
    public DbSet<User> Creators { get; set; }
    public DbSet<Gender> Gender { get; set; }
    
}