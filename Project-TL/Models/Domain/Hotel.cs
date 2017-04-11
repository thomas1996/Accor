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
     
        public virtual ICollection<Application> Systems { get; }
        public double TotalCost {
            get
            {
                totalCost = 0.0;
                if (Systems.Count > 0)
                {
                    Systems.ToList().ForEach(t =>
                    {
                        totalCost += t.Price;
                        if (t.Maintenance.Price > 0)
                        {
                            totalCost += t.Maintenance.Price;  
                        }
                    });
                }
                return totalCost;
            }

        }
        public virtual Status Status { get; set; }


        public Hotel()
        {
            Systems = new List<Application>();
            //TotalCost = calculateTotalCost();
        }

        public Hotel(string name,Adres adres, Branch branch, string vatNumber, ContactPerson contactPerson, string hotelId, string email, string telephoneNumber, Owner owner, List<Application> systems,Status status)
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
            Systems = systems;
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
            Systems = new List<Application>();
            Status = status;

            //TotalCost = calculateTotalCost();
        }

        public double calculateTotalCost()
        {
            double kosts = 0;
           foreach (Application s in Systems)
            {
                kosts += s.Price;
                if(s.Maintenance != null)
                {
                    kosts += s.Maintenance.Price;
                }
            }
            return kosts;
        }

        public void removeApplication(Application syst)
        {
            Systems.Remove(syst);
        }
        public void addApplication(Application syst)
        {
            Systems.Add(syst);
        }



    }
}