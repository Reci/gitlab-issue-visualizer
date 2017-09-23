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
            model.Users = client.GetUsers();
            model.Projects = client.GetProjects();
            model.Issues = client.GetIssues();
            model.Year = DateTime.Now.Year;
            model.Month = DateTime.Now.Month;
            return PartialView("_timetableContent", model);
        }

    }
}