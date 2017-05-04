using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_TL.ViewModels.Settings
{
    public class OwnerViewModel
    {
        public OwnerViewModel(Owner o)
        {
            LastName = o.LastName;
            FirstName = o.FirstName;
            Email = o.Email;
            CellphoneNumber = o.CellphoneNumber;
            Firms = o.Firms.ToList();
            Hotels = o.Hotels.ToList();
            
        }
        public OwnerViewModel()
        {
            Firms = new List<Firm>();
            Hotels = new List<Hotel>();
        }

        [Display(Name ="Last name")]
        [Required(ErrorMessage = "{0} is required")]
        public string LastName { get; set; }

        [Display(Name ="First name")]
        [Required(ErrorMessage = "{0} is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Email { get; set; }

        [Display(Name ="Phone number")]
        public string CellphoneNumber { get; set; }
        public virtual List<Firm> Firms { get; set; }
        public virtual List<Hotel> Hotels { get; }
    }
}