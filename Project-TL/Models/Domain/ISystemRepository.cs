using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TL.Models.Domain
{
    public interface ISystemRepository
    {
        IQueryable<Syst> FindByName(string name);
        IQueryable<Syst> findByStartDate(DateTime startDate);
        IQueryable<Syst> findByEndDate(DateTime endDate);
        IQueryable<Syst> FindByHotel(Hotel hotel);
        Syst FindById(int id);
        IQueryable<Syst> FindAll();
        void AddSyst(Syst syst);
        void EditSyst(Syst syst);
        void RemoveSyst(Syst syst);
        void SaveChanges(Syst syst);





    }
}
