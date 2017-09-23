using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitlabVisualizer.Models
{
    public class Issue
    {
        public int Id;
        public int IssueId;
        public int ProjectId;
        public string Title;
        public string Description;
        public string[] Labels;
        public Milestone Milestone;
        public Assignee Assignee;
        public Author Author;
        public string State;
        public DateTime CreatedAt;
        public DateTime UpdatedAt;
    }
}
