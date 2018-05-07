using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Poule.Entities;
using Poule.ViewModel;

namespace Poule.Services
{
    public class SqlUserData : IUserData
    {
        private PouleDbContext _context;

        public SqlUserData(PouleDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.OrderBy(u => u.Order);
        }

        public User Get(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User Add(User newUser)
        {
            _context.Users.Add(newUser);
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
