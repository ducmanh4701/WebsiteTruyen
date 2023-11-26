using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteTruyenOnline.Controllers
{
    public class HotStoryController : Controller
    {
        // GET: HotStory
        public ActionResult Index()
        {
            var dao = new StoryDao();
            ViewBag.HotStory = dao.ListHotStory(10000);
            ViewBag.ViewStory = dao.ListViewStory(5);
            return View();
        }
    }
}