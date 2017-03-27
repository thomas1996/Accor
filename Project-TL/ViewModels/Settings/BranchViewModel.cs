using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.ViewModels.Settings
{
    public class BranchViewModel
    {
        public BranchViewModel(Branch branch)
        {

        }
        public int BranchId { get; set; }
        public  PriceCategorie PriceCat { get; set; }
        public string Name { get; set; }
        public List<Hotel> Hotels { get; }
    }
}