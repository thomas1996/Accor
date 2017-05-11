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

        public EditHotelViewModel()
        {

        }

        public EditHotelViewModel(Hotel h)
        {

            Name = h.Name;
            VatNumber = h.VatNumber;
            HotelId = h.HotelId;
            Email = h.Email;
            TelephoneNumber = h.TelephoneNumber;
            Adres = h.Adres;

        }
        [Required(ErrorMessage ="{0} is required")]
        public string Name { get; set; }

        [Display(Name = "Select the branch")]
        public string SelectedBranchId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Branch name")]
        public IEnumerable<SelectListItem> Branch
        {
            get; set;
        }

        [Required(ErrorMessage = "{0} is required")]
        [DisplayFormat(DataFormatString = "{0:####.####.####}")]
        public string VatNumber { get; set; }

        [Display(Name = "Select the contact person")]
        public string SelectedContactPersonId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Contact person")]
        public IEnumerable<SelectListItem> ContactPerson
        {
            get;set;
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
        public string SelectedOwnerId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Owner")]
        public IEnumerable<SelectListItem> Owner
        {
            get;set;
        }
       
        public Adres Adres { get; set; }

        [Display(Name ="Select the status")]
        [Required(ErrorMessage ="{0} is required")]
        public String SelectedStatusId { get; set; }

        public IEnumerable<SelectListItem> Status { get; set; }

    

    } 
}