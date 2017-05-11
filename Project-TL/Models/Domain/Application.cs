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
                return TotalCost + TotalMaintenance + OtherCosts;           
            }        
        }
        public double TotalCost { get
            {
                price = 0.0;
                if (Hotels.Count > 0)
                {
                    Hotels.ToList().ForEach(t =>
                    {
                        //if contract is ended price has not be calculated
                        if(DateTime.Compare(t.EndDate,DateTime.Today) > 0 )
                        price += t.Cost;

                    });
                }
                return price;
            }
        }
        public string Name { get; set; }
        public double OtherCosts { get; set; }      
        public virtual ApplicationType Type {get;set;    }
        public virtual ICollection<HotelApplication> Hotels { get;  }
        public double TotalMaintenance { get
            {
                double maintenance = 0.0;
                
                if(Hotels.Count > 0)
                {
                    Hotels.ToList().ForEach(t =>
                    {
                        //If contract is ended, price has not to be calculated 
                        if(DateTime.Compare(t.Maintenance.EndDate,DateTime.Today) > 0 )
                        maintenance += t.Maintenance.Price;
                    });
                }
                
                return maintenance;
            }
        }

        public Application()
        {
            Hotels = new List<HotelApplication>();
           
            
        }

        public Application(double price,string name,ApplicationType type)
        {
            OtherCosts = price;
            Name = name;
            Type = type;
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