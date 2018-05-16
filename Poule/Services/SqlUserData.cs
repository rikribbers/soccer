using System.Collections.Generic;
using System.Linq;
using Poule.Data;
using Poule.Models;
using Microsoft.EntityFrameworkCore;
using Poule.ViewModel;

namespace Poule.Services
{
    public class SqlUserData : IUserData
    {
        private ApplicationDbContext _context;

        public SqlUserData(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.MyUsers.OrderBy(u => u.Order);
        }

        public User Get(int id)
        {
            return _context.MyUsers.FirstOrDefault(u => u.Id == id);
        }

        public User Get(string email)
        {
            return _context.MyUsers.FirstOrDefault(u => u.EmailAddress== email);
        }

        public User Add(User newUser)
        {
            _context.MyUsers.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        public UserEditModel ToEditModel(User user)
        {
            return new UserEditModel
            {
                Order = user.Order,
                Name = user.Name,
                EmailAddress = user.EmailAddress,
            };
        }

        public User Update(User user)
        {
            _context.Attach(user).State = EntityState.Modified;
            _context.SaveChanges();
            return user;
        }

    public User ToEntity(UserEditModel user, int id)
    {
    var g = new User
        {
            Order = user.Order,
            Name = user.Name,
            EmailAddress = user.EmailAddress,
        };
        if (id > 0)
    {
        g.Id = id;
    }

    return g;
    }
}

}
