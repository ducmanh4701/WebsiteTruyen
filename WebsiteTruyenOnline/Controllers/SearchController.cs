using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace WebsiteTruyenOnline.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string searchString)
        {
            var dao = new StoryDao();
            ViewBag.HotStory = dao.ListHotStory(8);
            ViewBag.ViewStory = dao.ListViewStory(5);
            var model = new SearchDao();
            ViewBag.ViewListSearchDetail = model.getSearchBook(searchString);
            ViewBag.ViewSearchString = searchString;
            return View();
        }
    }
}