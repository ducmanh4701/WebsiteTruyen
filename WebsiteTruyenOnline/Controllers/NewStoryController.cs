using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteTruyenOnline.Controllers
{
    public class NewStoryController : Controller
    {
        // GET: NewStory
        public ActionResult Index()
        {
            var dao = new StoryDao();
            ViewBag.NewStory = dao.ListNewStory(100000);
            ViewBag.ViewStory = dao.ListViewStory(5);
            return View();
        }
    }
}