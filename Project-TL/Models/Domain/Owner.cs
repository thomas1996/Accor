using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Owner
    {
        public int OwnerId { get; set; }
        public string LastName { get; set; }
        public string FistName { get; set; }
        public virtual ICollection<Firm> Firm{ get; set; }
        public virtual ICollection<Hotel> Hotels { get;  }

        public Owner()
        {
            Firm = new List<Firm>();
            Hotels = new List<Hotel>();
        }
        public Owner(string name,string firstname)
        {
            LastName = name;
            FistName = firstname;
            Firm = new List<Firm>();
            Hotels = new List<Hotel>();
        }

        public void addHotel(Hotel h)
        {
            Hotels.Add(h);
        }
    }
}