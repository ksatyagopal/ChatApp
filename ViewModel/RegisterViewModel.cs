using ChatApp.Commands;
using ChatApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Net.WebRequestMethods;

namespace ChatApp.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private string errorText;
        private string name;
        private string mail;
        private string mobile;
        private string password;
        private bool dataEntered = false;
        
        public string Name 
        { 
            get { return name; } 
            set { name = value; OnPropertyChanged(nameof(Name)); dataEntered = true; } 
        }
        public string Mail
        {
            get { return mail; }
            set { mail = value; OnPropertyChanged(nameof(Mail)); dataEntered = true; }
        }
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; OnPropertyChanged(nameof(Mobile)); dataEntered = true; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(nameof(Password)); dataEntered = true; }
        }

        public string ErrorText
        {
            get { return errorText; }
            set { errorText = value; OnPropertyChanged(nameof(ErrorText)); }
        }
        

        public ICommand registerCommand { get; }
        private readonly FlashContext _context = new();

        public RegisterViewModel()
        {
            registerCommand = new RelayCommand(Register, CanRegister);
            ErrorText = "";
        }

        public bool CanRegister(object para)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(Name))
            {
                isValid = false;
                ErrorText = "Name is required";
            }
            else if (string.IsNullOrEmpty(Mail))
            {
                isValid = false;
                ErrorText = "Mail is required";
            }
            else if (string.IsNullOrEmpty(Mobile))
            {
                isValid = false;
                ErrorText = "Mobile is required";
            }
            else if (string.IsNullOrEmpty(Password))
            {
                isValid = false;
                ErrorText = "Password is required";
            }
            //else if (Regex.IsMatch(Mail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            //{
            //    isValid = false;
            //    ErrorText = "Invalid mail";
            //}
            if (!dataEntered)
            {
                ErrorText = "";
            }
            return isValid;
        }

        public void Register(object para)
        {
            _context.Users.Add(new User()
            {
                FullName = Name,
                MailId = Mail,
                Mobile = Mobile,
                Password = Password,
                ImageUrl = "https://img.icons8.com/material-rounded/344/user-male-circle.png",
                IsActive = true,
                IsLoggedIn = false
            });
            _context.SaveChanges();
            Name = "";
            Mail = "";
            Mobile = "";
            Password = "";
            MessageBox.Show("Register success");
        }
    }
}
