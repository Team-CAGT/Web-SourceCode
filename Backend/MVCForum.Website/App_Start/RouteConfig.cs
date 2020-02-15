using System.Web.Mvc;
using System.Web.Routing;
using MVCForum.Domain.Constants;

namespace MVCForum.Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RouteTable.Routes.LowercaseUrls = true;
            RouteTable.Routes.AppendTrailingSlash = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            // API Attribute Routes
            //routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "categoryUrls", // Route name
                string.Concat(SiteConstants.Instance.CategoryUrlIdentifier, "/{slug}"), // URL with parameters
                new { controller = "Category", action = "Show", slug = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "categoryRssUrls", // Route name
                string.Concat(SiteConstants.Instance.CategoryUrlIdentifier, "/rss/{slug}"), // URL with parameters
                new { controller = "Category", action = "CategoryRss", slug = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "topicUrls", // Route name
                string.Concat(SiteConstants.Instance.TopicUrlIdentifier, "/{slug}"), // URL with parameters
                new { controller = "Topic", action = "Article", slug = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "topicReviewUrls", // Route name
                string.Concat("Review", "/{slug}"), // URL with parameters
                new { controller = "Topic", action = "Review", slug = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "memberUrls", // Route name
                string.Concat(SiteConstants.Instance.MemberUrlIdentifier, "/{slug}"), // URL with parameters
                new { controller = "Members", action = "GetByName", slug = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "tagUrls", // Route name
                string.Concat(SiteConstants.Instance.TagsUrlIdentifier, "/{tag}"), // URL with parameters
                new { controller = "Topic", action = "TopicsByTag", tag = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "topicXmlSitemap", // Route name
                "topicxmlsitemap", // URL with parameters
                new { controller = "Home", action = "GoogleSitemap" } // Parameter defaults
            );

            routes.MapRoute(
                "categoryXmlSitemap", // Route name
                "categoryxmlsitemap", // URL with parameters
                new { controller = "Home", action = "GoogleCategorySitemap" } // Parameter defaults
            );

            routes.MapRoute(
                "memberXmlSitemap", // Route name
                "memberxmlsitemap", // URL with parameters
                new { controller = "Home", action = "GoogleMemberSitemap" } // Parameter defaults
            );
            // route config
            routes.MapRoute(
               "lien-he", // Route name
               "lien-he", // URL with parameters
               new { controller = "Questions", action = "Lienhe" } // Parameter defaults
           );
            // route config
            routes.MapRoute(
                "hoi-dap", // Route name
                "hoi-dap", // URL with parameters
                new { controller = "Questions", action = "Hoidap" } // Parameter defaults
            );
            routes.MapRoute(
                "Đang-nhap", // Route name
                "Dang-nhap", // URL with parameters
                new { controller = "members", action = "logon" } // Parameter defaults
            );
            routes.MapRoute(
                "Đang-ky", // Route name
                "taouser", // URL with parameters
                new { controller = "members", action = "TaoUser" } // Parameter defaults
            );
            routes.MapRoute(
                "kham-chua-benh", // Route name
                "kham-chua-benh/{slug}", // URL with parameters
                new { controller = "Home", action = "Department", slug = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
             "gioi-thieu", // Route name
             "gioi-thieu", // URL with parameters
             new { controller = "Home", action = "About" } // Parameter defaults
            );
            routes.MapRoute(
             "dich-vu", // Route name
             "dich-vu", // URL with parameters
             new { controller = "Home", action = "ServiceDetail" } // Parameter defaults
            );
            routes.MapRoute(
                "tin-tuc", // Route name
                "tin-tuc/{type}", // URL with parameters
                new { controller = "Home", action = "News", type = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "tuyen-dung", // Route name
                "tuyen-dung", // URL with parameters
                new { controller = "Home", action = "Tuyendung" } // Parameter defaults
            );
            routes.MapRoute(
                "dao-tao", // Route name
                "dao-tao", // URL with parameters
                new { controller = "Home", action = "Daotao" } // Parameter defaults
            );
            routes.MapRoute(
                "huong-dan", // Route name
                "huong-dan", // URL with parameters
                new { controller = "Home", action = "Huongdan" } // Parameter defaults
            );
            routes.MapRoute(
                "tim-bac-si", // Route name
                "tim-bac-si", // URL with parameters
                new { controller = "Home", action = "DoctorSearch" } // Parameter defaults
            );
            routes.MapRoute(
                "tim-kiem", // Route name
                "tim-kiem", // URL with parameters
                new { controller = "Search", action = "Index" } // Parameter defaults
            );
            routes.MapRoute(
                "phong-chuc-nang", // Route name
                "phong-chuc-nang", // URL with parameters
                new { controller = "Home", action = "PhongChucNang" } // Parameter defaults
            );
            routes.MapRoute(
                "clb", // Route name
                "clb", // URL with parameters
                new { controller = "Home", action = "CauLacBoBenhNhan" } // Parameter defaults
            );
            routes.MapRoute(
                "clb-chi-tiet", // Route name
                "clb-chi-tiet", // URL with parameters
                new { controller = "Home", action = "PatientClubDetail" } // Parameter defaults
            );
            routes.MapRoute(
                "gia-vien-phi", // Route name
                "gia-vien-phi", // URL with parameters
                new { controller = "Home", action = "GiaVienphi" } // Parameter defaults
            );
            routes.MapRoute(
                "danh-sach-dao-tao", // Route name
                "danh-sach-dao-tao", // URL with parameters
                new { controller = "Home", action = "EducationList" } // Parameter defaults
            );
            routes.MapRoute(
                "quy-trinh-tuyen-dung", // Route name
                "quy-trinh-tuyen-dung", // URL with parameters
                new { controller = "Home", action = "QuyTrinhTuyenDung" } // Parameter defaults
            );
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            //.RouteHandler = new SlugRouteHandler()
        }
    }
}
