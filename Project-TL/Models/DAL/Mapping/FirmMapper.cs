using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL.Mapping
{
    public class FirmMapper : EntityTypeConfiguration<Firm>
    {
        public FirmMapper()
        {

            //properties
            this.Property(t => t.FirmId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(t => t.name).IsRequired().HasMaxLength(100);

            //associaties
            HasRequired(t => t.Owner).WithMany(t => t.Firms).Map(m => m.MapKey("OwnerId")).WillCascadeOnDelete(false);
            HasRequired(t => t.Contactperson).WithRequiredDependent();
            HasOptional(t => t.Adres).WithOptionalDependent();
            
            //key
            this.HasKey(t => t.FirmId);
        }

        
    }
}