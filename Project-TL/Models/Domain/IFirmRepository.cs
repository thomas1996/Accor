using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TL.Models.Domain
{
    interface IFirmRepository
    {
        Firm FindByName(string name);
        IQueryable FindByOwner(string owner);
        IQueryable FindAll();
        void AddFirm(Firm firm);
        void RemoveFirm(Firm firm);
        void EditFirm(Firm firm);
        void SaveChanges();
    }
}
