using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL.Mapping
{
    public class OwnerMapper : EntityTypeConfiguration<Owner>
    {
        public OwnerMapper()
        {
         //properties
         Property(t => t.OwnerId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(t => t.Name).IsRequired().HasMaxLength(100);

         //associations
        

        }
    }
}