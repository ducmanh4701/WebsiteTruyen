using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;

namespace WebsiteTruyenOnline.Areas.admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: admin/Category
        public ActionResult Index()
        {
            var list = new CategoryDao();
            var model = list.getAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TheLoai theloai)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                if(theloai.Ten_TheLoai == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập thể loại.");
                }
                else if (dao.CheckName(theloai.Ten_TheLoai) == true)
                {
                    ModelState.AddModelError("", "Đã có thể loại.");
                }
                else
                {
                    var theloainew = new TheLoai();
                    theloainew.Ten_TheLoai = theloai.Ten_TheLoai;
                    theloainew.CreateTime = DateTime.Now;
                    
                    long id = dao.Insert(theloainew);
                    if (id > 0)
                    {
                        SetAlert("Thêm user thành công", "success");
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm thể loại thất bại");
                    }
                }

            }
            return View("Create");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cat = new CategoryDao().ViewDetail(id);
            return View(cat);
        }
        [HttpPost]
        public ActionResult Edit(TheLoai theloai)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                var result = dao.Update(theloai);
                if (result)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thể loại thất bại");
                }
            }
            return View("Index");
        }
        public ActionResult Delete(int id)
        {
            new CategoryDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}