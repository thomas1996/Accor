using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TL.Models.Domain
{
    public interface IFirmRepository
    {
        Firm FindByName(string name);
        IQueryable<Firm> FindByOwner(int ownerId);
        IQueryable<Firm> FindAll();
        void AddFirm(Firm firm);
        void RemoveFirm(Firm firm);
       
        void SaveChanges();
    }
}
