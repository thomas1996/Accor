using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TL.Models.Domain
{
    interface IOwnerRepository
    {
        Owner FindByName(string name);
        Owner FindByFirm(string firm);
        IQueryable<Owner> FindAll();
        void AddOwner(Owner owner);
        void EditOwner(Owner owner);
        void RemoveOwner(Owner owner);



    }
}
