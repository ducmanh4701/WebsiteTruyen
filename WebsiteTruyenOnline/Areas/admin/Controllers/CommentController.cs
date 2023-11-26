using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteTruyenOnline.Areas.admin.Controllers
{
    public class CommentController : BaseController
    {
        // GET: admin/Comment
        public ActionResult Index()
        {
            var list = new CommentDao();
            var model = list.getAll();
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            new CommentDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}