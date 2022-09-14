using ChatApp.Commands;
using ChatApp.Model;
using ChatApp.UserControls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Linq;
using System.Windows;

namespace ChatApp.ViewModel
{
    public class DashboardViewModel: ViewModelBase
    {
        #region Properties
        public ObservableCollection<Contact> Contacts { get; set; }
        
        private User cUser = new();
        public User CUser
        {
            get { return cUser; }
            set { cUser = value; OnPropertyChanged(nameof(CUser)); }
        }


        private bool? selectedContactStatus;
        public bool? SelectedContactStatus 
        {
            get
            {
                return selectedContactStatus;
            }
            set
            {
                selectedContactStatus = value;
                OnPropertyChanged(nameof(SelectedContactStatus));
            }
        }

        private ObservableCollection<Message> selectedContactMessages = new();
        public ObservableCollection<Message> SelectedContactMessages 
        {
            get 
            {
                return selectedContactMessages;
            } 
            set 
            {
                selectedContactMessages = value;
                OnPropertyChanged(nameof(SelectedContactMessages));
            }
        }

        private User? selecteduser;
        private Contact selectedContact;
        public Contact SelectedContact
        {
            get
            {
                return selectedContact;
            }
            set
            {
                selectedContact = value;
                
                selecteduser = flashContext.Users.FirstOrDefault(u=>u.Mobile==SelectedContact.Mobile);
                if (selecteduser != null)
                {
                    SelectedContactMessages = new();
                    List<Message> msgs = new();
                    msgs = (from i in flashContext.Messages
                                          where (i.SenderId == CUser.Id && i.ReceiverId == selecteduser.Id ) || (i.ReceiverId == CUser.Id && i.SenderId == selecteduser.Id)
                                          select i).Include("Sender").Include("Receiver").ToList();
                    foreach (var m in msgs)
                        SelectedContactMessages.Add(m);
                    SelectedContactStatus = selecteduser.IsLoggedIn;
                }
                else
                {
                    SelectedContactMessages=new();
                    SelectedContactStatus = null;
                    MessageBox.Show($"{SelectedContact.ContactName} is not a member of Flash. Invite them to join.");
                }
                OnPropertyChanged(nameof(SelectedContact));
            }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(nameof(Message)); }
        }

        private object currentVM;
        public object CurrentVM
        {
            get { return currentVM; }
            set { currentVM = value; OnPropertyChanged(nameof(CurrentVM));  }
        }
        #endregion

        #region User Controls
        public ProfileUserControl profileUC = new();
        public AddContactUserControl addContactUC = new();
        public ContactsUserControl contactsUC = new();
        #endregion

        #region Commands
        public ICommand swapUserControls { get; }
        public ICommand logoutCommand { get; }
        public ICommand sendMessageCommand { get; }
        public ICommand DeleteChatCommand { get; }
        #endregion

        private readonly FlashContext flashContext = new();

        public DashboardViewModel()
        {
            CUser = new();
            CUser.Id = Convert.ToInt32(App.Current.Properties["userid"]);
            CUser = flashContext.Users.Find(CUser.Id);
            //CurrentVM = profileUC;
            swapUserControls = new RelayCommand(SwapUC);
            logoutCommand = new RelayCommand(Logout);
            DeleteChatCommand = new RelayCommand(deleteChat, canDeleteChat);
            sendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);
            Contacts = new();
            foreach(var i in flashContext.Contacts.ToList())
            {
                if (i.OwnerId == CUser.Id)
                {
                    Contacts.Add(new Contact()
                    {
                        ContactName = i.ContactName,
                        Mobile = i.Mobile,
                        Address = i.Address,
                        ImageUrl = i.ImageUrl
                    });
                }
            }
            flashContext.SaveChanges();
        }


        #region Commands Implementation

        private bool canDeleteChat(object arg)
        {
            return SelectedContact != null;
        }

        private void deleteChat(object obj)
        {
            flashContext.Messages.RemoveRange((from i in flashContext.Messages
                                               where ((i.SenderId == CUser.Id && i.ReceiverId == selecteduser.Id) || (i.ReceiverId == CUser.Id && i.SenderId == selecteduser.Id))
                                               select i)
                );
            flashContext.SaveChanges();
        }

        private void SendMessage(object obj)
        {
            flashContext.Messages.Add(new Message()
            {
                SenderId = CUser.Id,
                ReceiverId = selecteduser.Id,
                MessageBody = Message,
                SentTime = DateTime.Now.ToString("MMM dd, yyyy hh:mm tt"),
            });
            flashContext.SaveChanges();
            Message = "";
            SelectedContact = SelectedContact;
        }
        
        private bool CanSendMessage(object obj)
        {
            if (selecteduser==null || string.IsNullOrEmpty(Message))
                return false;
            else
                return true;
        }

        private void Logout(object para)
        {
            User? currentuser = flashContext.Users.FirstOrDefault(u => u.Id.ToString() == App.Current.Properties["userid"].ToString());
            if (currentuser != null)
            {
                currentuser.IsLoggedIn = false;
                flashContext.SaveChanges();
            }
            MainWindow window = new();
            window.Show();
            App.Current.Windows[0].Close();
        }

        private void SwapUC(object para)
        {
            if (para.ToString() == "addnew")
            {
                CurrentVM = addContactUC;
            }
            else if(para.ToString() == "profile")
            {
                CurrentVM = profileUC;
            }
            else if(para.ToString() == "contacts")
            {
                contactsUC = new();
                CurrentVM = contactsUC;
            }
        }

        #endregion
    }
}
