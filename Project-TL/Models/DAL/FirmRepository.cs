using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class FirmRepository : IFirmRepository
    {
        private DbSet<Firm> firms;
        private Context context;

        public FirmRepository(Context context)
        {
            this.context = context;
            firms = context.Firms;
        }
        public void AddFirm(Firm firm)
        {
            firms.Add(firm);
        }


        public IQueryable<Firm> FindAll()
        {
            return firms;
        }

        public Firm FindByName(string name)
        {
            return firms.Where(t => t.name == name).FirstOrDefault();
        }

        public IQueryable<Firm> FindByOwner(int ownerId)
        {
            return firms.Where(t => t.Owner.OwnerId == ownerId);
        }

        public void RemoveFirm(Firm firm)
        {
            firms.Remove(firm);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}