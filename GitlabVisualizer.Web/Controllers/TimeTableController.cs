using GitlabVisualizer.ApiClient;
using GitlabVisualizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GitlabVisualizer.Web.Controllers
{
    public class TimeTableController : Controller
    {
        private Client client = new Client();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUsers()
        {
            var users = client.GetUsers();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProjects()
        {
            var projects = client.GetProjects();
            return Json(projects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTimetableContent(int? employeeId, int? projectId)
        {
            TimeTableViewModel model = new TimeTableViewModel();
            model.Year = DateTime.Now.Year;
            model.Month = DateTime.Now.Month;

            DateTime PeriodStart = new DateTime(model.Year, model.Month, 1);
            DateTime PeriodEnd = new DateTime(model.Year, model.Month, DateTime.DaysInMonth(model.Year, model.Month));

            model.Users = client.GetUsers();
            model.Projects = client.GetProjects();
            var issues = client.GetIssues();
            if (projectId != null && projectId != -1)
            {
                issues = issues.Where(i => i.ProjectId == projectId);
            }
            if (employeeId != null && employeeId != -1)
            {
                issues = issues.Where(i => (i.Assignee != null && i.Assignee.Id == employeeId) 
                || 
                (i.Assignee == null && i.Author.Id == employeeId))
                .AsEnumerable();
            }

            foreach (var issue in issues)
            {
                if (issue.State == "closed") {
                    issue.CloseDate = issue.UpdatedAt.Date;
                }
            }
            issues = issues.Where(i => (i.CreatedAt >= PeriodStart && i.CreatedAt <= PeriodEnd) // created at current period
            || (i.CloseDate != null && i.CloseDate > PeriodStart && i.CloseDate < PeriodEnd) //closed in current period
            || (i.CloseDate == null) //not closed yet
            ).AsEnumerable(); 
            model.Issues = issues;
            
            return PartialView("_timetableContent", model);
        }

    }
}