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
        private List<Branch> branches;
        private readonly List<Models.Domain.Application> syst;
        private  List<Status> status;

        public EditHotelViewModel()
        {

        }

        public EditHotelViewModel(Project_TL.Models.Domain.Hotel h, List<Owner> owners, List<ContactPerson> contacts, List<Branch> branches, List<Models.Domain.Application> systems)
        {
           
            syst = systems;
            status = new List<Status>();
            makeStatusList();
            this.branches = branches;

            Name = h.Name;
            VatNumber = h.VatNumber;
            HotelId = h.HotelId;
            Email = h.Email;
            TelephoneNumber = h.TelephoneNumber;
            Adres = h.Adres;

            //filling the selectlist
            //Branch = branches.Select(t => new SelectListItem
            //{
            //    Value = t.BranchId.ToString(),
            //    Text = t.Name
            //});

            ContactPerson = new SelectList(contacts, "ContactPersonId", "LastName", "FirstName");
            Owner = new SelectList(owners, "OwnerId", "LastName", "FirstName");
            Status = new SelectList(status);
            Systems = new SelectList(syst, "ApplicationId", "Name");




        }
        [Required(ErrorMessage ="{0} is required")]
        public string Name { get; set; }

        [Display(Name = "Select the branch")]
        public int SelectedBranchId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Branch name")]
        public IEnumerable<SelectListItem> Branch
        {
            get { return new SelectList(branches, "BrangeId,name"); }
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
        public int SelectedOwnerId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Display(Name = "Owner")]
        public IEnumerable<SelectListItem> Owner
        {
            get;set;
        }
        [Display(Name ="Select the systems")]
        public int SelectedSystId { get; set; }

        public IEnumerable<SelectListItem> Systems { get;set;}
        [Required(ErrorMessage ="{0} is required")]
        public Adres Adres { get; set; }

        [Display(Name ="Select the status")]
        [Required(ErrorMessage ="{0} is required")]
        public Enum SelectedStatusId { get; set; }

        public IEnumerable<SelectListItem> Status { get; set; }

        private void makeStatusList()
        {

            foreach (Status s in Enum.GetValues(typeof(Status)))
            {
                this.status.Add(s);
            }
        }


    } 
}