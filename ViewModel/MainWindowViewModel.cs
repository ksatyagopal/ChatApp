using ChatApp.Commands;
using ChatApp.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatApp.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        private object currentVM;
        public object CurrentVM
        {
            get { return currentVM; }
            set { currentVM = value; OnPropertyChanged(nameof(CurrentVM)); }
        }

        public LoginUserControl luc = new();
        public RegisterUserControl ruc = new();

        public ICommand swapLoginRegister { get; }

        public MainWindowViewModel()
        {
            CurrentVM = luc;
            swapLoginRegister = new RelayCommand(SwapUserControls);
        }

        public void SwapUserControls(object para)
        {
            if(para.ToString() == "login")
            {
                CurrentVM = luc;
            }
            else
            {
                CurrentVM = ruc;
            }
        }
        
    }
}
