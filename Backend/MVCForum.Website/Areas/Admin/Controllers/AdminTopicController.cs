using MVCForum.Domain.Constants;
using MVCForum.Domain.Interfaces.Services;
using MVCForum.Domain.Interfaces.UnitOfWork;
using MVCForum.Website.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCForum.Domain.DomainModel;

namespace MVCForum.Website.Areas.Admin.Controllers
{
    [Authorize(Roles = AppConstants.AdminRoleName+",VietBai")]
    public class AdminTopicController : BaseAdminController
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ITopicService _topicService;
        private readonly ICategoryService _categoryService;
        protected MembershipRole LoggedOnReadOnlyUser;

        public AdminTopicController(ILoggingService loggingService,
                                    IUnitOfWorkManager unitOfWorkManager,
                                    IMembershipService membershipService,
                                    ILocalizationService localizationService,
                                    ISettingsService settingsService,
                                    ITopicService topicService,
                                    ICategoryService categoryService) 
            : base(loggingService, unitOfWorkManager, membershipService, localizationService, settingsService)
        {
            _topicService = topicService;
            _categoryService = categoryService;
        }
        // GET: Admin/AdminTopic
        public ActionResult Index(int? p, string search, bool isPending)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                ViewBag.IsPending = isPending;
                var pageIndex = p ?? 1;
                // Allowed Categories
                var allowedCategories = _categoryService.GetAll().Where(x => x.Slug == "y-hoc" || x.Slug == "su-kien").ToList();
                dynamic topics;
                if (ViewBag.IsPending == false)

                {
                    topics = string.IsNullOrEmpty(search) ? _topicService.GetPagedActiveTopicsAll(pageIndex, 10, allowedCategories) :
                        _topicService.SearchAllActiveTopics(search, pageIndex, 10, allowedCategories);
                }
                else
                {
                    topics = string.IsNullOrEmpty(search) ? _topicService.GetAllPendingTopics(pageIndex, 10, allowedCategories) :
                        _topicService.SearchPendingTopics(search, pageIndex, 10, allowedCategories);
                }
               
                var vm = new TopicPendingViewModel
                {
                    Topics = topics,
                    PageIndex = topics.PageIndex,
                    TotalPages = topics.TotalPages,
                    TotalCount = topics.TotalCount,
                    Search = search
                };
                return View(vm);
            }
        }

        //public ActionResult GetPendingTopics(int? p, string search, bool isPending)
        //{
        //    using (UnitOfWorkManager.NewUnitOfWork())
        //    {
        //        ViewBag.IsPending = isPending;
        //        var pageIndex = p ?? 1;
        //        var topics = string.IsNullOrEmpty(search) ? _topicService.GetAllPendingTopics(pageIndex, SiteConstants.Instance.AdminListPageSize) :
        //            _topicService.SearchPendingTopics(search, pageIndex, SiteConstants.Instance.AdminListPageSize);
        //        var vm = new TopicPendingViewModel()
        //        {
        //            Topics = topics,
        //            PageIndex = topics.PageIndex,
        //            TotalPages = topics.TotalPages,
        //            TotalCount = topics.TotalCount,
        //            Search = search
        //        };
        //        return View(vm);
        //    }
        //}
        public ActionResult DeletePendingTopic(Guid id, bool isPending)
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    var topic = _topicService.Get(id);

                    topic.Pending = isPending;

                    _topicService.UpdatePendingTopic(topic);

                    // We use temp data because we are doing a redirect
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Thành công!",
                        MessageType = GenericMessages.success
                    };
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Thất bại!",
                        MessageType = GenericMessages.warning
                    };
                }
            }

            return RedirectToAction("Index");
        }
    }
}