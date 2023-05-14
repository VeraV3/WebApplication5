using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class UserStoryController : Controller
    {
        // GET: UserStory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserStoryList()
        {
            UserStory userStory = new UserStory();
            userStory.Id = 1;
            userStory.Title = "Kreiranje korisnicke autentifikacije";
            userStory.Owner = "Tarasita";
           // userStory.Description = "Kreiraj korisnicku autentifikaciju";
            userStory.Priority = "Middle";
            userStory.Done = false;
            userStory.Tasks = new List<Task>
           {
                    new Task
                    {
                        Id = 1,
                        Title = "Implementacija modela korisnika",
                        Owner = "Bojan",
                        UserStory = userStory
                    },
                    new Task
                    {
                        Id = 2,
                        Title = "Implementacija prijave korisnika",
                        Owner = "Vera",
                        UserStory = userStory
                    }
            };
             
            List<UserStory> storyList = new List<UserStory>();
            storyList.Add(userStory);
            return View(storyList);
        }
    }
}