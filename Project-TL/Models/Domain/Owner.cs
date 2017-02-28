using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Owner
    {
        public int OwnerId { get; set; }
        public virtual List<Firm> Firm{ get; set; }
        public virtual List<Hotel> Hotels { get;  }

        public Owner()
        {

        }
    }
}