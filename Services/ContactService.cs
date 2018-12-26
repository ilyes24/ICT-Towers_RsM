using System.Collections.Generic;
using RelationShipManager.Entities;
using RelationShipManager.Helpers;

namespace RelationShipManager.Services
{
    public interface IContactService
    {
        IEnumerable<Contact> GetAll();
        Contact GetById(int id);
        Contact Create(Contact contact);
        void Update(Contact contact);
        void Delete(int id);
    }

    public class ContactService : IContactService
    {
        private readonly RelShip_ManContext _context = new RelShip_ManContext();

        public Contact Create(Contact contact)
        {
            //Validation
            if (string.IsNullOrWhiteSpace(contact.ContactInfo))
                throw new AppException("Contact Info is required");
            if (string.IsNullOrWhiteSpace(contact.ContactType))
                throw new AppException("Must define the contact type");

            contact.IdContact = 0;
            //Add
            _context.Contact.Add(contact);
            _context.SaveChanges();

            return contact;
        }

        public void Delete(int id)
        {
            var contact = _context.Contact.Find(id);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Contact> GetAll()
        {
            return _context.Contact;
        }

        public Contact GetById(int id)
        {
            return _context.Contact.Find(id);
        }

        public void Update(Contact contact)
        {
            //Find contact
            var _contact = _context.Contact.Find(contact.IdContact);

            //if NOT FOUND
            if (_contact == null)
                throw new AppException("Position NOT FOUNT");

            //Validation
            if (string.IsNullOrWhiteSpace(contact.ContactInfo))
                throw new AppException("Contact Info is required");
            if (string.IsNullOrWhiteSpace(contact.ContactType))
                throw new AppException("Must define the contact type");
            if (string.IsNullOrWhiteSpace(contact.IdMyUser.ToString()))
                throw new AppException("Must define the contact user");

            //Update
            _contact.ContactType = contact.ContactType;
            _contact.ContactInfo = contact.ContactInfo;
            _contact.IsPrimary = contact.IsPrimary;
            _contact.IdMyUser = contact.IdMyUser;
            _contact.IdMyUserNavigation = contact.IdMyUserNavigation;

            //Save
            _context.Contact.Update(_contact);
            _context.SaveChanges();
        }
    }
}
