using GitlabVisualizer.Models;
using Newtonsoft.Json;
using NGitLab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GitlabVisualizer.ApiClient
{
    public class Client
    {
        string endpoint = System.Configuration.ConfigurationManager.AppSettings["GitlabApiEndpoint"];
        string privateToken = System.Configuration.ConfigurationManager.AppSettings["GitlabApiPrivateToken"];
        int maxItems = 1000; //max items on page
        GitLabClient client;

        public Client()
        {
            client = GitLabClient.Connect(endpoint, privateToken);
        }

        public Client(string endpoint, string privateToken)
        {
            client = GitLabClient.Connect(endpoint, privateToken);
        }

        public IEnumerable<User> GetUsers()
        {
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var json = client.DownloadString($"{endpoint}/users?per_page={maxItems}&private_token={privateToken}");
                IEnumerable<User> result = JsonConvert.DeserializeObject<List<User>>(json);
                return result;
            }
        }

        public IEnumerable<Project> GetProjects()
        {
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var json = client.DownloadString($"{endpoint}/projects?per_page={maxItems}&private_token={privateToken}");
                IEnumerable<Project> result = JsonConvert.DeserializeObject<List<Project>>(json);
                return result;
            }
        }

        public IEnumerable<Issue> GetIssues()
        {
            var projects = GetProjects();
            List<Issue> result = new List<Issue>();
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                foreach (var project in projects)
                {
                    var json = client.DownloadString($"{endpoint}/projects/{project.Id}/issues?per_page={maxItems}&private_token={privateToken}");
                    result.AddRange(JsonConvert.DeserializeObject<List<Issue>>(json));
                }
            }
            return result;
        }

        public IEnumerable<Issue> GetIssuesByProjectId(int projectId)
        {
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                var json = client.DownloadString($"{endpoint}/projects/{projectId}/issues?per_page={maxItems}&private_token={privateToken}");
                IEnumerable<Issue> result = JsonConvert.DeserializeObject<List<Issue>>(json);
                return result;
            }
        }
    }
}
