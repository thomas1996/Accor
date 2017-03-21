using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL.Mapping
{
    public class AdresMapper : EntityTypeConfiguration<Adres>
    {
        public AdresMapper()
        {
            //key
            HasKey(t => t.Street);
            
            //prop
            Property(t => t.City).IsRequired().HasMaxLength(100);
            Property(t => t.Country).IsRequired().HasMaxLength(100);
            Property(t => t.HomeNumber).IsRequired();
            Property(t => t.Street).IsRequired().HasMaxLength(200);
            Property(t => t.PostalCode).IsRequired();  
         }
    }
}