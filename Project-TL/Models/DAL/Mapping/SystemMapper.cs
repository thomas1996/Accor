using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL.Mapping
{
    public class SystemMapper : EntityTypeConfiguration<Application>
    {
        public SystemMapper()
        {
            //properties
            Property(t => t.ApplicationId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(t => t.OtherCosts).IsRequired();            
            Property(t => t.Type).IsRequired();
            Property(t => t.Name).IsRequired().HasMaxLength(100);

            //associatons
         
        }
    }
}