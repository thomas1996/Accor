using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public interface IBranchRepository
    {
        Branch FindByName(string name);
        IQueryable<Branch> FindAll();
        void AddBranch(Branch branch);
        void EditBranch(Branch branch);
        void RemoveBranach(Branch branch);
        void SaveChanges();
       

    }
}