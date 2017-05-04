﻿using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_TL.ViewModels.Settings
{
    public class DeleteOwnerViewModel
    {
        public DeleteOwnerViewModel(Owner o )
        {
            FirstName = o.FirstName;
            LastName = o.LastName;
            Id = o.OwnerId;
        }

        public DeleteOwnerViewModel()
        {

        }       

        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public int Id { get; set; }

        [Required]
        [Display(Name="Please select the owner")]
        public int SelectedOwner { get; set; }
        public IEnumerable<SelectListItem> ListOwner { get; set; }

        [Required]
        [Display(Name ="Please select the firm")]
        public int SelectFirm { get; set; }
        public IEnumerable<SelectListItem> ListFirm { get; set; }

    }
}