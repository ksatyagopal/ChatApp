using ChatApp.Commands;
using ChatApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatApp.ViewModel
{
    public class ContactsViewModel: ViewModelBase
    {
        private Contact selectedContact;
        public Contact SelectedContact
        {
            get { return selectedContact; }
            set 
            {
                if(selectedContact != null)
                {
                    _context.Contacts.Update(selectedContact);
                    _context.SaveChanges();
                }
                selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));
            }
        }

        private List<Contact> contacts = new();
        public List<Contact> Contacts
        {
            set { contacts = value; OnPropertyChanged(nameof(Contacts)); }
            get { return contacts; }
        }


        private void OnPropertyChanged(string property)
        {
            base.OnPropertyChanged(property);
            
            _context.SaveChanges();
        }

        public RelayCommand SaveContactsCommand { get; }
        public RelayCommand DeleteContactsCommand { get; }

        private readonly FlashContext _context = new();


        public ContactsViewModel()
        {
            LoadContacts();
            SaveContactsCommand = new RelayCommand(SaveContacts, CanSaveOrDeleteContact);
            DeleteContactsCommand = new RelayCommand(DeleteContact, CanSaveOrDeleteContact);
        }

        private bool CanSaveOrDeleteContact(object arg)
        {
            return SelectedContact!= null;
        }

        private void DeleteContact(object obj)
        {
            _context.Contacts.Remove(SelectedContact);
            _context.SaveChanges();
        }

        private void SaveContacts(object obj)
        {
            _context.Contacts.Update(SelectedContact);
            _context.SaveChanges();
        }

        public void LoadContacts()
        {
            Contacts = new();
            foreach (var i in _context.Contacts.ToList())
            {
                var image = _context.Users.FirstOrDefault(u => u.Mobile == i.Mobile);
                if (image != null)
                {
                    i.ImageUrl = image.ImageUrl;
                    _context.SaveChanges();
                }
                if (i.ImageUrl == null)
                {
                    i.ImageUrl = "";
                }
            }
            
            Contacts = _context.Contacts.ToList();
        }

    }
}
