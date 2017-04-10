using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class AddHotel
    {
        public AddHotel()
        {

        }
        public AddHotel(Hotel h)
        {
            HotelId = h.HotelId;
            Owner = h.Owner;
            Contact = h.ContactPerson;
            Name = h.Name;
            Checked = false;
            Cost = 0.0;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public string HotelId { get; set; }
        public Owner Owner { get; set; }
        public String Name { get; set; }
        public ContactPerson Contact { get; set; }
        public bool Checked { get; set; }
        public double Cost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }

    
}