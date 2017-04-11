using System;
using System.Collections.Generic;
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
        }

        public int ApplicationId { get; set; }
        public bool Checked { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Cost { get; set; }

    }
}