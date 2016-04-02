using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traineeorganizer.Model
{
    class TraineeContext : DbContext
    {
        public TraineeContext()
            : base("DbConnection")
        { }

        public DbSet<TrTask> TrTasks { get; set; }
        public DbSet<Theme> Themes { get; set; }
    }

}
