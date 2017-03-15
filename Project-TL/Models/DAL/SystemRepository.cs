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
        private Context context;
        private DbSet<Syst> systems;

        public SystemRepository(Context context)
        {
            this.context = context;
            systems = context.Systems;
        }
        public void AddSyst(Syst syst)
        {
            systems.Add(syst);
        }

        public void EditSyst(Syst syst)
        {
            Syst s = FindById(syst.SystId);
            if(s != null)
            {
                systems.Remove(s);
                systems.Add(syst);
            }
        }

        public IQueryable<Syst> FindAll()
        {
            return systems;
        }

        public IQueryable<Syst> FindByHotel(Hotel hotel)
        {
            return null;
        }

        public Syst FindById(int id)
        {
            return systems.FirstOrDefault(t => t.SystId == id);
        }

        public IQueryable<Syst> FindByName(string name)
        {
            return systems.Where(t => t.Name.Equals(name));
        }

        public void RemoveSyst(Syst syst)
        {
            systems.Remove(syst);
        }

        public void SaveChanges(Syst syst)
        {
            context.SaveChanges();
        }

        public IQueryable<Syst> findByEndDate(DateTime endDate)
        {
            return systems.Where(t => t.EndDate.ToShortDateString().Equals(endDate.ToShortDateString()));
        }

        public IQueryable<Syst> findByStartDate(DateTime startDate)
        {
            return systems.Where(t => t.StartDate.ToShortDateString().Equals(startDate.ToShortDateString()));
        }
    }
}