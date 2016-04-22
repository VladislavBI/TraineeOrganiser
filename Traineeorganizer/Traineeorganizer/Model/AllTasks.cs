using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traineeorganizer.Model
{
    class AllTasks
    {
        public DataTable amvDateTable;
        public bool chBTh;
        public bool chBPr;
        public bool chBAc;
        public string selectedThem;
        public Priority selectedPrior;
        public string selectedActive;
        public int dgPsSelectedIndex;
        public AllTasks()
        {
            chBTh = false;
            chBPr = false;
            chBAc = false;
            dgPsSelectedIndex = 0;
        }
    }
}
