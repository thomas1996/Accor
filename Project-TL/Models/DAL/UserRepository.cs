using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class UserRepository : IUserRepository
    {
        private Context context;
        private DbSet<User> users;

        public UserRepository(Context context)
        {
            this.context = context;
            users = context.Users;
        }
        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void EditUser(User user)
        {
            User u = FindByUserName(user.Username);
            if(u != null)
            {
                users.Remove(u);
                users.Add(user);
            }
        }

        public IQueryable<User> findAll()
        {
            return users;
        }

        public User FindByUserName(string userName)
        {
            return users.FirstOrDefault(t => t.Username.Equals(userName));
        }

        public void RemoveUser(User user)
        {
            users.Remove(user);
        }

        public void SafeChanges()
        {
            context.SaveChanges();
        }
    }
}