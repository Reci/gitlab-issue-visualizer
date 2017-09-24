using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitlabVisualizer.Models
{
 
    public class Author
    {
        public int Id;
        public string Email;
        public string Name;
        public string State;
        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl;
        [JsonProperty(PropertyName = "web_url")]
        public string WebUrl;
    }
}
