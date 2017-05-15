using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_TL.Models.Encryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Project_TL.Models.Domain
{
    public class CostumLoginService : ILoginService
    {
        private DataProtection dp;
        private IUserRepository UR;
        private string key = "34875BNYM==";
        public CostumLoginService(IUserRepository repo)
        {
            UR = repo;
            DataProtection dp = new DataProtection();
        }


        public override async Task<bool> Login(string username, string password)
        {
            User u = UR.FindByUserName(username);
            if (u.Password == dp.Encrypt(password, key))
            {
                return true;
            }
            else
                return false;

        }
    }
}