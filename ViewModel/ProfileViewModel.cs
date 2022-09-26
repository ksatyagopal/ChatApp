using ChatApp.Commands;
using ChatApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatApp.ViewModel
{
    public class ProfileViewModel: ViewModelBase
    {
        private User user;
        public User User
        {
            get { return user; }
            set { user = value; OnPropertyChanged(nameof(User)); isdataChnaged = true; }
        }
        
        private bool isdataChnaged = false;
        
        /*
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); isdataChnaged = true; }
        }
        public string Mailid
        {
            get { return mailid; }
            set { mailid = value; OnPropertyChanged(nameof(Mailid)); isdataChnaged = true; }
        }
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; OnPropertyChanged(nameof(Mobile)); }
        }
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; OnPropertyChanged(nameof(ImageUrl)); isdataChnaged = true; }
        }*/

        private readonly FlashContext _context = new();
        public ICommand SaveDataCommand { get; }

        public ProfileViewModel()
        {
            User = _context.Users.Find(App.Current.Properties["userid"]);
            SaveDataCommand = new RelayCommand(SaveData, CanSave);
        }

        private bool CanSave(object parameter)
        {
            return isdataChnaged && !string.IsNullOrEmpty(User.MailId) && !string.IsNullOrEmpty(User.FullName);
        }

        private void SaveData(object parameter)
        {
            _context.Users.Update(User);
            _context.SaveChanges();
            MessageBox.Show("Profile Saved");
            App.Current.Windows[0].DataContext = new DashboardViewModel();
            
        }
    }
}
