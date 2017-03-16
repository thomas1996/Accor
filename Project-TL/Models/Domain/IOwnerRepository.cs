using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TL.Models.Domain
{
    public interface IOwnerRepository
    {
        Owner FindByName(string name);
        Owner FindByFirm(int firmId);
        Owner FindById(int id);
        IQueryable<Owner> FindAll();
        void AddOwner(Owner owner);
        void EditOwner(Owner owner);
        void RemoveOwner(Owner owner);
        void SaveChanges();



    }
}
