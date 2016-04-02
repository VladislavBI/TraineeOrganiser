using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traineeorganizer.Model;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace Traineeorganizer.ModelView
{
    class AllItViewModel:BaseViewModel
    {
        public DataTable AMVDateTable { get; set; }

        private string selectedThem;
        public string SelectedThem
        {
            get { return selectedThem; }
            set 
            {
                selectedThem = value;
                OnPropertyChanged("Them");
            }
        }
       
        private Priority selectedPrior;
        public Priority SelectedPrior
        {
            get { return selectedPrior; }
            set 
            {
                selectedPrior = value;
                OnPropertyChanged("Prior");
            }
        }

        private string selectedActive;
        public string SelectedActive
        {
            get { return selectedActive; }
            set 
            {
                selectedActive = value;
                OnPropertyChanged("Active");
            }
        }
          
        
        public AllItViewModel()
        {
            AMVDateTable = new DataTable();
            AMVDateTable.Columns.Add("Имя");
            AMVDateTable.Columns.Add("Тема");
            AMVDateTable.Columns.Add("Всего очков", typeof(int));
            AMVDateTable.Columns.Add("Приоритет", typeof(Priority));
            AMVDateTable.Columns.Add("Статус", typeof(bool));

            CreateList(true);
        }

        void CreateList(bool all)
        {
            using (TraineeContext tr=new TraineeContext())
            {
                tr.TrTasks.Load();
                tr.Themes.Load();
                var temp=tr.TrTasks.Local;
               /* if (!all)
                {
                    temp=temp.Where(p=>p.Th.Name==)
                }*/

                FillDataTable(temp);
            }
        }

        void FillDataTable(ObservableCollection<TrTask> t)
        {
           foreach (var tmp in t)
           {
               DataRow row = AMVDateTable.NewRow();
               row[0] = tmp.Name;
               row[1] = tmp.Th.Name;
               row[2] = tmp.Th.Points;
               row[3] = tmp.Prior;
               row[4] = tmp.Active;
               AMVDateTable.Rows.Add(row);
           }
        }
    }
}
