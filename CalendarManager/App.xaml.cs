using CalendarManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace CalendarManager
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
            MainViewModel mainViewModel = new MainViewModel();
            string filepath = "notes.json";

            if (!File.Exists(filepath))
            {
                File.WriteAllText(filepath, "[]");
            }

            base.OnStartup(e);
            mainViewModel.SelectedDate = DateTime.Today;
            window.DataContext = mainViewModel;
            window.Show();
        }
    }
}
