using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traineeorganizer.Model
{
    class Theme
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }

       // public ICollection<Task> Tasks { get; set; }

        public Theme()
        {
            //Tasks = new List<Task>();
            Points = 0;
        }
    }

   
}
