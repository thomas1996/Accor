using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Project_TL.Models.Domain
{
    public class Hotel
    {
        private double totalCost;
        public string Name { get; set; }
        public virtual Adres Adres { get; set; }
        public virtual Branch Branch { get; set; }
        public string VatNumber { get; set; }
        public virtual ContactPerson ContactPerson { get; set; }
        public string HotelId { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public virtual Owner Owner { get; set; }
     
        public virtual ICollection<HotelApplication> Applications { get; }
        public double TotalCost {
            get
            {
                totalCost = 0.0;
                if (Applications.Count > 0)
                {
                    Applications.ToList().ForEach(t =>
                    {
                        totalCost += t.Cost;
                        //if (t.Maintenance.Price > 0)
                        //{
                        //    totalCost += t.Maintenance.Price;  
                        //}
                    });
                }
                return totalCost;
            }

        }
        public virtual Status Status { get; set; }


        public Hotel()
        {
            Applications = new List<HotelApplication>();
            //TotalCost = calculateTotalCost();
        }

        public Hotel(string name,Adres adres, Branch branch, string vatNumber, ContactPerson contactPerson, string hotelId, string email, string telephoneNumber, Owner owner, List<HotelApplication> systems,Status status)
        {
            Name = name;
            Adres = adres;
            Branch = branch;
            VatNumber = vatNumber;
            ContactPerson = contactPerson;
            HotelId = hotelId;
            Email = email;
            TelephoneNumber = telephoneNumber;
            Owner = owner;
            Applications = systems;
            Status = status;
            
            //TotalCost = calculateTotalCost();
        }
        public Hotel(string name, Adres adres, Branch branch, string vatNumber, ContactPerson contactPerson, string hotelId, string email, string telephoneNumber, Owner owner, Status status)
        {
            Name = name;
            Adres = adres;
            Branch = branch;
            VatNumber = vatNumber;
            ContactPerson = contactPerson;
            HotelId = hotelId;
            Email = email;
            TelephoneNumber = telephoneNumber;
            Owner = owner;
            Applications = new List<HotelApplication>();
            Status = status;


        }

     

        public void removeApplication(HotelApplication syst)
        {
            Applications.Remove(syst);
        }
        public void addApplication(HotelApplication syst)
        {
            Applications.Add(syst);
        }



    }
}