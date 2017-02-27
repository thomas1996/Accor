using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Firm
    {

        public int Id { get; set; }
        public int VATNumber { get; set; }
        public ContactPerson Contactperson { get; set; }
        public Owner Owner { get; set; }

        public Firm()
        {

        }
    }
}