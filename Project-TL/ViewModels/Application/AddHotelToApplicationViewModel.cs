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

        public AddHotelToApplicationViewModel(List<Hotel> h)
        {
            Checked = false;          
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            Hotels = h;
            Cost = 0.0;
        }

        public bool Checked { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }

        public List<Hotel>  Hotels { get; set; }

        [Required(ErrorMessage ="{0} is required")]
        public double Cost { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string Code { get; set; }
    }
}