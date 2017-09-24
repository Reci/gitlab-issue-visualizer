using Newtonsoft.Json;
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
        public int IId;
        [JsonProperty(PropertyName = "project_id")]
        public int ProjectId;
        public string Title;
        public string Description;
        [JsonProperty(PropertyName = "due_date")]
        public string DueDate;
        public string State;
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt;
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt;
    }
}
