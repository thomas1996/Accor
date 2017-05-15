using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_TL.ViewModels.Application
{
    public class ApplicationViewModel
    {
        public ApplicationViewModel(Models.Domain.Application syst)
        {
            Id = syst.ApplicationId;
            Price = syst.TotalCost;
            Name = syst.Name;
            Type = syst.Type;          
            NumberOfHotels = syst.Hotels.Where(t => DateTime.Compare(t.EndDate,DateTime.Today) >= 0).Where(t => DateTime.Compare(t.StartDate,DateTime.Today) <= 0).Count();
            Hotels = syst.Hotels;
            Maintenance = syst.TotalMaintenance;
            TotalPrice = syst.TotalPrice;

                      
        }
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "€{0:N}")]

        [Display(Name ="Contract Cost")]
        public double Price { get; set; }

        public string Name { get; set; }

        public Models.Domain.ApplicationType Type { get; set; }
      

        public  int NumberOfHotels { get; set; }

        public  ICollection<HotelApplication> Hotels { get; }

        [Display(Name =("Maintenance Cost"))]
        public  double Maintenance { get; set; }

        [Display(Name ="Total Application Cost")]
        public double TotalPrice { get; set; }

    }
}