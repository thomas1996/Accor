using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public interface IUserRepository
    {

        User FindByUserName(string userName);
        IQueryable<User> findAll();
        void AddUser(User user);
        void EditUser(User user);
        void RemoveUser(User user);
        void SaveChanges();
        



    }
}