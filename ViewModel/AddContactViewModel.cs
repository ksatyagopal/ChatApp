using ChatApp.Commands;
using ChatApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatApp.ViewModel
{
    public class AddContactViewModel: ViewModelBase
    {
        
        private string contactName;
        private string phoneNumber;
        private string address;
        private string imageURL;

        #region Properties
        public string ContactName
        {
            get { return contactName; }
            set { contactName = value; OnPropertyChanged(nameof(ContactName)); }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; } 
            set { phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }

        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(nameof(Address)); }
        }

        public string ImageURL
        {
            get { return imageURL; }
            set { imageURL = value; OnPropertyChanged(nameof(ImageURL)); }
        }

        #endregion

        private readonly FlashContext _context = new();
        public ICommand addContactCommand { get; }

        public AddContactViewModel()
        {
            addContactCommand = new RelayCommand(AddContact, CanAddContact);
            
        }

        

        private bool CanAddContact(object parameter)
        {
            return !string.IsNullOrEmpty(ContactName) &&
                !string.IsNullOrEmpty(PhoneNumber);
        }

        private void AddContact(object parameter)
        {
            if (string.IsNullOrEmpty(ImageURL))
            {
                ImageURL = "https://img.icons8.com/material-rounded/344/user-male-circle.png";
            }
            _context.Contacts.Add(
                new Contact() {
                        OwnerId = _context.Users.FirstOrDefault(u => u.Mobile == App.Current.Properties["mobile"]).Id,
                        ContactName = ContactName,
                        Mobile = PhoneNumber,
                        Address = Address,
                        ImageUrl = ImageURL
                    }
                );
            _context.SaveChanges();
            App.Current.Windows[0].DataContext = new DashboardViewModel();
            MessageBox.Show("Contact Added");

        }
    }
}
