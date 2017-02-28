using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class User
    {
       public int UserId  {get;set;}
       public string Username { get; set; }
       public string Password { get; set; }
        public string Location { get; set; }
        public bool Admin { get; set; }
        

        public User()
        {

        }


        public User(String username,string password,bool admin)
        {
            this.Username = username;
            Password = password;
            Admin = admin;
        }
            
             

        
    }
}