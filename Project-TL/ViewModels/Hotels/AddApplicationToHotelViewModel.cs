using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project_TL.Models.Domain;

namespace Project_TL.ViewModels
{
    public class AddApplicationToHotelViewModel
    {
        public AddApplicationToHotelViewModel()
        {

        }

        public AddApplicationToHotelViewModel(List<AddApplication> list)
        {
            Applications = list;
        }
        public List<AddApplication> Applications { get; set; }
    }
}