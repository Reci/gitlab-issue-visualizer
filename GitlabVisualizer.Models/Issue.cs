using Newtonsoft.Json;
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
        public int IId; //issue Id
        [JsonProperty(PropertyName = "project_id")]
        public int ProjectId;
        public string Title;
        public string Description;
        public string[] Labels;
        public Milestone Milestone;
        public Assignee Assignee;
        public Author Author;
        [JsonProperty(PropertyName = "user_notes_count")]
        public int UserNotesCount;
        public string State;
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt;
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt;
        [JsonProperty(PropertyName = "due_date")]
        public DateTime? DueDate;
        public int Upvotes;
        public int Downvotes;
        public DateTime? CloseDate;
    }
}
