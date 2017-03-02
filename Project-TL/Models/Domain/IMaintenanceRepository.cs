using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TL.Models.Domain
{
    interface IMaintenanceRepository
    {
        Maintenance FindById(int id);
        IQueryable<Maintenance> FindByStartDate(DateTime startDate);
        IQueryable<Maintenance> FindByEndDate(DateTime endDate);
        IQueryable<Maintenance> FindAll();
        void AddMaintenance(Maintenance maintenance);
        void EditMaintenance(Maintenance maintenance);
        void RemoveMaintenance(Maintenance mainenance);
        void SaveChanges();
    }
}
