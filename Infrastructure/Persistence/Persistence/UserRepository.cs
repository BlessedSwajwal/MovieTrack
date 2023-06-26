using Application.Common.Interfaces.Persistence;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Persistence;

public class UserRepository : IUserRepository
{
   // private static readonly List<User> _users = new();
    private readonly TrackItDbContext _dbContext;

    public UserRepository(TrackItDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public User? GetUserByEmail(string Email)
    {
    //    return _users.SingleOrDefault(x => x.Email == Email);
       return _dbContext.Users.FirstOrDefault(x => x.Email == Email);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}
