using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Project_TL.Models.Domain
{
    public abstract class ILoginService
    {
        abstract public Task<bool> Login(string username, string password);
    }
}