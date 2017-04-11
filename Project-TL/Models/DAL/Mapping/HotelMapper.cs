using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL.Mapping
{
    public class HotelMapper : EntityTypeConfiguration<Hotel>
    {

        public HotelMapper()
        {
            //propertie
           
            Property(t => t.Email).IsOptional().HasMaxLength(100);
            Property(t => t.TelephoneNumber).IsRequired();
            Property(t => t.VatNumber).IsRequired();
            Ignore(t => t.TotalCost);
            Property(t => t.Status).IsRequired();
            Property(t => t.Name).IsRequired().HasMaxLength(100);

            //associations
            //HasRequired(t => t.Owner).WithMany().Map(m => m.MapKey("Owner")).WillCascadeOnDelete(false);
            HasRequired(t => t.Owner).WithMany(t => t.Hotels).Map(m => m.MapKey("Owner")).WillCascadeOnDelete(false);
            HasRequired(t => t.Branch).WithMany(t => t.Hotels).Map(m => m.MapKey("branch")).WillCascadeOnDelete(false);
            HasRequired(t => t.ContactPerson).WithMany(t => t.Hotels).Map(m => m.MapKey("ContactPerson")).WillCascadeOnDelete(false);
            //HasMany(t => t.Applications).WithMany(s => s.Hotels).Map(m =>
            //{
            //    m.ToTable("HotelSystem");
            //    m.MapLeftKey("HotelId");
            //    m.MapRightKey("SystId");
            //});

            //HasMany(t => t.Applications).WithOptional().Map(m => m.MapKey("Application"));
            HasRequired(t => t.Adres).WithOptional().Map(m => m.MapKey("Adres"));
         //   HasRequired(t => t.Status).WithOptional().Map(m => m.MapKey("Status"));
           

        }
    }
}