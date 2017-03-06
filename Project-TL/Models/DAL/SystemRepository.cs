using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class SystemRepository : ISystemRepository
    {
        public void AddSyst(Syst syst)
        {
            throw new NotImplementedException();
        }

        public void EditSyst(Syst syst)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Syst> FindAll()
        {
            throw new NotImplementedException();
        }

        public Syst findByEndDate(DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Syst> FindByHotel(Hotel hotel)
        {
            throw new NotImplementedException();
        }

        public Syst FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public Syst findByStartDate(DateTime startDate)
        {
            throw new NotImplementedException();
        }

        public void RemoveSyst(Syst syst)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges(Syst syst)
        {
            throw new NotImplementedException();
        }
    }
}