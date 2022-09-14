using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public class UserSetting: INotifyPropertyChanged
    {
		private string mode;

		public string Mode
		{
			get { return mode; }
			set { mode = value; OnPropertyChanged(nameof(Mode)); }
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		public void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}
	}
}
