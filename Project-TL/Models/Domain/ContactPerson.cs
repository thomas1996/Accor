using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class ContactPerson
    {
        public int ContactPersonId { get; set; }
        public string Email { get; set; }
        public int SelphoneNr { get; set; }
        public string Name { get; set; }
        public List<Hotel> Hotels { get; set; }
        public ContactPerson()
        {
            Hotels = new List<Hotel>();
        }

        public ContactPerson(string email,string name)
        {
            Email = email;
            Name = name;
            Hotels = new List<Hotel>();
        }
        public void addHotel(Hotel h)
        {
            Hotels.Add(h);
        }
    }
}