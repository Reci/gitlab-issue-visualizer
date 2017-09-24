using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitlabVisualizer.Models
{
    public class Project
    {
        public int Id;
        public string Description;
        [JsonProperty(PropertyName = "default_branch")]
        public string DefaultBranch;
        [JsonProperty(PropertyName = "tag_list")]
        string[] TagList;
        public bool Public;
        public bool Archived;
        [JsonProperty(PropertyName = "visibility_level")]
        public int VisibilityLevel;
        [JsonProperty(PropertyName = "http_url_to_repo")]
        public string HttpUrlToRepo;
        [JsonProperty(PropertyName = "ssh_url_to_repo")]
        public string SshUrlToRepo;
        [JsonProperty(PropertyName = "web_url")]
        public string WebUrl;
        public string Name;
        [JsonProperty(PropertyName = "name_with_namespace")]
        public string NameWithNamespace;
        public string Path;
        [JsonProperty(PropertyName = "path_with_namespace")]
        public string PathWithNamespace;
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt;
        //...
    }
}
