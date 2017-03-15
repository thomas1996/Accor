using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class OwnerRepository : IOwnerRepository
    {
        public void AddOwner(Owner owner)
        {
            throw new NotImplementedException();
        }

        public void EditOwner(Owner owner)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Owner> FindAll()
        {
            throw new NotImplementedException();
        }

        public Owner FindByFirm(string firm)
        {
            throw new NotImplementedException();
        }

        public Owner FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveOwner(Owner owner)
        {
            throw new NotImplementedException();
        }
    }
}