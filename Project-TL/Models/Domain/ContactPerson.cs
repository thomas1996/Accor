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
        public string SelphoneNr { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
        public ContactPerson()
        {
            Hotels = new List<Hotel>();
        }

        public ContactPerson(string email,string name,string firstname)
        {
            Email = email;
            LastName = name;
            SelphoneNr = "";
            FirstName = firstname;
            Hotels = new List<Hotel>();
        }
        public ContactPerson(string email,string name,string firstname,string telephone)
        {
            Email = email;
            LastName = name;
            FirstName = firstname;
            SelphoneNr = telephone;
            Hotels = new List<Hotel>();
        }
        public void addHotel(Hotel h)
        {
            Hotels.Add(h);
        }
    }
}