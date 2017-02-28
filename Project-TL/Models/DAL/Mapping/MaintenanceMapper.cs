using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL.Mapping
{
    public class MaintenanceMapper : EntityTypeConfiguration<Maintenance>
    {

        public MaintenanceMapper()
        {
            //properties
            Property(t => t.MaintenanceId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(t => t.Price).IsRequired();
            Property(t => t.EndDate).IsRequired();
            Property(t => t.StartDate).IsRequired();
        }
    }
}