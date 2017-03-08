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

            //associations
            HasRequired(t => t.Owner).WithMany().Map(m => m.MapKey("Owner")).WillCascadeOnDelete(false);
            HasRequired(t => t.Branch).WithMany().Map(m => m.MapKey("branch")).WillCascadeOnDelete(false);
            HasRequired(t => t.ContactPerson).WithMany().Map(m => m.MapKey("ContactPerson")).WillCascadeOnDelete(false);
            HasMany(t => t.Systems).WithMany().Map(m =>
            {
                m.ToTable("HotelSystem");
                m.MapLeftKey("HotelId");
                m.MapRightKey("BranchId");
            });
            HasRequired(t => t.Adres).WithOptional().Map(m => m.MapKey("Adres"));
         //   HasRequired(t => t.Status).WithOptional().Map(m => m.MapKey("Status"));
           

        }
    }
}