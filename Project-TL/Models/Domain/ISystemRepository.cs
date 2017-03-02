using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TL.Models.Domain
{
    interface ISystemRepository
    {
        Syst FindByName(string name);
        Syst findByStartDate(DateTime startDate);
        Syst findByEndDate(DateTime endDate);
        IQueryable<Syst> FindByHotel(Hotel hotel);
        IQueryable<Syst> FindAll();
        void AddSyst(Syst syst);
        void EditSyst(Syst syst);
        void RemoveSyst(Syst syst);
        void SaveChanges(Syst syst);





    }
}
