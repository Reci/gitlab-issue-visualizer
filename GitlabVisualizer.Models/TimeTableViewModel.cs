using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitlabVisualizer.Models
{
    public class TimeTableViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Issue> Issues { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
