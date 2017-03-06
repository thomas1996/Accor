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
            try
            {
                context.Users.Add(new User("Thomas", "Ik", true));
                context.SaveChanges();
                context.Database.CreateIfNotExists();
            }
            catch (Exception)
            {

                throw;
            }
            base.Seed(context);

            

        }
    }
}