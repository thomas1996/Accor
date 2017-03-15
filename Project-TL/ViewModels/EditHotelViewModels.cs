using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_TL.ViewModels
{
    public class EditHotelViewModel
    {
        //make a readonly list so it's impossible to change the objects, but only change the selected object
        private readonly List<Branch> branches;
        private readonly List<ContactPerson> contacts;
        private readonly List<Owner> owners;
        private readonly List<Syst> syst;

        public EditHotelViewModel(Hotel h, List<Owner> owners, List<ContactPerson> contacts, List<Branch> branches, List<Syst> systems)
        {
            this.branches = branches;
            this.owners = owners;
            this.contacts = contacts;
            syst = systems;

            VatNumber = h.VatNumber;
            HotelId = h.HotelId;
            Email = h.Email;
            TelephoneNumber = h.TelephoneNumber;
            syst = systems;

        }
        [Display(Name = "Select the branch")]
        public int SelectedBranchId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Branch name")]
        public IEnumerable<SelectListItem> Branch
        {
            get
            {
                return new SelectList(branches, "BranchId", "Name");
            }
        }

        [Required(ErrorMessage = "{0} is required")]
        [DisplayFormat(DataFormatString = "{0:####.####.####}")]
        public string VatNumber { get; set; }

        [Display(Name = "Select the contact person")]
        public int SelectedContactPersonId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Contact person")]
        public IEnumerable<SelectListItem> ContactPerson
        {
            get
            {
                return new SelectList(contacts, "ContactPersonId", "LastName", "FistName");
            }
        }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(4, ErrorMessage = "{0} contains out of 4 digits", MinimumLength = 4)]
        [Display(Name = "Hotel code")]
        public string HotelId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MinLength(0, ErrorMessage = "Phone number can'ts be empty")]
        public string TelephoneNumber { get; set; }

        [Display(Name = "Select the owner")]
        public int SelectedOwnerId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Owner")]
        public IEnumerable<SelectListItem> Owner
        {
            get
            {
                return new SelectList(owners, "OwnerId", "LastName", "FirstName");
            }
        }
        [Display(Name = "Select the systems")]
        public int SelectedSystId { get; set; }

        public IEnumerable<SelectListItem> Systems
        {
            get
            {
                return new MultiSelectList(syst, "SystId", "Name");
            }

        }

    }
}