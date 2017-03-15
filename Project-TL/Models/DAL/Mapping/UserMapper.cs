using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL.Mapping
{
    public class UserMapper : EntityTypeConfiguration<User>
    {

        public UserMapper()
        {
            //key
            HasKey(t => t.Username);
            //properties
           
            Property(t => t.Username).IsRequired().HasMaxLength(150);
            Property(t => t.Password).IsRequired().HasMaxLength(20);
            Property(t => t.Location).IsOptional().HasMaxLength(50);
            Property(t => t.Admin).IsRequired();
        }
    }
}