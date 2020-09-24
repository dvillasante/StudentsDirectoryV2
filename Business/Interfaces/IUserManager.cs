using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IUserManager
    {

        User Authenticate(string username, string password);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
    }
}
