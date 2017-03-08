using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Project_TL.Models.Domain
{
    public class Hotel
    {
        public Adres Adres { get; set; }
        public virtual Branch Branch { get; set; }
        public string VatNumber { get; set; }
        public virtual ContactPerson ContactPerson { get; set; }
        public string HotelId { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public virtual Owner Owner { get; set; }
       // public virtual Status Status { get; set; }

        public List<Syst> Systems { get; }


        public Hotel()
        {

        }

        public Hotel(Adres adres, Branch branch, string vatNumber, ContactPerson contactPerson, string hotelId, string email, string telephoneNumber, Owner owner, List<Syst> systems)
        {
            Adres = adres;
            Branch = branch;
            VatNumber = vatNumber;
            ContactPerson = contactPerson;
            HotelId = hotelId;
            Email = email;
            TelephoneNumber = telephoneNumber;
            Owner = owner;
            Systems = systems;
          //  Status = status;
        }


    }
}