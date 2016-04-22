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
        public string TName { get; set; }
        public List<string>  ThemesList { get; set; }
        public string SelectedTheme { get; set; }
        public Priority SelectedPriority { get; set; }
        public string HeaderText { get; set; }

        TraineeContext context = new TraineeContext();
        TrTask tTask;


        public ICommand AcceptCommand { get; set; }
        
#endregion

        public EditTheoryModelView(TrTask tr=null)
        {
            if (tr!=null)
            {
                tTask = tr;
                HeaderText = "Изменить " + tr.Name;
            }
            else
            {
                HeaderText = "Добавить задачу";
            }
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
            int tId=context.Themes.Where(x=>x.Name==SelectedTheme).FirstOrDefault().Id;
            if (tTask==null)
            {
                context.TrTasks.Add(new TrTask { Name = TName, thId = tId, Prior = SelectedPriority });
            }
            context.SaveChanges();

            if (DelegateClass.RefreshDataGridHandler!=null)
            {
                DelegateClass.RefreshDataGridHandler();
            }
        }
        #endregion
    }
}
