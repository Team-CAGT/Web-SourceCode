using MVCForum.Domain.Constants;
using MVCForum.Domain.DomainModel;
using MVCForum.Domain.Interfaces.Services;
using MVCForum.Domain.Interfaces.UnitOfWork;
using MVCForum.Website.Application;
using MVCForum.Website.Areas.Admin.ViewModels;
using MVCForum.Website.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using MVCForum.Domain.Model;
using MVCForum.Utilities;

namespace MVCForum.Website.Controllers
{
    public class QuestionsController : BaseController
    {
        private readonly IContactService _contactService;

        public QuestionsController(ILoggingService loggingService, IUnitOfWorkManager unitOfWorkManager,
            IMembershipService membershipService,
            ILocalizationService localizationService, IRoleService roleService, ISettingsService settingsService,
            IContactService contactService) :
            base(loggingService, unitOfWorkManager, membershipService, localizationService, roleService,
                settingsService)
        {
            _contactService = contactService;
        }
        // GET: Questions
        public ActionResult Index()
        {
            var vm = new QuestionViewModel();
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuestion(QuestionViewModel vm)
        {

            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    var question = vm.ToEntity();
                    if (vm.Files != null)
                    {
                        var uploadFolderPath =
                              HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath,
                                 "Questions"));
                        if (!Directory.Exists(uploadFolderPath))
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }
                        foreach (var file in vm.Files)
                        {
                            if (file != null)
                            {
                                // If successful then upload the file
                                var uploadResult = AppHelpers.UploadFile(file, uploadFolderPath,
                                    LocalizationService);
                                if (!uploadResult.UploadSuccessful)
                                {
                                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                                    {
                                        Message = uploadResult.ErrorMessage,
                                        MessageType = GenericMessages.danger
                                    };
                                    unitOfWork.Rollback();
                                    return Json(new
                                    {
                                        Result = false,
                                        Message = "Vui lòng kiểm tra lại file thông tin",

                                    }, JsonRequestBehavior.AllowGet);
                                }
                                question.UrlFile = uploadResult.UploadedFileName;
                                // Add the filename to the database
                                //var uploadedFile = new UploadedFile
                                //{
                                //    Filename = uploadResult.UploadedFileName,
                                //    question.UrlFile = uploadResult.UploadedFileName;
                                //};
                                //_uploadedFileService.Add(uploadedFile);
                            }
                        }

                    }
                    _contactService.AddQuestion(question);
                    try
                    {
                        unitOfWork.Commit();
                        return Json(new
                        {
                            Result = true,
                            Message = "Thành công",

                        }, JsonRequestBehavior.AllowGet);

                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                        LoggingService.Error(ex);
                        return Json(new
                        {
                            Result = false,
                            Message = "Vui lòng kiểm tra lại thông tin",

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json(new
                {
                    Result = false,
                    Message = "Vui lòng kiểm tra lại thông tin",

                }, JsonRequestBehavior.AllowGet);
            }
            return View(vm);
        }
        public ActionResult Lienhe()
        {
            //  var vm = new QuestionViewModel();
            return View();
        }
        public ActionResult Hoidap(Guid? id,int? p)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var pageIndex = p ?? 1;

                var rs = _contactService.GetAllQuestionPost(pageIndex, 3);

                if (id != Guid.Empty && id != null)
                {
                    rs = _contactService.GetQuestions(id.Value);
                }

                // Redisplay list of questions

                var vm = new QuestionsViewModel()
                {
                    Questions = rs,
                    PageIndex = pageIndex,
                    TotalPages = rs.TotalPages,
                    TotalCount = rs.TotalCount
                };

                return View(vm);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContact(ContactViewModel vm)
        {

            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                var contact = vm.ToEntity();

                _contactService.Add(contact);
                try
                {
                    unitOfWork.Commit();
                    return Json(new
                    {
                        Result = true,
                        Message = "Cảm ơn bạn đã gửi tin nhắn!"

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
        public ActionResult DangkykhamchuaBenh()
        {
            var vm = new AppointmentViewModel() { Amount = 1 };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAppointment(AppointmentViewModel vm)
        {

            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                var app = vm.ToEntity();
                if (vm.IsCompany == true)
                {
                    app.IsCompany = true;
                    app.CompanyName = vm.CompanyName;

                }
                app.Type = "Khám sức khỏe tổng quát";
                _contactService.AddAppointment(app);
                try
                {
                    unitOfWork.Commit();
                    return Json(new
                    {
                        Result = true,
                        Message = "Cảm ơn bạn đã đăng ký!",

                    }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    LoggingService.Error(ex);
                    return Json(new
                    {
                        Result = false,
                        Message = "Vui lòng kiểm tra lại thông tin",
                    }, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [ChildActionOnly]
        public ActionResult TodayQuestion()
        {
            var viewModel = _contactService.GetAllQuestionEditToday();
            return PartialView(viewModel);
        }
        [ChildActionOnly]
        public ActionResult GiaVienphi()
        {

            return PartialView();

        }
        [ChildActionOnly]
        public ActionResult GiaVienphiPartial(string url)
        {
            ViewBag.Url = url;
            return PartialView();

        }
    }
}