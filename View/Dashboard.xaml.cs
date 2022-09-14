using ChatApp.UserControls;
using ChatApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatApp.View
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        
        public Dashboard()
        {
            InitializeComponent();
        }

        private void wmode_Click(object sender, RoutedEventArgs e)
        {
            if(togglemode.IsChecked == false)
            {
                
                dockright.Background = new SolidColorBrush(Colors.White);
                dockleft.Background = new SolidColorBrush(Colors.DarkGray);
                
            }
            else
            {
                
                dockright.Background = new SolidColorBrush(Colors.DarkGray);
                dockleft.Background = new SolidColorBrush(Colors.DarkGray);
                
            }
        }

        private void reload_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new DashboardViewModel();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState != WindowState.Maximized)
            {
                //this.MaxHeight = SystemParameters.WorkArea.Height+10;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        
    }
}
