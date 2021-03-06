﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traineeorganizer.Model;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Traineeorganizer.Command;
using System.Windows;
using Traineeorganizer.View;
using Traineeorganizer.Infrastructure;

namespace Traineeorganizer.ModelView
{
    class AllItViewModel:BaseViewModel
    {
        #region Prop
        AllTasks at;
        public DataTable AMVDateTable 
        { get{return at.amvDateTable;}
            set
            {
               
                if(at.amvDateTable!=value)
                    at.amvDateTable=value;
                OnPropertyChanged("AMVDateTable");
            }
        }


        #region CheckBox

        public bool ChBTh
        {
            get { 
                return at.chBTh; }
            set 
            {
                if (at.chBTh!=value)
                {
                    at.chBTh = value;
                    OnPropertyChanged("ChBTh");
                }
            }
        }

        public bool ChBPr
        {
            get
            {
                return at.chBPr;
            }
            set
            {
                if (at.chBPr != value)
                {
                    at.chBPr = value;
                    OnPropertyChanged("ChBPr");
                }
            }
        }

        public bool ChBAc
        {
            get
            {
                return at.chBAc;
            }
            set
            {
                if (at.chBAc != value)
                {
                    at.chBAc = value;
                    OnPropertyChanged("ChBAc");
                }
            }
        }

        #endregion
        public List<string> ThemesList { get; set; }

        public string SelectedThem
        {
            get { return at.selectedThem; }
            set 
            {
                at.selectedThem = value;
                OnPropertyChanged("SelectedThem");
            }
        }
       
        public Priority SelectedPrior
        {
            get { return at.selectedPrior; }
            set 
            {
                at.selectedPrior = value;
                OnPropertyChanged("SelectedPrior");
            }
        }

        public string SelectedActive
        {
            get { return at.selectedActive; }
            set 
            {
                at.selectedActive = value;
                OnPropertyChanged("SelectedActive");
            }
        }

        public int DGPsSelectedIndex
        {
            get 
            {
                return at.dgPsSelectedIndex; 
            }
            set
            {
                if (at.dgPsSelectedIndex!= value)
                 at.dgPsSelectedIndex = value;
                OnPropertyChanged("DGPsSelectedIndex");
            }
        }

        public ICommand filterCommand { get; set; }
        public ICommand clearFilterCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }
        public ICommand EditTaskCommand { get; set; }
        public ICommand RemoveTaskCommand { get; set; }

        public ICommand ChangeStatusCommand { get; set; }
        #endregion

        public AllItViewModel(AllTasks task)
        {

            filterCommand = new CommandNP(arg => FilterMethod());
            clearFilterCommand = new CommandNP(arg => ClearFilterMethod());
            AddTaskCommand = new CommandNP(arg => AddTaskMethod());
            EditTaskCommand = new CommandNP(arg => EditTaskMethod());
            RemoveTaskCommand = new CommandNP(arg => RemoveTaskMethod());
            ChangeStatusCommand = new CommandNP(arg => ChangeTaskStatusMethod());

            at = task;

            CreateDataTable();
            CreateLists(true);

            DelegateClass.RefreshDataGridHandler = new DelegateClass.RefreshDataGrid(ClearFilterMethod);
  
        }
       
        void CreateDataTable()
        {
            AMVDateTable = new DataTable();
            AMVDateTable.Columns.Add("Имя");
            AMVDateTable.Columns.Add("Тема");
            AMVDateTable.Columns.Add("Всего очков", typeof(int));
            AMVDateTable.Columns.Add("Приоритет", typeof(Priority));
            AMVDateTable.Columns.Add("Статус", typeof(bool));
            AMVDateTable.Columns.Add("Id", typeof(int));
        }
        void CreateLists(bool all)
        {
            using (TraineeContext tr=new TraineeContext())
            {
                tr.TrTasks.Load();
                tr.Themes.Load();
                var temp=tr.TrTasks.Local;
                ThemesList = new List<string>();

                foreach (var item in tr.Themes)
                {
                    ThemesList.Add(item.Name);
                }

                if (!all)
                {
                    FilterListCreate(tr);
                }
               

                else
                    FillDataTable(temp);
            }
        }

        void FilterListCreate(TraineeContext tr)
        {
            IEnumerable<TrTask> temp2=tr.TrTasks.Where(p=>true);

            if(ChBTh)
                temp2 = tr.TrTasks.Where(p => p.Th.Name == SelectedThem);

            if(ChBPr)
                temp2 = temp2.Where(p => p.Prior == SelectedPrior);
           
            if (ChBAc)
            {
                bool d = SelectedActive.Contains("Активно") ? true : false;
                temp2 = temp2.Where(p => p.Active == d);
            }


            FillDataTable(temp2);
        }

        void FillDataTable(IEnumerable<TrTask> t)
        {
            AMVDateTable.Rows.Clear();
            foreach (var tmp in t)
            {
                DataRow row = AMVDateTable.NewRow();
                row[0] = tmp.Name;
                row[1] = tmp.Th.Name;
                row[2] = tmp.Th.Points;
                row[3] = tmp.Prior;
                row[4] = tmp.Active;
                row[5] = tmp.TrTaskId;
                AMVDateTable.Rows.Add(row);
            }
        }

        void ChBToFalse()
        {
            this.ChBAc = false;
            this.ChBPr = false;
            this.ChBTh = false;
        }

        /// <summary>
        /// Проверка на изменение статуса
        /// </summary>
        /// <param name="val">DataTable из View</param>
        /// <returns></returns>


        #region Methods
        void FilterMethod()
        {
            CreateLists(false);
        }

        /// <summary>
        /// Пересоздает список тасков и заново заполняет DataGrid
        /// </summary>
        void ClearFilterMethod()
        {
            CreateLists(true);
            ChBToFalse();
            
        }

        void AddTaskMethod()
        {
            EditTheory View = new EditTheory();
            EditTheoryModelView modelView = new EditTheoryModelView();
            View.DataContext = modelView;
            View.ShowDialog();

        }

        void EditTaskMethod()
        {
            TrTask t;
            string thName;
            using(TraineeContext cnt=new TraineeContext())
            {
                int i=Convert.ToInt32(AMVDateTable.Rows[DGPsSelectedIndex]["Id"]);
                t = cnt.TrTasks.Where(x => x.TrTaskId ==i ).Include(tt=>tt.Th).FirstOrDefault();
                thName=t.Th.Name;
            }
            EditTheory View = new EditTheory();
            EditTheoryModelView modelView = new EditTheoryModelView(t, thName);
            View.DataContext = modelView;
            View.ShowDialog();
        }
        void RemoveTaskMethod()
        {
            DGPsSelectedIndex = 0;
            //RemoveMethod - wait for further realization
        }

        /// <summary>
        /// Изменяет статус задачи
        /// </summary>
        void ChangeTaskStatusMethod()
        {
            if (DGPsSelectedIndex < AMVDateTable.Rows.Count)
           {
                using (TraineeContext trC = new TraineeContext())
                {
                    string name = AMVDateTable.Rows[DGPsSelectedIndex][0].ToString();
                    TrTask tr = trC.TrTasks.Where(
                        x => x.Name == name).FirstOrDefault();

                    tr.Active = !tr.Active;
                    trC.SaveChanges();
                }
                ClearFilterMethod();
           }
        }

        #endregion
    }
}
