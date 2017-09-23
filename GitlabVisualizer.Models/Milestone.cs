using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitlabVisualizer.Models
{
    
    public class Milestone
    {
        public int Id;
        public string Title;
        public string Description;
        public string DueDate;
        public string State;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;
    }
}
