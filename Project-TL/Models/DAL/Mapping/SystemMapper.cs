using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL.Mapping
{
    public class SystemMapper : EntityTypeConfiguration<Syst>
    {
        public SystemMapper()
        {
            //properties
            Property(t => t.SystId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(t => t.Price).IsRequired();
            Property(t => t.StartDate).IsRequired();
            Property(t => t.EndDate).IsRequired();
            Property(t => t.Type).IsRequired();
            Property(t => t.Name).IsRequired().HasMaxLength(100);

            //associatons
            HasOptional(t => t.Maintenance).WithRequired().Map(m => m.MapKey("Maintenance"));
        }
    }
}