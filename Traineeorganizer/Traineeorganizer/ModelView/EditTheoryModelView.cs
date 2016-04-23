using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Traineeorganizer.Command;
using Traineeorganizer.Infrastructure;
using Traineeorganizer.Model;

namespace Traineeorganizer.ModelView
{
    class EditTheoryModelView:BaseViewModel
    {
        #region Prop

        string tName;
        public string TName
        {
            get { return tName; }
            set
            {
                {
                    if (tName != value)
                        tName = value;
                    OnPropertyChanged("TName");
                }
            }
        }
        public List<string> ThemesList{ get; set;}

        string sTheme;
        public string SelectedTheme
        {
            get { return sTheme; }
            set
            {
                {
                    if (sTheme != value)
                        sTheme = value;
                    OnPropertyChanged("SelectedTheme");
                }
            }
        }

        Priority sPrior;
        public Priority SelectedPriority
        {
            get { return sPrior; }
            set 
            {
                {
                    if (sPrior != value)
                        sPrior = value;
                    OnPropertyChanged("SelectedPriority");
                }
            }
        }
        public string HeaderText { get; set; }

        bool sActive;
        public bool SelectedActive
        {
            get { return sActive; }
            set
            {
                {
                    if (sActive != value)
                        sActive = value;
                    OnPropertyChanged("SelectedActive");
                }
            }
        }

        TraineeContext context = new TraineeContext();
        int taskId;
        bool isEdit=false;

        public ICommand AcceptCommand { get; set; }
        
#endregion

        /// <summary>
        /// добавление объекта
        /// </summary>
        public EditTheoryModelView()
        {
            
            HeaderText = "Добавить задачу";

            GetThemesList();
            AcceptCommand = new CommandNP(arg => AcceptMethod());
        }

        /// <summary>
        /// Изменение объекта 
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="thName"></param>
        public EditTheoryModelView(TrTask tr, string thName)
        {
                
                HeaderText = "Изменить " + tr.Name;
                tName = tr.Name;
                sActive = tr.Active;
                sPrior = tr.Prior;
                sTheme = tr.Th.Name;

                isEdit = true;
                taskId = tr.TrTaskId;
                GetThemesList();
                AcceptCommand = new CommandNP(arg => AcceptMethod());
        }

        void GetThemesList()
        {
            ThemesList = new List<string>();
            ThemesList.AddRange(context.Themes.Select(x => x.Name));
        }

        #region Command
        void AcceptMethod()
        {
            bool act = SelectedActive;
            int tId=context.Themes.Where(x=>x.Name==SelectedTheme).FirstOrDefault().Id;
            if (isEdit)
            {
                TrTask temp=context.TrTasks.Where(i=>i.TrTaskId==taskId).FirstOrDefault();
                temp.Name = tName;
                temp.Prior = sPrior;
                temp.thId = tId;
                temp.Active = sActive;
            }
            else
            context.TrTasks.Add(new TrTask { Name = TName, thId = tId, Prior = SelectedPriority, Active = act });            
            
            context.SaveChanges();

            if (DelegateClass.RefreshDataGridHandler!=null)
            {
                DelegateClass.RefreshDataGridHandler();
            }
        }
        #endregion
    }
}
