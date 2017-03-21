using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Project_TL.Models.Domain
{
    public class Hotel
    {
        public virtual Adres Adres { get; set; }
        public virtual Branch Branch { get; set; }
        public string VatNumber { get; set; }
        public virtual ContactPerson ContactPerson { get; set; }
        public string HotelId { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public virtual Owner Owner { get; set; }
        
        public double TotalCost { get; set; }

        public virtual ICollection<Syst> Systems { get; }
        public virtual Status Status { get; set; }


        public Hotel()
        {
            Systems = new List<Syst>();
            TotalCost = calculateTotalCost();
        }

        public Hotel(Adres adres, Branch branch, string vatNumber, ContactPerson contactPerson, string hotelId, string email, string telephoneNumber, Owner owner, List<Syst> systems,Status status)
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
            Status = status;
            
            TotalCost = calculateTotalCost();
        }

        public double calculateTotalCost()
        {
            double kosts = 0;
           foreach (Syst s in Systems)
            {
                kosts += s.Price;
                if(s.Maintenance != null)
                {
                    kosts += s.Maintenance.Price;
                }
            }
            return kosts;
        }



    }
}