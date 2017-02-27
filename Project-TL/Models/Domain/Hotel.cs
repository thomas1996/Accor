using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Hotel
    {
        public string Adres { get; set; }
        public Branch Branch { get; set; }
        public int VatNumber { get; set; }
        public ContactPerson ContactPerson { get; set; }
        public int Code { get; set; }
        public string Email { get; set; }
        public int TelephoneNumber { get; set; }
        public Owner Owner { get; set; }
        public enum Status {
            HQI = 0,
            HQS= 1,
            JV = 2,
            FR = 3,
            FIL = 4,
            MAN = 5,

        }

        public List<System> Systems { get; }
        

        public Hotel()
        {
                
        }
    }
}