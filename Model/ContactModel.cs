using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Model
{
    public class ContactModel: ModelBase
    {
        private int id;
        private int ownerId;
        private string contactName;
        private string mobile;
        private string address;
        private string imageUrl;

        public int Id 
        { 
            get { return id; } 
            set { id = value; OnPropertyChanged(nameof(Id)); } 
        }
        public int OwnerId 
        {
            get { return ownerId; }
            set { ownerId = value; OnPropertyChanged(nameof(OwnerId)); }
        }

        public string ContactName
        {
            get { return contactName; }
            set { contactName = value; OnPropertyChanged(nameof(ContactName)); }
        }
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; OnPropertyChanged(nameof(Mobile)); }
        }
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(nameof(Address)); }
        }
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; OnPropertyChanged(nameof(ImageUrl)); }
        }

    }
}
