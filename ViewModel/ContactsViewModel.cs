using ChatApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ViewModel
{
    public class ContactsViewModel: ViewModelBase
    {
        private List<Contact> contacts = new();
        public List<Contact> Contacts
        {
            set { contacts = value; OnPropertyChanged(nameof(Contacts)); _context.SaveChanges(); }
            get { return contacts; }
        }

        private readonly FlashContext _context = new();


        public ContactsViewModel()
        {
            LoadContacts();
        }

        public void LoadContacts()
        {
            Contacts = new();
            foreach (var i in _context.Contacts.ToList())
            {
                    var image = _context.Users.FirstOrDefault(u => u.Mobile == i.Mobile);
                    Contacts.Add(new Contact()
                    {
                        ContactName = i.ContactName,
                        Mobile = i.Mobile,
                        Address = i.Address,
                        ImageUrl = image != null ? image.ImageUrl : i.ImageUrl
                    });
                    if (image != null)
                    {
                        i.ImageUrl = image.ImageUrl;
                    }
                
            }
            _context.SaveChanges();
        }
    }
}
