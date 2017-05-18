using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_TL.ViewModels.Settings
{
    public class DeleteBranchViewModel
    {

        public DeleteBranchViewModel(string name,int numberOfHotels)
        {
            Name = name;
            NumberOfHotels = numberOfHotels;
           
        }
        public DeleteBranchViewModel()
        {
            
        }

        public string Name { get; set; }
        public int NumberOfHotels { get;  }

        [Display(Name ="Select the branch")]
        public string SelectedListItem { get; set; }

        [Required(ErrorMessage ="{0} is required")]
        [Display(Name ="Branch")]
        public IEnumerable<SelectListItem> List { get; set; }
    }
}