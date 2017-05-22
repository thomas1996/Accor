using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Status
    {
        public Status()
        {

        }
        public Status(string status)
        {
            St = status;
        }
       
        public string St { get; set; }
        public int Id { get; set; }

    }
}