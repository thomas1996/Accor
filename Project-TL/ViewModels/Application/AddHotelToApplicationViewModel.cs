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

        public AddHotelToApplicationViewModel(Hotel h)
        {
            Checked = false;
            Name = h.Name;
            Branch = h.Branch;
            Owner = h.Owner;
            Contact = h.ContactPerson;
        }

        public bool Checked { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
       
        [Display(Name = "Branch name")]
        public virtual Branch Branch { get; set; }

        [Display(Name ="Owner")]
        public Owner Owner { get; set; }

        [Display(Name ="Contact Person")]
        public ContactPerson Contact { get; set; }

    }
}