using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_TL.ViewModels.Application
{
    public class EditApplicationViewModel
    {
        public EditApplicationViewModel()
        {

        }
        public EditApplicationViewModel(Project_TL.Models.Domain.Application app)
        {
            Name = app.Name;
            Id = app.ApplicationId;
        }

        public string Name { get; set; }
        public string SelectedStatusId { get; set; }
        public IEnumerable<SelectListItem> TypeList { get; set; }
        public int Id { get; set; }
    }
}