using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Owner
    {
        public int Id { get; set; }
        public Firm Firm{ get; set; }
        public List<Hotel> Hotels { get;  }

        public Owner()
        {

        }
    }
}