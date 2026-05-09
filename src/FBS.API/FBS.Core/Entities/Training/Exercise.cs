using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBS.Core.Entities.Training
{
    public class Exercise
    {
        public string Name { get; set; } = string.Empty;
        public int? Sets { get; set; }
        public int? Reps { get; set; }
    }
}
