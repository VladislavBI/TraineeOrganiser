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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Traineeorganizer.Model;
using MohammadDayyanCalendar;

namespace Traineeorganizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (TraineeContext tr = new TraineeContext())
            {
                
                /*Theme th2 = new Theme { Name = "Th2" };
                
                tr.Themes.Add(th2);
                tr.SaveChanges();

                TrTask t = new TrTask {  Th=th2 };
                TrTask t1 = new TrTask {  Th = th2 };
                TrTask t2 = new TrTask {  Th = th2 };

                tr.TrTasks.AddRange(new List<TrTask>{t, t1, t2});
                tr.SaveChanges();*/
                 
            }
        }
    }
}
