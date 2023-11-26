using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using WebsiteTruyenOnline.Common;

namespace WebsiteTruyenOnline.Areas.admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: admin/User
        public ActionResult Index()
        {
            var list = new UserDao();
            var model = list.getAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }


        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                //var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                //user.Password = encryptedMd5Pas;
                if (dao.CheckUserName(user.UserName))
                {
                    ModelState.AddModelError("", "Tên tài khoản đã tồn tại");
                }
                else if (dao.CheckEmail(user.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else if (user.UserName == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập tài khoản.");
                }
                else if (user.Password == null)
                {
                    ModelState.AddModelError("", "Vui lòng nhập mật khẩu.");
                }
                else if (user.status == false)
                {
                    ModelState.AddModelError("", "Vui lòng chọn trạng thái tài khoản.");
                }
                else
                {
                    var unew = new User();
                    unew.HoTen = user.HoTen;
                    unew.GioiTinh = user.GioiTinh;
                    unew.NgaySinh = user.NgaySinh;
                    unew.SDT = user.SDT;
                    unew.Email = user.Email;
                    unew.UserName = user.UserName;
                    unew.Password = user.Password;
                    unew.CreateTime = DateTime.Now;

                    long id = dao.Insert(unew);
                    if (id > 0)
                    {
                        //SetAlert("Thêm user thành công", "success");
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm user không thành công");
                    }
                }
            }
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
               
                //var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                //user.Password = encryptedMd5Pas;

                var result =new  UserDao().Update(user);

                if (result)
                {
                    //SetAlert("Cập nhật user thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user không thành công");
                }
            }
            return View("Index");
        }

        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}