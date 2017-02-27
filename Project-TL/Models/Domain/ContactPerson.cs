using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class ContactPerson
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int SelphoneNr { get; set; }
        public ContactPerson()
        {

        }
    }
}