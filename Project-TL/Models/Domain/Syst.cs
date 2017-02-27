using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Syst
    {
        public virtual Hotel Hotel { get; set; }
        public int Id { get; set; }
        public double Price { get; set; }
        public virtual Type Type {get;set;    }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual List<Hotel> Hotels { get; }
        public virtual Maintenance Maintenance { get; set; }

        public Syst()
        {

        }
    }
}