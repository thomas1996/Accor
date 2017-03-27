using Project_TL.Models.DAL;
using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_TL.ViewModels.Settings
{
    public class BranchViewModel
    {
        
        public BranchViewModel(Branch branch, HotelRepository hotelRepo)
        {
            PriceCat = branch.PriceCat;
            Name = branch.Name;
            if (hotelRepo == null)
                Hotels = branch.Hotels.ToList();
            else
                Hotels = hotelRepo.FindAll().ToList();

        }
        [Display(Name ="Price category")]
        public  PriceCategorie PriceCat { get; set; }

        [Display(Name ="Branch name")]
        public string Name { get; set; }

        [Display(Name ="List of hotels")]
        public List<Hotel> Hotels { get; }
    }
}