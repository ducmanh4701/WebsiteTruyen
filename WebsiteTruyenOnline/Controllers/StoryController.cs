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
            var chuongTruyenEntities = new StoryDao().getAllContentByID(id) ?? new List<Model.EF.ChuongTruyen>();

            Guid anonymousId = Guid.Empty;
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                anonymousId = Guid.Parse(reqCookies["anonymousId"].ToString());
            }
            var histories = new StoryDao().GetListHistoryByTruyenId(id, anonymousId) ?? new List<Model.EF.History>();
            var chuongTruyens = chuongTruyenEntities.Select(m => new Model.ViewModel.ChuongTruyenViewModel
            {
                DisplayOrder = m.DisplayOrder,
                Id_Chuong = m.Id_Chuong,
                Id_Truyen = m.Id_Truyen,
                MetaTitle = m.MetaTitle,
                NoiDung_Chuong = m.NoiDung_Chuong,
                Ten_Chuong = m.Ten_Chuong,
                IsRead = histories.Any(z => z.ChuongTruyenId == m.Id_Chuong && z.TruyenId == m.Id_Truyen)
            }).ToList();
            story.CurrentChuongId = histories.OrderByDescending(m => m.CreatedAt).FirstOrDefault()?.ChuongTruyenId ?? null;
            if(story.CurrentChuongId.HasValue)
            {
                story.CurrentMetaTitle = chuongTruyens.FirstOrDefault(m => m.Id_Chuong == story.CurrentChuongId)?.MetaTitle;
            }
            
            if(Session[WebsiteTruyenOnline.Common.CommonConstants.USER_SESSION] != null)
            {
                var Users = (WebsiteTruyenOnline.Common.UserLogin)Session[WebsiteTruyenOnline.Common.CommonConstants.USER_SESSION];
                if(Users != null)
                {
                    var follow = new StoryDao().GetFollowByTruyenUser(story.ID, Users.ID);
                    story.HasFollow = follow != null;
                }
            }
            ViewBag.Chuongtruyen = chuongTruyens;
            return View(story);
        }

        public ActionResult DocTruyen(int id_chuong)
        {
            var model = new StoryDao().ViewDetailContent(id_chuong);
            ViewBag.Truyen = new StoryDao().ViewDetail(model.Id_Truyen);
            ViewBag.Chuongtruyen = new StoryDao().getAllContentByID(model.Id_Truyen);
            Comment(model.Id_Truyen);
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            var anonymousId = Guid.Empty;
            if (reqCookies == null)
            {
                anonymousId = Guid.NewGuid();
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo["anonymousId"] = anonymousId.ToString();
                userInfo.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(userInfo);
            }
            else
            {
                anonymousId = Guid.Parse(reqCookies["anonymousId"].ToString());
            }
            new StoryDao().CreateHistory(model.Id_Truyen.Value, model.Id_Chuong, anonymousId);

            return View(model);
        }
        [ChildActionOnly]
        public ActionResult Comment(long? id)
        {
            var model = new StoryDao().ListAllComment(id);
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Follow()
        {
            if (Session[WebsiteTruyenOnline.Common.CommonConstants.USER_SESSION] != null)
            {
                var Users = (WebsiteTruyenOnline.Common.UserLogin)Session[WebsiteTruyenOnline.Common.CommonConstants.USER_SESSION];
                if(Users != null)
                {
                    var results = new StoryDao().GetFollowByUsers(Users.ID);
                    return View(results);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Follow(long truyenId, long userId)
        {
            new StoryDao().FollowStory(truyenId, userId);
            return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UnFollows(long truyenId, long userId)
        {
            new StoryDao().UnFollowStory(truyenId, userId);
            return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
        }
    }
}