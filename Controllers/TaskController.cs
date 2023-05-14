using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TaskList()
        {
            List<Task> taskList = new List<Task>();
            Task task1 = new Task();
            task1.Id = 1;
            task1.Title = "prviTask";
            task1.Owner = "Tara"; 
            task1.UserStory = new UserStory();
            taskList.Add(task1);

            return View(taskList);
        }
    }
}