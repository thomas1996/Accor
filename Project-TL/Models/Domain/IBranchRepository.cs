using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.Domain
{
    public interface IBranchRepository
    {
        Branch FindByName(string name);
        Branch FindById(int id);
        IQueryable<Branch> FindAll();
        void AddBranch(Branch branch);
        void EditBranch(Branch branch);
        void RemoveBranch(Branch branch);
        void SaveChanges();
       

    }
}