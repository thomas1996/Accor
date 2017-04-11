using Project_TL.Models.Domain;
using Project_TL.Models.Encryption;
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
            DataProtection dp = new DataProtection();
            try
            {
                string pass = dp.Encrypt("P@ssword123", "34875BNYM==");
                context.Users.Add(new User("Thomas", "Ik", true));
                context.Users.Add(new User("Jan@accor.com",pass , false));

                Adres a1 = new Adres("Rue de l'industrie", "1000", "Brussels", 12, "Belgium");
                Adres a2 = new Adres("Schipholweg", "1171", "Badhoevedorp", 181, "The nederlands");
                Adres a3 = new Adres("Prof K J bavicklaan", "1183", "At", 1, "The nederlands");

                Branch b1 = new Branch("Adagio Acces");
                Branch b2 = new Branch("Ibis budget");

                ContactPerson p1 = new ContactPerson("H8602@Adagio-city.com", "Laurence","Braem","0032479670122");
                ContactPerson p2 = new ContactPerson("h0649-GM@Accor.com"," van der Graaf", "Martijn","0031627081233");
                ContactPerson p3 = new ContactPerson("HA3P2-GM@accor.com", "Rutger","Blom", "0031627081168");

                Owner o1 = new Owner("Georges", "Deom");
                Owner o2 = new Owner("Their","Bommens");
                Owner o3 = new Owner("Rullems", "Dennis");

                Maintenance m = new Maintenance(new DateTime(2017, 03, 15), new DateTime(2018, 10, 10), 5000);

                Application s1 = new Application(10000.00, "Boekhouding", Domain.Type.Rented, new DateTime(2017,03,15), (new DateTime(2017, 03, 15)).Date, m);
                Application s2 = new Application(1500.00, "Reservaties", Domain.Type.Rented, new DateTime(2017, 03, 15).Date, new DateTime(2017, 03, 15), m);
                Application s3 = new Application(1500.00, "Personeelsprogramma", Domain.Type.Rented, new DateTime(2017, 03, 15).Date, new DateTime(2017, 03, 15), m);
 
                List<Application> sys = new List<Application>();
                sys.Add(s1);
                sys.Add(s2);

                List<Application> sys2 = new List<Application>();
                sys2.Add(s1);
                sys2.Add(s2);
                sys2.Add(s3);

                Hotel h1 = new Hotel("Adagio Brussel",a1, b1, "BE 0817.220.446 ", p1, "8602", "H8602@Adagio-city.com", "+32 227 41 780", o1,Status.FR);
                Hotel h2 = new Hotel("Ibis Schiphol",a2, b2, "BE 0635.611.207 ", p2, "0649", "h0649-GM@Accor.com", "+31 20 50 25 100", o3,Status.MAN);
                Hotel h3 = new Hotel("Ibis At",a3, b2, "BE 0635.611.207 ", p3, "0650", "HA3P2@accor.com", "+31 20 34 83 533", o2,Status.FR);

                h1.addApplication(s1);
                h1.addApplication(s2);
                h1.addApplication(s3);

               

                h2.addApplication(s1);

                h3.addApplication(s2);
                h3.addApplication(s3);


                p1.addHotel(h1);
                p2.addHotel(h2);
                p3.addHotel(h3);

                s1.addHotel(h1);
                s1.addHotel(h2);
               

                s2.addHotel(h2);
                s3.addHotel(h3);

                b1.addHotel(h1);
                b2.addHotel(h2);
                b2.addHotel(h3);

                o1.addHotel(h1);
                o2.addHotel(h3);
                o3.addHotel(h2);

                context.Hotels.Add(h1);
                context.Hotels.Add(h2);
                context.Hotels.Add(h3);

                context.Branches.Add(b1);
                context.Branches.Add(b2);
                context.Owner.Add(o1);
                context.Owner.Add(o2);
                context.Owner.Add(o3);
                context.Systems.AddRange(sys2);


             
                context.SaveChanges();
                //context.Database.CreateIfNotExists();
            }
            catch (Exception)
            {

                throw;
            }
            base.Seed(context);
            System.Console.Write("test");

            

        }
    }
}