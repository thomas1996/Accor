using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class AddHotel
    {
        public AddHotel()
        {

        }
        public AddHotel(Hotel h)
        {
            HotelId = h.HotelId;
            Owner = h.Owner;
            Contact = h.ContactPerson;
            Name = h.Name;
            Checked = false;
            Cost = 0.0;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            MaintenanceCost = 0.0;
            MStartDate = DateTime.Today;
            MEndDate = DateTime.Today;
        }

        public string HotelId { get; set; }
        public Owner Owner { get; set; }
        public String Name { get; set; }
        public ContactPerson Contact { get; set; }
        public bool Checked { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Has to be positive")]
        [Display(Name = "Application Cost")]
        public double Cost { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Has to be positive")]
        [Display(Name = "maintenance Cost")]
        public double MaintenanceCost { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "StartDate")]
        public DateTime MStartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "EndDate")]
        public DateTime MEndDate { get; set; }

    }

    
}