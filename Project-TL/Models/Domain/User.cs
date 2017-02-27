using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class User
    {
       public int Id  {get;set;}
       public string Username { get; set; }
       public string Password { get; set; }
        public string Location { get; set; }
        public bool Admin { get; set; }
        public string ad;
    


        
    }
}