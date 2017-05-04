using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_TL.ViewModels.Settings
{
    public class ContactViewModel
    {
        public ContactViewModel(ContactPerson cp)
        {
            ContactPersonId = cp.ContactPersonId;
            Email = cp.Email;
            CellphoneNr = cp.CellphoneNr;
            LastName = cp.LastName;
            FirstName = cp.FirstName;
            Hotels = cp.Hotels.ToList();
        }
        public ContactViewModel()
        {
            Hotels = new List<Hotel>();
        }

        public int ContactPersonId { get; set; }

        [Required(ErrorMessage ="{0} is required")]
        public string Email { get; set; }

        [Display(Name ="Phone number")]
        public string CellphoneNr { get; set; }

        [Display(Name ="Last name")]
        [Required(ErrorMessage = "{0} is required")]
        public string LastName { get; set; }

        [Display(Name ="Fist name")]
        [Required(ErrorMessage = "{0} is required")]
        public string FirstName { get; set; }

        [Display(Name ="List of hotels")]
        public  List<Hotel> Hotels { get; set; }

    }
}