using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;

namespace WebsiteTruyenOnline.Areas.admin.Controllers
{
    public class AuthorController : BaseController
    {
        // GET: admin/Author
        public ActionResult Index()
        {
            var list = new AuthorDao();
            var model = list.getAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TacGia tacgia)
        {
            if (ModelState.IsValid)
            {
                var dao = new AuthorDao();

                if (tacgia.Ten_TacGia == null)
                {
                    ModelState.AddModelError("", "Vui lòng điền tên tác giả");
                }
                else if (dao.CheckName(tacgia.Ten_TacGia) == true)
                {
                    ModelState.AddModelError("", "Đã có tác giả");
                }
                else
                {
                    var anew = new TacGia();
                    anew.Ten_TacGia = tacgia.Ten_TacGia;
                    anew.CreateTime = DateTime.Now;

                    long id = dao.Insert(anew);
                    if (id > 0)
                    {
//                        SetAlert("Thêm user thành công", "success");
                        return RedirectToAction("Index", "Author");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm tác giả thất bại");
                    }
                }

            }
            return View("Create");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var tacgia = new AuthorDao().ViewDetail(id);
            return View(tacgia);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TacGia tacgia)
        {
            if (ModelState.IsValid)
            {
                var dao = new AuthorDao();
                var result = dao.Update(tacgia);
                if (result)
                {
                    return RedirectToAction("Index", "Author");
                }
                else
                {
                    ModelState.AddModelError("", "Sửa tác giả thất bại");
                }
            }

            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            new AuthorDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}