using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL.Mapping
{
    public class HotelApplicationMapper: EntityTypeConfiguration<HotelApplication>
    {
        public HotelApplicationMapper()
        {
            //key
            HasKey(t => new { t.ApplicationId, t.HotelId,t.EndDate });

            //properties
            Property(t => t.Cost).IsRequired();
            Property(t => t.StartDate).IsRequired();
            Property(t => t.EndDate).IsRequired();

            //assocations
            HasOptional(t => t.Maintenance).WithMany();
        }
    }
}