using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.ViewModels.Settings
{
    public class StatusViewModel
    {
     
        public StatusViewModel()
        {
            Hotels = new List<Hotel>();
        }
        public StatusViewModel(Status s )
        {
            Name = s.St;
            Hotels = new List<Hotel>();
        }

        public string Name { get; set; }
        public List<Hotel> Hotels { get; set; }

    }
}