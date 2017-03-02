using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TL.Models.Domain
{
    interface IContactPersonRepository
    {
        ContactPerson FindByEmail(string email);
        ContactPerson FindByName(string lastname);
        IQueryable<ContactPerson> FindAll();
        void AddContactPerson(ContactPerson contact);
        void EditContactPerson(ContactPerson contact);
        void RemoveContactPerson(ContactPerson contact);
        void SaveChanges();


    }
}
