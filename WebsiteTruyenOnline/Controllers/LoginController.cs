using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteTruyenOnline.Models;
using Model.Dao;
using WebsiteTruyenOnline.Common;
using Model.EF;

namespace WebsiteTruyenOnline.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, model.PassWord);
                //var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord));
                if (result == 1)
                {
                    var user = dao.GetById(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.ID = user.Id_User;

                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại.");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa.");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.");
                }
            }
            return View("Index");

        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
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
                    ModelState.AddModelError("", "Bạn chưa đồng ý với các điều khoản");
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
                    unew.status = true;
          //          Session.Add(CommonConstants.USER_SESSION, unew);
                    long id = dao.Insert(unew);
                    if (id > 0)
                    {

                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng kí thất bại");
                    }
                }
            }
            return View("Register");
        }
    }

}
