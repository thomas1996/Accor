using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Syst
    {
        private double price;
        
        public int SystId { get; set; }
        public double TotalPrice
        {

            get
            {
                price = 0.0;
                if (Hotels.Count > 0)
                {
                    Hotels.ToList().ForEach(t =>
                   {
                       if (t.Systems.Count > 0)
                       {
                           t.Systems.ToList().ForEach(s =>
                            {
                                price += s.Price;
                                if (s.Maintenance.Price > 0)
                                    price += s.Maintenance.Price;
                            });
                       }
                   });
                
                }
                return price;                
            }        
        //set {
        //price = value;
    //}
}
        public string Name { get; set; }
        public double Price { get; set; }
        public virtual Type Type {get;set;    }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual ICollection<Hotel> Hotels { get;  }
        public virtual Maintenance Maintenance { get; set; }

        public Syst()
        {
            Hotels = new List<Hotel>();
            
        }

        public Syst(double price,string name,Type type,DateTime startDate,DateTime endDate,Maintenance maintenance)
        {
            Price = price;
            Name = name;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Maintenance = maintenance;

            Hotels = new List<Hotel>();
        }

        public void addHotel(Hotel h)
        {
            Hotels.Add(h);
        }
        public void removeHotel(Hotel h)
        {
            Hotels.Remove(h);
        }
    }
}