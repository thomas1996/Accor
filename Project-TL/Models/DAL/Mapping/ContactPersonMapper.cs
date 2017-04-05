using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL.Mapping
{
    public class ContactPersonMapper : EntityTypeConfiguration<ContactPerson>
    {
        public ContactPersonMapper()
        {
            //properties
            Property(t => t.Email).HasMaxLength(200);
            Property(t => t.CellphoneNr).HasMaxLength(100); 
            Property(t => t.LastName).IsRequired().HasMaxLength(100);
            Property(t => t.ContactPersonId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            //associations
           
        }


    }
}