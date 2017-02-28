using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL.Mapping
{
    public class BranchMapper : EntityTypeConfiguration<Branch>
    {
        public BranchMapper()
        {
            //table
            ToTable("Branches");

            //properties
            Property(t => t.Name).IsRequired().HasMaxLength(100);
            Property(t => t.BranchId).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            //associations
           // HasMany(t => t.Hotels).WithRequired().Map(m => m.MapKey("Hotels")).WillCascadeOnDelete(false);
            
        }
    }
}