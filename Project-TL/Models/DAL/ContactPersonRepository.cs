using Project_TL.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_TL.Models.DAL
{
    public class ContactPersonRepository : IContactPersonRepository
    {
        private readonly Context context;
        private DbSet<ContactPerson> contactpersons;

        public ContactPersonRepository(Context context)
        {
            this.context = context;
            contactpersons = context.ContactPersons;
        }
        public void AddContactPerson(ContactPerson contact)
        {
            contactpersons.Add(contact);
        }
      
        public void EditContactPerson(ContactPerson contact)
        {
            ContactPerson cp = FindByEmail(contact.Email);
            if(cp != null)
            {
                contactpersons.Remove(cp);
                contactpersons.Add(contact);
            }
        }

        public IQueryable<ContactPerson> FindAll()
        {
            return contactpersons;
        }

        public ContactPerson FindByEmail(string email)
        {
            return contactpersons.FirstOrDefault(t => t.Email.Equals(email));
        }

        public ContactPerson FindByName(string lastname)
        {
            return contactpersons.FirstOrDefault(t => t.LastName.Equals(lastname));
        }

        public void RemoveContactPerson(ContactPerson contact)
        {
            contactpersons.Remove(contact);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public ContactPerson FindById(int id)
        {
            return contactpersons.FirstOrDefault(t => t.ContactPersonId == id);
        }
    }
}