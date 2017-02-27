using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class Initializer : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            base.Seed(context);

            context.Hotels.Add(new Hotel());
            context.SaveChanges();
        }
    }
}