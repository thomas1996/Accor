﻿using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class BranchRepository : IBranchRepository
    {
        private Context context;
        private DbSet<Branch> branches;

        public BranchRepository(Context context)
        {
            this.context = context;
            branches = context.Branches;
        }
        public void AddBranch(Branch branch)
        {
            branches.Add(branch);
        }

        public void EditBranch(Branch branch)
        {
            Branch b = FindByName(branch.Name);
            if(b !=null)
            {
                branches.Remove(b);
                branches.Add(branch);
            }
        }

        public IQueryable<Branch> FindAll()
        {
             return branches;
           
        }

        public Branch FindByName(string name)
        {
            return branches.FirstOrDefault(t => t.Name.Equals(name));
        }

        public void RemoveBranach(Branch branch)
        {
            branches.Remove(branch);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}