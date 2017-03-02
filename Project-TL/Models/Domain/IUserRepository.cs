using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public interface IUserRepository
    {

        User FindByEmail(string email);
        IQueryable<User> findAll();
        void AddUser(User user);
        void EditUser(User user);
        void RemoveUser(User user);
        void SafeChanges();
        



    }
}