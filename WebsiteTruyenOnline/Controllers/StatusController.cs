using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteTruyenOnline.Controllers
{
    public class StatusController : Controller
    {
        // GET: Status
        public ActionResult Index(int id_trangthai)
        {
            var model = new StoryDao().ListStoryByIDStatus(id_trangthai);
            ViewBag.ViewStory = new StoryDao().ListViewStory(5);
            ViewBag.Status = new StatusDao().ViewDetail(id_trangthai);
            return View(model);
        }
    }
}