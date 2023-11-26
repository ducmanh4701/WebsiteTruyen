using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteTruyenOnline.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(int id_theloai)
        {
            var model = new StoryDao().ListStoryByIDCategory(id_theloai);
            ViewBag.ViewStory = new StoryDao().ListViewStory(5);
            ViewBag.Category = new CategoryDao().ViewDetail(id_theloai);
            return View(model);
        }
    }
}