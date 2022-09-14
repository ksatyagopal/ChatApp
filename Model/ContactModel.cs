using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Model
{
    public class ContactModel: ModelBase
    {
        private string contactName;
        private string mobile;
        private string address;
        private string imageUrl;

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
