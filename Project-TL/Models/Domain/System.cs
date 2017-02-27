using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class System
    {
        public Hotel Hotel { get; set; }
        public int Id { get; set; }
        public double Price { get; set; }
        public enum Type {
        Leased = 0,
        Rented = 1,
        Purashed = 2
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Hotel> Hotels { get; }
        public Maintenance Maintenance { get; set; }

        public System()
        {

        }
    }
}