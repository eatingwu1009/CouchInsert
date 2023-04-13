using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VMS.TPS.Common.Model.API;

namespace CouchInsert
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            // TODO : Add here the code that is called when the script is launched from Eclipse.
            Window window = new Window();

            MainView mainView = new MainView();
            MainViewModel mainViewModel = new MainViewModel();
            mainView.DataContext = mainViewModel;

            window.Content = mainView;
            window.Title = "CouchInsert";
            window.Height = 350;
            window.Width = 400;
            window.Top = 400;
            window.Left = 1200;

            window.ShowDialog();
        }
    }
}
