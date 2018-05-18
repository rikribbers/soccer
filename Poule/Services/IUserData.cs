using System.Collections.Generic;
using Poule.Entities;
using Poule.ViewModel;

namespace Poule.Services
{
    public interface IUserData
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        User Get(string email);
        User Add(User newUser);
        User Update(User user);
        User ToEntity(UserEditModel user, int id = 0);
        UserEditModel ToEditModel(User game);
    }
}
