using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_TL.ViewModels
{
    public class HotelViewModel
    {
        public HotelViewModel()
        {

        }

        public HotelViewModel(Hotel h)
        {
            Owner = h.Owner;
            Adres = h.Adres;
            Branch = h.Branch;
            VatNumber = h.VatNumber;
            ContactPerson = h.ContactPerson;
            HotelId = h.HotelId;
            Email = h.Email;
            TelephoneNumber = h.TelephoneNumber;
            Systems = h.Systems;

        }
        [Required(ErrorMessage ="{0} is required")]
        [Display(Name ="City")]
        public Adres Adres { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name ="Branch name")]
        public virtual Branch Branch { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Range(0,Int32.MaxValue,ErrorMessage ="Vat-number can't be negative")]
        public int VatNumber { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name ="Contact person")]
        public virtual ContactPerson ContactPerson { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(4,ErrorMessage ="{0} contains out of 4 digits",MinimumLength = 4)]
        [Display(Name ="Hotel code")]
        public string HotelId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [MinLength(0,ErrorMessage ="Phone number can'ts be empty")]
        public string TelephoneNumber { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Display(Name ="Owner")]
        public virtual Owner Owner { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public List<Syst> Systems { get; }

    }
}