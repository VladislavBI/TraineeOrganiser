using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traineeorganizer.ModelView
{
    /// <summary>
    /// Содержит все ViewModel Главного экрана. К нему происходит binding
    /// </summary>
    class MainViewModel
    {
        public AllItViewModel altVM { get; set; }

        public MainViewModel(AllItViewModel AMV)
        {
            altVM = AMV;
        }
    }
}
