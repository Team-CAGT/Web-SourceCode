using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using MVCForum.Domain.Constants;
using MVCForum.Domain.DomainModel;
using MVCForum.Domain.DomainModel.Activity;
using MVCForum.Domain.DomainModel.Enums;
using MVCForum.Domain.Interfaces.Services;
using MVCForum.Domain.Interfaces.UnitOfWork;
using MVCForum.Website.Application;
using MVCForum.Website.ViewModels;
using RssItem = MVCForum.Domain.DomainModel.RssItem;
using MVCForum.Website.Areas.Admin.ViewModels;
using MVCForum.Website.ViewModels.Mapping;
using static MVCForum.Website.ViewModels.RSSReader;
using CategoryViewModel = MVCForum.Website.ViewModels.CategoryViewModel;

namespace MVCForum.Website.Controllers
{
    public partial class HomeController : BaseController
    {
        private readonly ITopicService _topicService;
        private readonly ICategoryService _categoryService;
        private readonly IActivityService _activityService;
        private readonly IHospitalFeeService _hospitalFeeService;
        private readonly IDoctorService _doctorService;
        private readonly IRecruitmentService _recruitmentService;
        private readonly IMedicalPackageService _medicalPackageService;
        private readonly IEducationService _educationService;
        private readonly IBannerService _bannerService;

