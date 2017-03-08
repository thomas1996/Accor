using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Owner
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public virtual List<Firm> Firm{ get; set; }
        public virtual List<Hotel> Hotels { get;  }

        public Owner()
        {
            Firm = new List<Firm>();
            Hotels = new List<Hotel>();
        }
        public Owner(string name)
        {
            Name = name;
            Firm = new List<Firm>();
            Hotels = new List<Hotel>();
        }
    }
}