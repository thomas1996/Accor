using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class StatusRepository : IStatusRepository
    {
        private readonly Context context;
        private DbSet<Status> statusses;
        public StatusRepository(Context context)
        {
            this.context = context;
            statusses = context.Statusses;
        }

        public void AddStatus(Status status)
        {
            statusses.Add(status);
        }

        public IQueryable<Status> FindAll()
        {
            return statusses;
        }

        public Status FindStatus(string status)
        {
            return statusses.Where(t => t.St == status).FirstOrDefault();
        }

        public void RemoveStatus(Status status)
        {
            statusses.Remove(status);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}