using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL.Mapping
{
    public class StatusMapper : EntityTypeConfiguration<Status>
    {
        public StatusMapper()
        {
            HasKey(t => t.St);
        }
    }
}