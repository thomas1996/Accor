using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_TL.ViewModels.Settings
{
    public class DeleteContactPersonViewModel
    {

        public DeleteContactPersonViewModel(ContactPerson cp)
        {
            Id = cp.ContactPersonId;
            LastName = cp.LastName;
            FirstName = cp.FirstName;
            NumberOfHotels =cp.Hotels.Count();
        }
        public DeleteContactPersonViewModel()
        {

        }
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int NumberOfHotels { get; }

        [Display(Name ="Select the contact person")]
        [Required()]
        public int SelectedContactId { get; set; }

        public IEnumerable<SelectListItem> List { get; set; }
    }
}