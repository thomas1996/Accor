using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Branch
    {
        public int Int { get; set; }
        public PriceCategorie PriceCat { get; set; }
        public string Name { get; set; }

        public List<Hotel> Hotels { get; }


        public Branch()
        {

        }


    }
}