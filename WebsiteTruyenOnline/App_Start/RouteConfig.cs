using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteTruyenOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "truyenmoi",
                url: "truyen-moi",
                defaults: new { controller = "NewStory", action = "Index"},
                namespaces: new[] { "WebsiteTruyenOnline.Controllers" }
            );
            routes.MapRoute(
                name: "truyenhot",
                url: "truyen-hot",
                defaults: new { controller = "HotStory", action = "Index" },
                namespaces: new[] { "WebsiteTruyenOnline.Controllers" }
            );
            routes.MapRoute(
                name: "login",
                url: "Dang-nhap",
                defaults: new { controller = "Login", action = "Index" },
                namespaces: new[] { "WebsiteTruyenOnline.Controllers" }
            );
            routes.MapRoute(
                name: "register",
                url: "Dang-ky",
                defaults: new { controller = "Login", action = "Register" },
                namespaces: new[] { "WebsiteTruyenOnline.Controllers" }
            );
            routes.MapRoute(
                name: "theloai",
                url: "the-loai/{metatitle}-{id_theloai}",
                defaults: new { controller = "Category", action = "Index" },
                namespaces: new[] { "WebsiteTruyenOnline.Controllers" }
            );
            routes.MapRoute(
                name: "trangthai",
                url: "trang-thai/{metatitle}-{id_trangthai}",
                defaults: new { controller = "Status", action = "Index" },
                namespaces: new[] { "WebsiteTruyenOnline.Controllers" }
            );
            routes.MapRoute(
                name: "Chitiettruyen",
                url: "{metatitle}-{id}",
                defaults: new { controller = "Story", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteTruyenOnline.Controllers" }
            );
            routes.MapRoute(
                name: "doctruyen",
                url: "{meta}/{metatitle}-{id_chuong}",
                defaults: new { controller = "Story", action = "DocTruyen", id_chuong = UrlParameter.Optional },
                namespaces: new[] { "WebsiteTruyenOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteTruyenOnline.Controllers" }
            );
        }
    }
}
