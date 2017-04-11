using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Application
    {
        private double price;
        
        public int ApplicationId { get; set; }
        public double TotalPrice
        {

            get
            {
                price = 0.0;
                if (Hotels.Count > 0)
                {
                    Hotels.ToList().ForEach(t =>
                   {

                       price += t.Cost;
                       
                   });
                    price += Maintenance.Price;
                
                }
                return price;                
            }        
        //set {
        //price = value;
    //}
}
        public string Name { get; set; }
        public double OtherCosts { get; set; }      
        public virtual Type Type {get;set;    }
        public virtual ICollection<HotelApplication> Hotels { get;  }
        public virtual Maintenance Maintenance { get; set; }

        public Application()
        {
            Hotels = new List<HotelApplication>();
            
        }

        public Application(double price,string name,Type type,Maintenance maintenance)
        {
            OtherCosts = price;
            Name = name;
            Type = type;
            Maintenance = maintenance;

            Hotels = new List<HotelApplication>();
        }

        public void addHotel(HotelApplication h)
        {
            Hotels.Add(h);
        }
        public void removeHotel(HotelApplication h)
        {
            Hotels.Remove(h);
        }
    }
}