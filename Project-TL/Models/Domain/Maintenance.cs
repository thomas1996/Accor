﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class Maintenance
    {
        public int MaintenanceId { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public double Price { get; set; }
        public Maintenance()
        {

        }
        public Maintenance(DateTime startDate,DateTime endDate,double price)
        {
            StartDate = startDate;
            EndDate = endDate;
            Price = price;
        }
    }
}