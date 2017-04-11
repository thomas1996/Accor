using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly Context context;
        private DbSet<Application> systems;

        public ApplicationRepository(Context context)
        {
            this.context = context;
            systems = context.Systems;
        }
        public void AddSyst(Application syst)
        {
            systems.Add(syst);
        }

        public void EditSyst(Application syst)
        {
            Application s = FindById(syst.ApplicationId);
            if(s != null)
            {
                systems.Remove(s);
                systems.Add(syst);
            }
        }

        public IQueryable<Application> FindAll()
        {
            return systems;
        }

        public IQueryable<Application> FindByHotel(String hotelId)
        {
            List<Application> sy = new List<Application>();
            foreach(Application s in systems)
            {
                s.Hotels.ToList().ForEach(t =>
                {
                    if (t.HotelId.Equals(hotelId))
                        sy.Add(s);
                });
            }
            return sy.AsQueryable();
        }

        public Application FindById(int id)
        {
            return systems.FirstOrDefault(t => t.ApplicationId == id);
        }

        public IQueryable<Application> FindByName(string name)
        {
            return systems.Where(t => t.Name.Equals(name));
        }

        public void RemoveSyst(Application syst)
        {
            systems.Remove(syst);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        
    }
}