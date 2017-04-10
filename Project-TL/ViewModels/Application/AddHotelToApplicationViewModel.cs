using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_TL.ViewModels.Application
{
    public class AddHotelToApplicationViewModel
    {
        public AddHotelToApplicationViewModel()
        {

        }

        public AddHotelToApplicationViewModel(List<AddHotel> ah)
        {

            Hotels = ah;
        }

       
        public List<AddHotel>  Hotels { get; set; }     
     
    }
}