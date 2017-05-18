using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_TL.ViewModels.Settings
{
    public class DeleteStatusViewModel
    {
        public DeleteStatusViewModel()
        {

        }
        public DeleteStatusViewModel(Status s,int numberOfHotels)
        {
            Name = s.St;
            NumberOfHotels = numberOfHotels;
        }
        public string  Name { get; set; }
        public int NumberOfHotels { get; set; }
        public string SelectedStatusId { get; set; }
        public IEnumerable<SelectListItem> Statusses { get; set; }
    }
}