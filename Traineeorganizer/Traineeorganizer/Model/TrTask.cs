using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traineeorganizer.Model
{
    public enum Priority
    {
        Low=1,
        Middle=2, 
        High=3
    }
    class TrTask
    {
       [Key]
        public int TrTaskId { get; set; }
        public string Name { get; set; }

        public Priority Prior { get; set; }
        public bool Active { get; set; }
        public int? thId { get; set; }
        public Theme Th { get; set; }

        public TrTask()
        {
            Active = false;
            Prior = Priority.Low;
            Active = false;
            Name = "";
        }
    }

}
