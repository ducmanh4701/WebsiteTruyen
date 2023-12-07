using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using Model.ViewModel;
using WebsiteTruyenOnline.Areas.admin.Model;

namespace WebsiteTruyenOnline.Areas.admin.Controllers
{
    public class StoryController : BaseController
    {
        // GET: admin/Story
        public ActionResult Index()
        {
            var list = new StoryDao();
            var model = list.getAll();
            return View(model);
        }
        public ActionResult Content(int id)
        {
            var list = new StoryDao();
            List<ChuongTruyen> model = new List<ChuongTruyen>();
            model = list.getAllContentByID(id);
            ViewBag.id_truyen = id;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(StoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new StoryDao();

                if (model.Name == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập tên truyện");
                }
                else if (model.NameAuthor == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập tên tác giả");
                }
                else if (model.NameCategory == null)
                {
                    ModelState.AddModelError("", "Vui lòng chọn thể loại");
                }
                else if (model.Status == null)
                {
                    ModelState.AddModelError("", "Vui lòng chọn trạng thái");
                }
                else if (model.GioiThieu == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập giới thiệu truyện");
                }
                else if (model.Avt == null)
                {
                    ModelState.AddModelError("", "Vui lòng chọn avt truyện");
                }
                else if (dao.CheckName(model.Name))
                {
                    ModelState.AddModelError("", "Truyện đã tồn tại");
                }
                else if(model.MetaTitle == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập tên meta title");
                }
                else
                {
                    var story = new Truyen();
                    story.Ten_Truyen = model.Name;
                    story.GioiThieu_Truyen = model.GioiThieu;
                    story.Avt_Truyen = model.Avt;
                    story.MetaTitle = model.MetaTitle;
                    long id = dao.Insert(story, model.NameAuthor, model.NameCategory, model.Status);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Story");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm truyện thất bại");
                    }
                }

            }
            return View("Create");
        }
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            var story = new StoryDao().ViewDetail(id);
            return View(story);
        }
        [HttpPost]
        public ActionResult Edit(StoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new StoryDao();
                var story = new Truyen();
                story.Ten_Truyen = model.Name;
                story.GioiThieu_Truyen = model.GioiThieu;
                story.Avt_Truyen = model.Avt;
                story.MetaTitle = model.MetaTitle;
                bool res = dao.Edit(story, model.NameAuthor, model.NameCategory, model.Status, model.ID);
                if (res)
                {
                    return RedirectToAction("Index", "Story");
                }
                else
                {
                    ModelState.AddModelError("", "Sửa truyện thất bại");
                }


            }
            return View("Edit");
        }
        public ActionResult Delete(int id)
        {
            new StoryDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateContent(long id)
        {
            var model = new ChuongTruyen() { Id_Truyen = id };
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateContent(ChuongTruyen chuongtruyen)
        {
            if (ModelState.IsValid)
            {
                var dao = new StoryDao();
                if (chuongtruyen.Ten_Chuong == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập tên chương");
                }
                else if (chuongtruyen.MetaTitle == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập metatitle");
                }
                else if (chuongtruyen.NoiDung_Chuong == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập nội dung chương");
                }
                else
                {
                    var res = dao.InsertContent(chuongtruyen);

                    if (res > 0)
                    {
                        return RedirectToAction("Content", "Story", new { id = chuongtruyen.Id_Truyen });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm truyện thất bại");
                    }
                }
            }
            return View("CreateContent");
        }
        [HttpGet]
        public ActionResult EditContent(int id)
        {
            var content = new StoryDao().ViewDetailContent(id);
            return View(content);
        }
        [HttpPost]
        public ActionResult EditContent(ChuongTruyen entity)
        {
            if (ModelState.IsValid)
            {
                var dao = new StoryDao();
                var story = new ChuongTruyen();
                story.Ten_Chuong = entity.Ten_Chuong;
                story.MetaTitle = entity.MetaTitle;
                story.NoiDung_Chuong = entity.NoiDung_Chuong;

                bool res = dao.EditContent(entity);
                if (res)
                {
                    return RedirectToAction("Content", "Story", new { id = entity.Id_Truyen });
                }
                else
                {
                    ModelState.AddModelError("", "Sửa chương truyện thất bại");
                }
            }
            return View();
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {

            file.SaveAs(Server.MapPath("~/DATA/img_AvtStory/" + file.FileName));
            return "/DATA/img_AvtStory/" + file.FileName;
        }


    }
}