using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Traineeorganizer.Model;
using Traineeorganizer.ModelView;

namespace Traineeorganizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
          void OnStartup(object sender, StartupEventArgs e)
        {

            MainWindow view = new MainWindow();
            AllItViewModel alt=new AllItViewModel(new AllTasks()); 
	        
            
            MainViewModel viewModel = new MainViewModel(alt);
            view.DataContext = viewModel;
            view.Show();
        }
    }
}