        public HomeController(ILoggingService loggingService, IUnitOfWorkManager unitOfWorkManager,
            IActivityService activityService, IMembershipService membershipService,
            ITopicService topicService, ILocalizationService localizationService, IRoleService roleService,
            ISettingsService settingsService, ICategoryService categoryService, IHospitalFeeService hospitalFeeService,
            IDoctorService doctorService, IRecruitmentService recruitmentService, IMedicalPackageService medicalPackageService,
            IEducationService educationService, IBannerService bannerService)
            : base(loggingService, unitOfWorkManager, membershipService, localizationService, roleService,
                settingsService)
        {
            _topicService = topicService;
            _categoryService = categoryService;
            _activityService = activityService;
            _hospitalFeeService = hospitalFeeService;
            _doctorService = doctorService;
            _recruitmentService = recruitmentService;
            _medicalPackageService = medicalPackageService;
            _educationService = educationService;
            _bannerService = bannerService;
        }
        [OutputCache(Duration = (int)CacheTimes.TenMinutes)]
        public ActionResult Index()
        {
            var banners = _bannerService.GetActiveBanners();
            return View(banners);
        }
        [OutputCache(Duration = (int)CacheTimes.TenMinutes)]
        public ActionResult Trangchu()
        {
            var banners = _bannerService.GetActiveBanners();
            return View(banners);
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Demo()
        {
            return View();
        }

        public ActionResult InternalNews()
        {
            return View();
        }

        public ActionResult Quytrinh()
        {
            return View();
        }

        public ActionResult Lichkhambenh()
        {
            return View();
        }

        [OutputCache(Duration = (int)CacheTimes.TwoHours)]
        public ActionResult GiaVienphi()
        {
            var vm = _hospitalFeeService.GetBangiaDichvu();
            return View(vm);
        }

        public ActionResult BaotriWebsite()
        {
            return View();
        }

        public ActionResult About()
        {
            var department = _doctorService.GetAllDepartmentWithDoctor();
            return View(department);
        }

        public ActionResult ServiceDetail()
        {
            try
            {
                var rs = _medicalPackageService.GetAllMedicalPackages();
                return View(rs);
            }
            catch (Exception)
            {

                return RedirectToAction("Error404");
            }
        }

        public ActionResult Sodotochuc()
        {
            return View();
        }

        public ActionResult Danhsachkhoa()
        {
            return View();
        }

        public ActionResult DanhsachKhoaCanLamsang()
        {
            return View();
        }

        public ActionResult DanhsachPhongChucnang()
        {
            return View();
        }

        public ActionResult Daotao()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult EducationPartial()
        {
            // Set the page index
            var pageIndex = 1;

            var education = _educationService.GetAllPageEducation(null, false, pageIndex, 3);
            var viewModel = new EducationPageViewModel
            {
                Educations = education,
                PageIndex = pageIndex,
                TotalCount = education.TotalCount,
                TotalPages = education.TotalPages
            };
            return PartialView(viewModel);
        }

        public ActionResult EducationList(int? p)
        {
            // Set the page index
            var pageIndex = p ?? 1;

            var education = _educationService.GetAllPageEducation(null, false, pageIndex, 10);
            var viewModel = new EducationPageViewModel
            {
                Educations = education,
                PageIndex = pageIndex,
                TotalCount = education.TotalCount,
                TotalPages = education.TotalPages
            };
            return View(viewModel);
        }
        [ChildActionOnly]
        public ActionResult ScientificResearch()
        {
            // Set the page index
            var pageIndex = 1;
            var allowedCategories = _categoryService.GetAllowedCategories(UsersRole).Where(x => x.Slug == "nghien-cuu-khoa-hoc").ToList();
            var topics = _topicService.GetRecentTopics(pageIndex, 3, SiteConstants.Instance.ActiveTopicsListSize, allowedCategories);
            var viewModel = new ScientificResearchPageViewModel
            {
                Topics = topics,
                PageIndex = pageIndex,
                TotalCount = topics.TotalCount,
                TotalPages = topics.TotalPages
            };
            return PartialView(viewModel);
        }

        public ActionResult ScientificList(int? p)
        {
            // Set the page index
            var pageIndex = p ?? 1;
            var allowedCategories = _categoryService.GetAllowedCategories(UsersRole).Where(x => x.Slug == "nghien-cuu-khoa-hoc").ToList();
            var topics = _topicService.GetRecentTopics(pageIndex, 3, SiteConstants.Instance.ActiveTopicsListSize, allowedCategories);
            var viewModel = new ScientificResearchPageViewModel
            {
                Topics = topics,
                PageIndex = pageIndex,
                TotalCount = topics.TotalCount,
                TotalPages = topics.TotalPages
            };
            return View(viewModel);
        }
        public ActionResult Tuyendung(int? p)
        {
            // Set the page index
            var pageIndex = p ?? 1;

            var recruitment = _recruitmentService.GetAllPageRecruitment(null, false, pageIndex, 10);
            var viewModel = new RecruitmentPageViewModel
            {
                Recruitments = recruitment,
                PageIndex = pageIndex,
                TotalCount = recruitment.TotalCount,
                TotalPages = recruitment.TotalPages
            };
            return View(viewModel);
        }

        public ActionResult RecruitmentDetail(Guid id)
        {
            var recruitment = _recruitmentService.Get(id);
            return View(recruitment);
        }
        public ActionResult ReviewRecruitment(Guid id)
        {
            var recruitment = _recruitmentService.Get(id);
            return View(recruitment);
        }
        public ActionResult Huongdan()
        {
            return View();
        }

        public ActionResult PhongChucNang(string tag)
        {
            try
            {
                var slug = "phong-chuc-nang";
                var department = _doctorService.GetAllSubDepartmentBySlug(slug, out var name);
                ViewBag.NameDepartment = name;
                return View(department);
            }
            catch (Exception)
            {

                return RedirectToAction("Error404");
            }
        }

        public ActionResult CauLacBoBenhNhan(int? p)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    var category = _categoryService.GetCategoryBySlug("cau-lac-bo-benh-nhan");

                    // Set the page index
                    var pageIndex = p ?? 1;

                    // Get the topics
                    var subCategories = _categoryService.GetPagedSubCategory(pageIndex,
                        6,
                        SiteConstants.Instance.ActiveTopicsListSize,
                        category.Id);

                    var viewModel = new CategoryPagedViewModel
                    {
                        Categories = subCategories,
                        PageIndex = pageIndex,
                        TotalCount = subCategories.TotalCount,
                        TotalPages = subCategories.TotalPages
                    };
                    return View(viewModel);
                }
                catch (Exception)
                {

                    return RedirectToAction("Error404");
                }

            }
        }

        public ActionResult PatientClubDetail(Guid id, int? p)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var category = _categoryService.GetAllowedCategories(UsersRole).Where(x => x.Id == id).ToList();
                var cate = _categoryService.Get(id);
                ViewBag.Category = cate;
                var settings = SettingsService.GetSettings();

                // Set the page index
                var pageIndex = p ?? 1;

                // Get the topics
                var topics = _topicService.GetRecentTopics(pageIndex,
                    10,
                    SiteConstants.Instance.ActiveTopicsListSize,
                    category);

                // Get the Topic View Models
                var topicViewModels = ViewModelMapping.CreateTopicViewModels(topics, RoleService, UsersRole, LoggedOnReadOnlyUser, category, settings);

                // create the view model
                var viewModel = new ActiveTopicsViewModel
                {
                    Topics = topicViewModels,
                    PageIndex = pageIndex,
                    TotalCount = topics.TotalCount,
                    TotalPages = topics.TotalPages
                };

                return View(viewModel);
            }
        }
        public ActionResult Chuyenmon()
        {
            return View();
        }

        public ActionResult ThongbaoNoibo()
        {
            return View();
        }

        public ActionResult QuanLyChatluong()
        {
            return View();
        }

        public ActionResult BoIcon()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Error401()
        {
            return View();
        }
        public ActionResult DanhmucKythuat()
        {
            return View();
        }
        //public ActionResult Leaderboard()
        //{
        //    return View();
        //}

        //public ActionResult Following()
        //{

        //    return View();
        //}

        //public ActionResult PostedIn()
        //{
        //    return View();
        //}
        public ActionResult Icon()
        {
            return View();
        }
        //public ActionResult TermsAndConditions()
        //{
        //    using (UnitOfWorkManager.NewUnitOfWork())
        //    {
        //        var settings = SettingsService.GetSettings();
        //        var viewModel = new TermsAndConditionsViewModel
        //        {
        //            Agree = false,
        //            TermsAndConditions = settings.TermsAndConditions
        //        };
        //        return View(viewModel);
        //    }
        //}

        //[HttpPost]
        //public ActionResult TermsAndConditions(TermsAndConditionsViewModel viewmodel)
        //{
        //    using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var user = MembershipService.GetUser(LoggedOnReadOnlyUser.Id);
        //            user.HasAgreedToTermsAndConditions = viewmodel.Agree;
        //            try
        //            {
        //                unitOfWork.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                unitOfWork.Rollback();
        //                LoggingService.Error(ex);
        //            }
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    return View(viewmodel);
        //}

        //public ActionResult Activity(int? p)
        //{
        //    using (UnitOfWorkManager.NewUnitOfWork())
        //    {
        //        // Set the page index
        //        var pageIndex = p ?? 1;

        //        // Get the topics
        //        var activities = _activityService.GetPagedGroupedActivities(pageIndex, SettingsService.GetSettings().ActivitiesPerPage);

        //        // create the view model
        //        var viewModel = new AllRecentActivitiesViewModel
        //        {
        //            Activities = activities,
        //            PageIndex = pageIndex,
        //            TotalCount = activities.TotalCount,
        //        };

        //        return View(viewModel);
        //    }
        //}

        //[OutputCache(Duration = (int)CacheTimes.TwoHours)]
        //public ActionResult LatestRss()
        //{
        //    using (UnitOfWorkManager.NewUnitOfWork())
        //    {
        //        // Allowed Categories for a guest - As that's all we want latest RSS to show
        //        var guestRole = RoleService.GetRole(AppConstants.GuestRoleName);
        //        var allowedCategories = _categoryService.GetAllowedCategories(guestRole);

        //        // get an rss lit ready
        //        var rssTopics = new List<RssItem>();

        //        // Get the latest topics
        //        var topics = _topicService.GetRecentRssTopics(50, allowedCategories);

        //        // Get all the categories for this topic collection
        //        var categories = topics.Select(x => x.Category).Distinct();

        //        // create permissions
        //        var permissions = new Dictionary<Category, PermissionSet>();

        //        // loop through the categories and get the permissions
        //        foreach (var category in categories)
        //        {
        //            var permissionSet = RoleService.GetPermissions(category, UsersRole);
        //            permissions.Add(category, permissionSet);
        //        }

        //        // Now loop through the topics and remove any that user does not have permission for
        //        foreach (var topic in topics)
        //        {
        //            // Get the permissions for this topic via its parent category
        //            var permission = permissions[topic.Category];

        //            // Add only topics user has permission to
        //            if (!permission[SiteConstants.Instance.PermissionDenyAccess].IsTicked)
        //            {
        //                if (topic.Posts.Any())
        //                {
        //                    var firstOrDefault = topic.Posts.FirstOrDefault(x => x.IsTopicStarter);
        //                    if (firstOrDefault != null)
        //                        rssTopics.Add(new RssItem { Description = firstOrDefault.PostContent, Link = topic.NiceUrl, Title = topic.Name, PublishedDate = topic.CreateDate });
        //                }
        //            }
        //        }

        //        return new RssResult(rssTopics, LocalizationService.GetResourceString("Rss.LatestActivity.Title"), LocalizationService.GetResourceString("Rss.LatestActivity.Description"));
        //    }
        //}

        //[OutputCache(Duration = (int)CacheTimes.TwoHours)]
        //public ActionResult ActivityRss()
        //{
        //    using (UnitOfWorkManager.NewUnitOfWork())
        //    {
        //        // get an rss lit ready
        //        var rssActivities = new List<RssItem>();

        //        var activities = _activityService.GetAll(50).OrderByDescending(x => x.ActivityMapped.Timestamp);

        //        var activityLink = Url.Action("Activity");

        //        // Now loop through the topics and remove any that user does not have permission for
        //        foreach (var activity in activities)
        //        {
        //            if (activity is BadgeActivity)
        //            {
        //                var badgeActivity = activity as BadgeActivity;
        //                rssActivities.Add(new RssItem
        //                {
        //                    Description = badgeActivity.Badge.Description,
        //                    Title = string.Concat(badgeActivity.User.UserName, " ", LocalizationService.GetResourceString("Activity.UserAwardedBadge"), " ", badgeActivity.Badge.DisplayName, " ", LocalizationService.GetResourceString("Activity.Badge")),
        //                    PublishedDate = badgeActivity.ActivityMapped.Timestamp,
        //                    RssImage = AppHelpers.ReturnBadgeUrl(badgeActivity.Badge.Image),
        //                    Link = activityLink
        //                });
        //            }
        //            else if (activity is MemberJoinedActivity)
        //            {
        //                var memberJoinedActivity = activity as MemberJoinedActivity;
        //                rssActivities.Add(new RssItem
        //                {
        //                    Description = string.Empty,
        //                    Title = LocalizationService.GetResourceString("Activity.UserJoined"),
        //                    PublishedDate = memberJoinedActivity.ActivityMapped.Timestamp,
        //                    RssImage = memberJoinedActivity.User.MemberImage(SiteConstants.Instance.GravatarPostSize),
        //                    Link = activityLink
        //                });
        //            }
        //            else if (activity is ProfileUpdatedActivity)
        //            {
        //                var profileUpdatedActivity = activity as ProfileUpdatedActivity;
        //                rssActivities.Add(new RssItem
        //                {
        //                    Description = string.Empty,
        //                    Title = LocalizationService.GetResourceString("Activity.ProfileUpdated"),
        //                    PublishedDate = profileUpdatedActivity.ActivityMapped.Timestamp,
        //                    RssImage = profileUpdatedActivity.User.MemberImage(SiteConstants.Instance.GravatarPostSize),
        //                    Link = activityLink
        //                });
        //            }

        //        }

        //        return new RssResult(rssActivities, LocalizationService.GetResourceString("Rss.LatestActivity.Title"), LocalizationService.GetResourceString("Rss.LatestActivity.Description"));
        //    }
        //}

        //[OutputCache(Duration = (int)CacheTimes.TwoHours)]
        //public ActionResult GoogleSitemap()
        //{
        //    using (UnitOfWorkManager.NewUnitOfWork())
        //    {
        //        // Allowed Categories for a guest
        //        var guestRole = RoleService.GetRole(AppConstants.GuestRoleName);
        //        var allowedCategories = _categoryService.GetAllowedCategories(guestRole);

        //        // Get all topics that a guest has access to
        //        var allTopics = _topicService.GetAll(allowedCategories);

        //        // Sitemap holder
        //        var sitemap = new List<SitemapEntry>();

        //        // ##### TOPICS
        //        foreach (var topic in allTopics.Where(x => x.LastPost != null))
        //        {
        //            var sitemapEntry = new SitemapEntry
        //            {
        //                Name = topic.Name,
        //                Url = topic.NiceUrl,
        //                LastUpdated = topic.LastPost.DateEdited,
        //                ChangeFrequency = SiteMapChangeFreqency.daily,
        //                Priority = "0.6"
        //            };
        //            sitemap.Add(sitemapEntry);
        //        }

        //        return new GoogleSitemapResult(sitemap);
        //    }
        //}

        //[OutputCache(Duration = (int)CacheTimes.TwoHours)]
        //public ActionResult GoogleMemberSitemap()
        //{
        //    using (UnitOfWorkManager.NewUnitOfWork())
        //    {
        //        // get all members profiles
        //        var members = MembershipService.GetAll();

        //        // Sitemap holder
        //        var sitemap = new List<SitemapEntry>();

        //        // #### MEMBERS
        //        foreach (var member in members)
        //        {
        //            var sitemapEntry = new SitemapEntry
        //            {
        //                Name = member.UserName,
        //                Url = member.NiceUrl,
        //                LastUpdated = member.CreateDate,
        //                ChangeFrequency = SiteMapChangeFreqency.weekly,
        //                Priority = "0.4"
        //            };
        //            sitemap.Add(sitemapEntry);
        //        }

        //        return new GoogleSitemapResult(sitemap);
        //    }
        //}

        //[OutputCache(Duration = (int)CacheTimes.TwoHours)]
        //public ActionResult GoogleCategorySitemap()
        //{
        //    using (UnitOfWorkManager.NewUnitOfWork())
        //    {
        //        // Allowed Categories for a guest
        //        var guestRole = RoleService.GetRole(AppConstants.GuestRoleName);
        //        var allowedCategories = _categoryService.GetAllowedCategories(guestRole);

        //        // Sitemap holder
        //        var sitemap = new List<SitemapEntry>();

        //        // #### CATEGORIES
        //        foreach (var category in allowedCategories)
        //        {
        //            // Get last post 
        //            var topic = category.Topics.OrderByDescending(x => x.LastPost.DateEdited).FirstOrDefault();
        //            var sitemapEntry = new SitemapEntry
        //            {
        //                Name = category.Name,
        //                Url = category.NiceUrl,
        //                LastUpdated = topic != null ? topic.LastPost.DateEdited : category.DateCreated,
        //                ChangeFrequency = SiteMapChangeFreqency.monthly
        //            };
        //            sitemap.Add(sitemapEntry);
        //        }

        //        return new GoogleSitemapResult(sitemap);
        //    }
        //}
        public ActionResult Department(string slug)
        {
            try
            {
                var department = _doctorService.GetAllSubDepartmentBySlug(slug, out var name);
                ViewBag.NameDepartment = name;
                return View(department);
            }
            catch (Exception)
            {

                return RedirectToAction("Error404");
            }
        }

        public ActionResult DoctorSearch(string departmentSlug = "", string departmentManage_Slug = "",
            string search = "", int p = 1)
        {
            try
            {
                ViewBag.ListDepartment = _doctorService.GetListDepartment();
                ViewBag.DepartmentSlug = departmentSlug;
                ViewBag.DepartmentManage_Slug = departmentManage_Slug;
                ViewBag.Doctor = search;
                var doctor = _doctorService.GetDoctorSearch(departmentSlug, departmentManage_Slug, search, p, 16);
                return View(doctor);
            }
            catch (Exception)
            {

                return RedirectToAction("Error404");
            }
        }

        public ActionResult ApplyCV(Guid id)
        {
            var recruitment = _recruitmentService.Get(id);
            var vm = new RecruitmentCVViewModel();
            vm.Recruitment = recruitment;
            vm.RecruitmentId = id;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplyCV(RecruitmentCVViewModel vm)
        {

            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                var uploadFolderPath =
                    HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath,
                        "ApplyCV" + "/" + vm.RecruitmentId));
                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }
                if (vm.Files != null)
                {
                    if (vm.Files[0] != null)
                    {
                        // Loop through each file and get the file info and save to the users folder and Db
                        // var file = vm.Files[0];
                        if (vm.Files.Length > 0)
                        {
                            List<string> filedata = new List<string>();
                            foreach (var file in vm.Files)
                            {
                                var uploadResult = AppHelpers.UploadRecruitmentFile(file, uploadFolderPath, LocalizationService);

                                if (!uploadResult.UploadSuccessful)
                                {
                                    return Json(new
                                    {
                                        Result = false,
                                        Message = "Hồ sơ không đúng định dạng!"

                                    }, JsonRequestBehavior.AllowGet);
                                }

                                filedata.Add(uploadResult.UploadedFileUrl);

                            }

                            vm.FileUrl = string.Join(";", filedata);

                        }
                    }
                }

                if (vm.Images[0] != null)
                {
                    // Loop through each file and get the file info and save to the users folder and Db
                    // var file = vm.Files[0];
                    if (vm.Images.Length > 0)
                    {
                        List<string> imageData = new List<string>();
                        foreach (var img in vm.Images)
                        {
                            var uploadResult = AppHelpers.UploadRecruitmentFile(img, uploadFolderPath, LocalizationService,
                                true);

                            if (!uploadResult.UploadSuccessful)
                            {
                                TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                                {
                                    Message = uploadResult.ErrorMessage,
                                    MessageType = GenericMessages.danger
                                };
                                return Json(new
                                {
                                    Result = false,
                                    Message = "Hình ảnh không đúng định dạng lớn hơn 2mb!"

                                }, JsonRequestBehavior.AllowGet);
                            }

                            imageData.Add(uploadResult.UploadedFileUrl);

                        }

                        vm.HinhAnhChungChi = string.Join(";", imageData);

                    }
                }
                //    // Loop through each file and get the file info and save to the users folder and Db
                //    var file = vm.Files[0];
                //if (file != null)
                //{
                //    // If successful then upload the file
                //    var uploadResult = AppHelpers.UploadFile(file, uploadFolderPath, LocalizationService);

                //    if (!uploadResult.UploadSuccessful)
                //    {
                //        return Json(new
                //        {
                //            Result = false,
                //            Message = uploadResult.ErrorMessage

                //        }, JsonRequestBehavior.AllowGet);
                //    }

                //    // Save image to data
                //    vm.FileUrl = uploadResult.UploadedFileName;
                //}

                try
                {

                    var cv = vm.ToEntity();
                    var recruitment = _recruitmentService.Get(vm.RecruitmentId);
                    cv.Recruitment = recruitment;
                    _recruitmentService.AddApplyCV(cv);

                    unitOfWork.Commit();
                    return Json(new
                    {
                        Result = true,
                        Message = "Nộp hồ sơ ứng tuyển thành công!"

                    }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    LoggingService.Error(ex);
                    return Json(new
                    {
                        Result = false,
                        Message = "Vui lòng kiểm tra lại thông tin"

                    }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult QuyTrinhTuyenDung()
        {
            return View();
        }
    }
}
