using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Syst
    {
        
        public int SystId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public virtual Type Type {get;set;    }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual ICollection<Hotel> Hotels { get; }
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