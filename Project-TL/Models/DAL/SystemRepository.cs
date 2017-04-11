using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class SystemRepository : ISystemRepository
    {
        private readonly Context context;
        private DbSet<Application> systems;

        public SystemRepository(Context context)
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
                if(s.Hotels.FirstOrDefault(h => h.HotelId.Equals(hotelId)) != null)
                {
                    sy.Add(s);
                }
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

        public IQueryable<Application> findByEndDate(DateTime endDate)
        {
            return systems.Where(t => t.EndDate.ToShortDateString().Equals(endDate.ToShortDateString()));
        }

        public IQueryable<Application> findByStartDate(DateTime startDate)
        {
            return systems.Where(t => t.StartDate.ToShortDateString().Equals(startDate.ToShortDateString()));
        }
    }
}