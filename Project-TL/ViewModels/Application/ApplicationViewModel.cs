﻿using Project_TL.Models.Domain;
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
        public ApplicationViewModel(Syst syst)
        {
            Id = syst.SystId;
            Price = syst.Price;
            Name = syst.Name;
            Type = syst.Type;
            StartDate = syst.StartDate;
            EndDate = syst.EndDate;
            Hotels = syst.Hotels;
            Maintenance = syst.Maintenance;

                      
        }
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "€{0:N}")]
        public double Price { get; set; }

        public string Name { get; set; }

        public Models.Domain.Type Type { get; set; }

        public  DateTime StartDate { get; set; }

        public  DateTime EndDate { get; set; }

        public  ICollection<Hotel> Hotels { get; }

        public  Maintenance Maintenance { get; set; }

    }
}