using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TL.Models.Domain
{
    public interface ISystemRepository
    {
        IQueryable<Application> FindByName(string name);
        IQueryable<Application> findByStartDate(DateTime startDate);
        IQueryable<Application> findByEndDate(DateTime endDate);
        IQueryable<Application> FindByHotel(string hotelId);
        Application FindById(int id);
        IQueryable<Application> FindAll();
        void AddSyst(Application syst);
        void EditSyst(Application syst);
        void RemoveSyst(Application syst);
        void SaveChanges();





    }
}
