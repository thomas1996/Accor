using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TL.Models.Domain
{
   public interface IContactPersonRepository
    {
        ContactPerson FindByEmail(string email);
        ContactPerson FindByName(string lastname);
        ContactPerson FindById(int id);
        IQueryable<ContactPerson> FindAll();
        void AddContactPerson(ContactPerson contact);
        void EditContactPerson(ContactPerson contact);
        void RemoveContactPerson(ContactPerson contact);
        void SaveChanges();


    }
}
