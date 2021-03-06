﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class AddApplication
    {
        public AddApplication()
        {

        }

        public AddApplication(Application app)
        {
            Checked = false;
            ApplicationId = app.ApplicationId;
            Name = app.Name;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            Cost = 0.0;
            MaintenanceCost = 0.0;
            MStartDate = DateTime.Today;
            MEndDate = DateTime.Today;
        }

        public int ApplicationId { get; set; }
        public bool Checked { get; set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Range(0,int.MaxValue,ErrorMessage ="Has to be positive")]
        [Display(Name = "Application Cost")]
        public double Cost { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Has to be positive")]
        [Display(Name = "Maintenance Cost")]
        public double MaintenanceCost { get; set; }

        [Display(Name="StartDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime MStartDate { get; set; }

        [Display(Name ="EndDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime MEndDate { get; set; }

    }
}