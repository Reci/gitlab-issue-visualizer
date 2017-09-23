using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitlabVisualizer.Models
{
    public class Project
    {
        public const string Url = "/projects";
        public string HttpUrl;
        public string SshUrl;
        public DateTime CreatedAt;
        public bool WikiEnabled;
        public bool WallEnabled;
        public bool MergeRequestsEnabled;
        public bool IssuesEnabled;
        public string PathWithNamespace;
        public string Path;
        public bool Public;
        public User Owner;
        public string DefaultBranch;
        public string Description;
        public string Name;
        public int Id;
    }
}
