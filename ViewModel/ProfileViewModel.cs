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
        private string name = App.Current.Properties["username"].ToString();
        private string mailid = App.Current.Properties["mailid"].ToString();
        private string mobile = App.Current.Properties["mobile"].ToString();
        private string imageUrl = App.Current.Properties["imageurl"].ToString();
        private bool isdataChnaged = false;
        
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
        }

        private readonly FlashContext _context = new();
        public ICommand SaveDataCommand { get; }

        public ProfileViewModel()
        {
            SaveDataCommand = new RelayCommand(SaveData, CanSave);
        }

        private bool CanSave(object parameter)
        {
            return isdataChnaged && !string.IsNullOrEmpty(Mailid) && !string.IsNullOrEmpty(Name);
        }

        private void SaveData(object parameter)
        {
            User user = _context.Users.Find(App.Current.Properties["userid"]);
            if (user != null)
            {
                user.FullName = Name;
                user.MailId = Mailid;
                user.ImageUrl = ImageUrl;
                _context.SaveChanges();
                isdataChnaged = false;
                MessageBox.Show("Profile Saved");
                App.Current.Properties["userid"] = user.Id;
                App.Current.Properties["username"] = user.FullName;
                App.Current.Properties["mobile"] = user.Mobile;
                App.Current.Properties["mailid"] = user.MailId;
                App.Current.Properties["imageurl"] = (user.ImageUrl == null) ? "https://img.icons8.com/material-rounded/344/user-male-circle.png" : user.ImageUrl;
                App.Current.Windows[0].DataContext = new DashboardViewModel();
            }
        }
    }
}
