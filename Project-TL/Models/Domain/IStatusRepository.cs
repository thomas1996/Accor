using Project_TL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public interface IStatusRepository
    {
        Status FindStatus(string status);
        void AddStatus(Status status);
        void RemoveStatus(Status status);
        IQueryable<Status> FindAll();
        void SaveChanges();
    }
}