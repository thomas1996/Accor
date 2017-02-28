using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Project_TL.Models.Domain
{
    public class Hotel
    {
        public string Adres { get; set; }
        public virtual Branch Branch { get; set; }
        public int VatNumber { get; set; }
        public virtual ContactPerson ContactPerson { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public int TelephoneNumber { get; set; }
        public virtual Owner Owner { get; set; }
        public virtual Syst System {get;set;         
        }

        public List<Syst> Systems { get; }
        

        public Hotel()
        {
                
        }
    }
}