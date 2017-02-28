using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Firm
    {

        public int FirmId { get; set; }
        public int VATNumber { get; set; }
        public virtual ContactPerson Contactperson { get; set; }
        public virtual Owner Owner { get; set; }

        public Firm()
        {

        }
    }
}