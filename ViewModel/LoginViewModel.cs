using ChatApp.Commands;
using ChatApp.Model;
using ChatApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatApp.ViewModel
{
    public class LoginViewModel: ViewModelBase
    {
        private string mobile;
        private string password;

        //public event PropertyChangedEventHandler? PropertyChanged
        //{
        //    add 
        //    { 
        //        truepassword.Append(password.Last());
        //        password.Remove(password.Last());
        //        password.Append('*');
        //    }
        //    remove 
        //    { 
        //        truepassword.Remove(truepassword.Last());
        //        password.Remove(password.Last());
        //    }
        //}

        public string Mobile 
        { 
            get { return mobile; } 
            set { mobile = value; OnPropertyChanged(nameof(Mobile)); } 
        }
        public string Password 
        { 
            get { return password; } 
            set { password = value; OnPropertyChanged(nameof(Password)); } 
        }

        public ICommand loginCommand { get; }
        private readonly FlashContext _context = new();

        public LoginViewModel()
        {
            loginCommand = new RelayCommand(Login, CanLogin);
        }

        private bool CanLogin(object para)
        {
            return !string.IsNullOrEmpty(Mobile) && !string.IsNullOrEmpty(Password);
        }
        private void Login(object para)
        {
            User? currentuser = _context.Users.FirstOrDefault(u => u.Mobile == Mobile && u.Password == Password);
            if (currentuser != null)
            {
                App.Current.Properties["userid"] = currentuser.Id;
                App.Current.Properties["username"] = currentuser.FullName;
                App.Current.Properties["mobile"] = currentuser.Mobile;
                App.Current.Properties["mailid"] = currentuser.MailId;
                App.Current.Properties["imageurl"] = (currentuser.ImageUrl==null)? "https://img.icons8.com/material-rounded/344/user-male-circle.png" : currentuser.ImageUrl;
                currentuser.IsLoggedIn = true;
                _context.SaveChanges();
                Dashboard dash = new Dashboard();
                dash.Show();
                App.Current.Windows[0].Close();
            }
            else
            {
                MessageBox.Show("Wrong credentials");
            }
        }
    }
}
