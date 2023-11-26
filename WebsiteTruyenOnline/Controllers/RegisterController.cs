using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteTruyenOnline.Common;

using System.Web.Mvc;
using Model.Dao;
using Model.EF;

namespace WebsiteTruyenOnline.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user)
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
                else
                /*else if (user.status == false)
                {
                    ModelState.AddModelError("", "Vui lòng chọn trạng thái tài khoản.");
                }
                else*/
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
                    Session.Add(CommonConstants.USER_SESSION, unew);
                    long id = dao.Insert(unew);
                    if (id > 0)
                    {

                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm tài khoan không thành công");
                    }
                }
            }
            return View("Index");
        }
    }
}