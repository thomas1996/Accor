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

                Owner o1 = new Owner("Georges", "Deom","Geores.deom@accor.com","+324816981");
                Owner o2 = new Owner("Their","Bommens","Their.bommes@accor.com","/");
                Owner o3 = new Owner("Rullems", "Dennis","rullems.dennis@accor.com","/");

                Maintenance m = new Maintenance(DateTime.Today, DateTime.Today, 0);
               // Maintenance m2 = new Maintenance(new DateTime(2017, 8, 1), new DateTime(2019, 8, 31), 8500);

                Application s1 = new Application(10000.00, "Boekhouding", Domain.ApplicationType.Rented);
                Application s2 = new Application(1500.00, "Reservaties", Domain.ApplicationType.Rented);
                Application s3 = new Application(1500.00, "Personeelsprogramma", Domain.ApplicationType.Rented);

                Status st1 = new Status("HQI");
                Status st2 = new Status("MAN");
                List<Application> sys = new List<Application>();
                sys.Add(s1);
                sys.Add(s2);

                List<Application> sys2 = new List<Application>();
                sys2.Add(s1);
                sys2.Add(s2);
                sys2.Add(s3);

                Hotel h1 = new Hotel("Adagio Brussel",a1, b1, "BE 0817.220.446 ", p1, "8602", "H8602@Adagio-city.com", "+32 227 41 780", o1,st1);
                Hotel h2 = new Hotel("Ibis Schiphol",a2, b2, "BE 0635.611.207 ", p2, "0649", "h0649-GM@Accor.com", "+31 20 50 25 100", o3,st2);
                Hotel h3 = new Hotel("Ibis At",a3, b2, "BE 0635.611.207 ", p3, "0650", "HA3P2@accor.com", "+31 20 34 83 533", o2,st1);

                //HotelApplication ha1 = new HotelApplication("8602",1, 15000,m, DateTime.Today, new DateTime(2018, 12, 31), "Adagio Brussel", "PersoneelsProgramma");
                //HotelApplication ha2 = new HotelApplication("8602", 2, 20000,m, DateTime.Today, new DateTime(2017, 12, 31), "Adagio Brussel", "Reservatie");
                HotelApplication ha3 = new HotelApplication("8602", 3, 5000,m, DateTime.Today, new DateTime(2018, 12, 31), "Adagio Brussel", "Boekhouden");

                HotelApplication ha4 = new HotelApplication("0649", 1, 15000,m, DateTime.Today, new DateTime(2018, 12, 31), "Ibis Schiphol", "Personeelsprogramma");
                HotelApplication ha5 = new HotelApplication("0649", 3, 1500,m, DateTime.Today, new DateTime(2018, 12, 31), "Ibis Schiphol", "Boekhouden");

                HotelApplication ha6 = new HotelApplication("0950", 2, 20000,m, DateTime.Today, new DateTime(2019, 12, 31), "Ibis At", "Reservaties");

                //h1.addApplication(ha1);
                //h1.addApplication(ha2);
                h1.addApplication(ha3);
                h2.addApplication(ha4);
                h2.addApplication(ha5);
                h3.addApplication(ha6);

                //s1.addHotel(ha1);
                //s1.addHotel(ha2);
                //s2.addHotel(ha1);
                s2.addHotel(ha3);
                
                //s3.addHotel(ha1);
                //s3.addHotel(ha2);

                p1.addHotel(h1);
                p2.addHotel(h2);
                p3.addHotel(h3);


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

                context.Statusses.Add(st1);
                context.Statusses.Add(st2);


             
                context.SaveChanges();
                //context.Database.CreateIfNotExists();
            }
            catch (Exception)
            {

                throw;
            }
            base.Seed(context);
           

            

        }
    }
}