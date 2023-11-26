using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteTruyenOnline.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var dao = new StoryDao();
            ViewBag.NewStory = dao.ListNewStory(8);
            ViewBag.HotStory = dao.ListHotStory(8);
            ViewBag.ViewStory = dao.ListViewStory(5);
            return View();
        }

        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var model = new CategoryDao().getAll();
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult StatusMenu()
        {
            var model = new StatusDao().ListAll();
            return PartialView(model);
        }

    }
}