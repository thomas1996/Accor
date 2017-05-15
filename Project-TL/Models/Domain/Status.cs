using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Status
    {
        public Status(string status)
        {
            St = status;
        }
        public Status()
        {

        }
        public string St { get; set; }
       
    }
}