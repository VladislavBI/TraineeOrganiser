using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traineeorganizer.Infrastructure
{
    public static class DelegateClass
    {
        /// <summary>
        /// Перезаполнение DataGrid
        /// </summary>
        public delegate void RefreshDataGrid();
        public static RefreshDataGrid RefreshDataGridHandler;
    }
}
