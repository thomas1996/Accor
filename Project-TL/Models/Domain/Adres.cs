using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Adres
    {
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public int HomeNumber { get; set; }
        public string Country { get; set; }

        public Adres()
        {

        }
        public Adres(string street,string postalCode,string city,int homeNumber,string country)
        {
            Street = street;
            PostalCode = postalCode;
            City = city;
            HomeNumber = homeNumber;
            Country = country;
        }
    }
}