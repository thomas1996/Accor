using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class ContactPersonRepository : IContactPersonRepository
    {
        public void AddContactPerson(ContactPerson contact)
        {
            throw new NotImplementedException();
        }

        public void EditContactPerson(ContactPerson contact)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ContactPerson> FindAll()
        {
            throw new NotImplementedException();
        }

        public ContactPerson FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public ContactPerson FindByName(string lastname)
        {
            throw new NotImplementedException();
        }

        public void RemoveContactPerson(ContactPerson contact)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}