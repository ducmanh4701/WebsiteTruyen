using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteTruyenOnline.Common;

namespace WebsiteTruyenOnline.Controllers
{
    public class StoryController : Controller
    {
        // GET: Story
        public ActionResult Index(long? id)
        {
            var story = new StoryDao().ViewDetail(id);
            ViewBag.Chuongtruyen = new StoryDao().getAllContentByID(id);
            return View(story);
        }

        public ActionResult DocTruyen(int id_chuong)
        {
            var model = new StoryDao().ViewDetailContent(id_chuong);
            ViewBag.Truyen = new StoryDao().ViewDetail(model.Id_Truyen);
            ViewBag.Chuongtruyen = new StoryDao().getAllContentByID(model.Id_Truyen);
            Comment(model.Id_Truyen);
            return View(model);
        }
        [ChildActionOnly]
        public ActionResult Comment(long? id)
        {
            var model = new StoryDao().ListAllComment(id);
            return PartialView(model);
        }
    }
}