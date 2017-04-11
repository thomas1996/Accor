using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class OwnerRepository : IOwnerRepository
    {
        private DbSet<Owner> owners;
        private Context context;

        public OwnerRepository(Context context)
        {
            this.context = context;
            owners = context.Owner;
        }
        public void AddOwner(Owner owner)
        {
            owners.Add(owner);
        }

        public void EditOwner(Owner owner)
        {
            Owner o = FindById(owner.OwnerId);
            if(o != null)
            {
                owners.Remove(o);
                owners.Add(owner);
            }
        }

        public IQueryable<Owner> FindAll()
        {
            return owners;
        }

        public Owner FindByFirm(int firmId)
        {
            //owners.Select(t =>
            //{
            //    t.Firm.Select(f =>
            //    {
            //        if (f.FirmId == firmId)
            //        {

            //        }
            //    });
            //});

            Owner ow = null;
            foreach (Owner o in owners)
            {
                foreach(Firm f in o.Firms)
                {
                    if (f.FirmId == firmId) 
                    {
                       ow = o; 
                    }
                }
            }
            return ow;
        }

        public Owner FindByName(string name)
        {
            return owners.FirstOrDefault(t => t.FirstName.Equals(name));
        }

        public void RemoveOwner(Owner owner)
        {
            owners.Remove(owner);
        }

        public Owner FindById(int id)
        {
            return owners.FirstOrDefault(t => t.OwnerId == id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
           
        }
    }
}