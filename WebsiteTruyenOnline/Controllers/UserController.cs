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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(int id)
        {
            

            var model = new UserDao().ViewDetail(id);
            return View(model);
        }
        public ActionResult Account_Info(int id)
        {
          
       
            var model = new UserDao().ViewDetail(id);
            return View(model);
        }
        public ActionResult Change_Password(int id)
        {

            var model = new UserDao().ViewDetail(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change_Password(User user)
        {
            if (ModelState.IsValid)
            {

                //var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                //user.Password = encryptedMd5Pas;

                var result = new UserDao().Update(user);

                if (result)
                {
                    //SetAlert("Cập nhật user thành công", "success");
                    return RedirectToAction("Change_Password", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user không thành công");
                }
            }
            return View("Change_Password","User");
        }
        public ActionResult Following_Story(int id)
        {
            var model = new UserDao().ViewDetail(id);
            return View(model);
            
        }
        
    }
}