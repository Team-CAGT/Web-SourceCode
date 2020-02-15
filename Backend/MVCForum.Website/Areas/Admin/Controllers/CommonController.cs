using MVCForum.Domain.Constants;
using MVCForum.Domain.DomainModel.Entities;
using MVCForum.Domain.Interfaces.Services;
using MVCForum.Domain.Interfaces.UnitOfWork;
using MVCForum.Website.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using MVCForum.Website.Application;
using MVCForum.Domain.DomainModel;
using MVCForum.Utilities;
using MVCForum.Website.ViewModels;

namespace MVCForum.Website.Areas.Admin.Controllers
{
    [Authorize(Roles = AppConstants.AdminRoleName + ",TuyenDung,VietBai,HoiDap")]
    public class CommonController : BaseAdminController
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IContactService _contactService;
        private readonly ICategoryService _categoryService;
        private readonly IHospitalFeeService _hospitalFeeService;
        private readonly IDoctorService _doctorService;
        private readonly IRecruitmentService _recruitmentService;
        private readonly IMedicalPackageService _medicalPackageService;
        private readonly IEducationService _educationService;
        private readonly IRoleService _roleService;
        private readonly IEmailService _emailService;

        public CommonController(ILoggingService loggingService,
                                        IUnitOfWorkManager unitOfWorkManager,
                                        IMembershipService membershipService,
                                        ILocalizationService localizationService,
                                        IContactService contactService,
                                        ISettingsService settingsService,
                                        ICategoryService categoryService,
                                        IHospitalFeeService hospitalFeeService,
                                        IDoctorService doctorService,
                                        IRecruitmentService recruitmentService,
                                        IMedicalPackageService medicalPackageService,
                                        IEducationService educationService,
                                        IRoleService roleService, IEmailService emailService)
            : base(loggingService, unitOfWorkManager, membershipService, localizationService, settingsService)
        {
            _contactService = contactService;
            _categoryService = categoryService;
            _hospitalFeeService = hospitalFeeService;
            _doctorService = doctorService;
            _recruitmentService = recruitmentService;
            _medicalPackageService = medicalPackageService;
            _educationService = educationService;
            _roleService = roleService;
            _emailService = emailService;
        }
        // GET: Admin/Common
        [Authorize(Roles = AppConstants.AdminRoleName + ",HoiDap,VietBai")]
        public ActionResult Index(int? p, string search)
        {

            //AdminTopicViewModel vm = new AdminTopicViewModel();

            //  var rs = _medihubTopicService.GetAllTopicAdminPost().Select(x => new DuyetTopicViewModel(x)).ToList();
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var pageIndex = p ?? 1;
                var allTopic = string.IsNullOrEmpty(search) ? _contactService.GetAllContactPost(pageIndex, SiteConstants.Instance.AdminListPageSize) :
                                    _contactService.SearchContact(search, pageIndex, SiteConstants.Instance.AdminListPageSize);

                // Redisplay list of users

                var vm = new ContactFormViewModel()
                {
                    ContactForms = allTopic,
                    PageIndex = allTopic.PageIndex,
                    TotalPages = allTopic.TotalPages,
                    TotalCount = allTopic.TotalCount,
                    Search = search
                };

                return View(vm);
            }

            // return View(rs.ToList());
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai,LienHe")]
        public ActionResult UpdateContactStatus(Guid id, string keyWord, string search)
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    var contact = _contactService.Get(id);
                    contact.Status = keyWord;
                    _contactService.Update(contact);
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Cập nhật trạng thái Liên Hệ thành công!",
                        MessageType = GenericMessages.success
                    };
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                }

                return RedirectToAction("Index", new { search = search });
            }
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",HoiDap,VietBai")]
        public ActionResult XuatExcelLienhe()
        {
            var gv = new GridView();
            var list = _contactService.GetAllContacts();
            gv.DataSource = list.Select(x => new ContactExcelViewModel(x)).ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DanhSachLienHeBVDKKVTD.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = UnicodeEncoding.UTF8.WebName;
            Response.HeaderEncoding = UnicodeEncoding.UTF8;
            Response.ContentEncoding = UnicodeEncoding.UTF8;



            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();

            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter);
            Response.Flush();
            Response.End();
            return View("Index");
        }
        //[Authorize(Roles = AppConstants.AdminRoleName)]
        //public ActionResult Edit(Guid id)
        //{
        //    using (UnitOfWorkManager.NewUnitOfWork())
        //    {
        //        var contact = _contactService.Get(id);



        //        return View(viewModel);
        //    }
        //}
        [Authorize(Roles = AppConstants.AdminRoleName + ",HoiDap,VietBai")]
        public ActionResult QuestionList(int? p, string search)
        {

            //AdminTopicViewModel vm = new AdminTopicViewModel();

            //  var rs = _medihubTopicService.GetAllTopicAdminPost().Select(x => new DuyetTopicViewModel(x)).ToList();
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var pageIndex = p ?? 1;
                var allTopic = string.IsNullOrEmpty(search) ? _contactService.GetAllQuestionAdminPost(pageIndex, SiteConstants.Instance.AdminListPageSize) :
                                    _contactService.SearchQuestions(search, pageIndex, SiteConstants.Instance.AdminListPageSize);

                // Redisplay list of users

                var vm = new QuestionsViewModel()
                {
                    Questions = allTopic,
                    PageIndex = allTopic.PageIndex,
                    TotalPages = allTopic.TotalPages,
                    TotalCount = allTopic.TotalCount,
                    Search = search
                };

                return View(vm);
            }

            // return View(rs.ToList());
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",HoiDap,VietBai")]
        public ActionResult XuatExcel()
        {
            var gv = new GridView();
            var list = _contactService.GellAllNewQuestions();
            gv.DataSource = list.Select(x => new QuestionExcelViewModel(x)).ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DanhSachCauHoiBVDKKVTD.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("QuestionList");
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",HoiDap")]
        public ActionResult EditQuestion(Guid id)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var quest = _contactService.GetQuestion(id);



                return View(quest);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",HoiDap")]
        public ActionResult EditQuestion(Question question)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    var quest = _contactService.GetQuestion(question.Id);


                    // Map everything in model except properties hidden on page
                    quest.Name = question.Name;
                    quest.Age = question.Age;
                    quest.Phone = question.Phone;
                    quest.Address = question.Address;
                    quest.Email = question.Email;
                    quest.ContentQuestion = question.ContentQuestion;
                    quest.ReplyContent = question.ReplyContent;
                    quest.Status = "UPDATE";
                    quest.Chuyenmuc = question.Chuyenmuc;
                    quest.Title = question.Title;
                    quest.ContentQuestion = question.ContentQuestion;

                    try
                    {
                        _contactService.UpdateQuestion(quest);
                        SendEmailConfirmationEmail(new MailTo()
                        {
                            Id = quest.Id,
                            Title = "Câu hỏi của bạn đã được trả lời",
                            Name = quest.Name,
                            Content = $"Nội dung trả lời: {quest.ReplyContent}",
                            Email = quest.Email

                        });
                        unitOfWork.Commit();
                        ViewBag.Message = new GenericMessageViewModel
                        {
                            Message = "Câu hỏi đã được chỉnh sửa",
                            MessageType = GenericMessages.success
                        };
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                        LoggingService.Error(ex);
                        ModelState.AddModelError(string.Empty,
                            LocalizationService.GetResourceString("Errors.GenericMessage"));
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
            return RedirectToAction("QuestionList", "Common", new { area = "admin" });
        }
        [Authorize(Roles = AppConstants.AdminRoleName)]
        public ActionResult AppointmentList(int? p, string search)
        {

            //AdminTopicViewModel vm = new AdminTopicViewModel();

            //  var rs = _medihubTopicService.GetAllTopicAdminPost().Select(x => new DuyetTopicViewModel(x)).ToList();
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var pageIndex = p ?? 1;
                var allTopic = string.IsNullOrEmpty(search) ? _contactService.GetAllAppointmentPost(pageIndex, SiteConstants.Instance.AdminListPageSize) :
                                    _contactService.SearchAppointment(search, pageIndex, SiteConstants.Instance.AdminListPageSize);

                // Redisplay list of users

                var vm = new AppointmentsViewModel()
                {
                    Appointments = allTopic,
                    PageIndex = allTopic.PageIndex,
                    TotalPages = allTopic.TotalPages,
                    TotalCount = allTopic.TotalCount,
                    Search = search
                };

                return View(vm);
            }
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult SoyteList(int? p, string search)
        {

            //AdminTopicViewModel vm = new AdminTopicViewModel();

            //  var rs = _medihubTopicService.GetAllTopicAdminPost().Select(x => new DuyetTopicViewModel(x)).ToList();
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var pageIndex = p ?? 1;
                var allTopic = _contactService.GetAllSoytePost(pageIndex, SiteConstants.Instance.AdminListPageSize);

                // Redisplay list of users

                var vm = new SoyteInfoViewModel()
                {
                    SoyteInfo = allTopic,
                    PageIndex = allTopic.PageIndex,
                    TotalPages = allTopic.TotalPages,
                    TotalCount = allTopic.TotalCount,
                    Search = search
                };

                return View(vm);
            }
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult CreateInfoSoyte()
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var info = new SoyteModel();
                return View(info);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult CreateInfoSoyte(SoyteModel model)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {

                    var info = new SoyteInfo();

                    // Map everything in model except properties hidden on page
                    info.Url = model.Url;
                    info.Title = model.Name;
                    info.Type = model.Type;
                    info.Status = "A";
                    info.CreateDate = DateTime.Now;
                    info.NgayApdung = DateTime.Now;
                    info.UserId = LoggedOnReadOnlyUser.Id;

                    _contactService.AddSoyteInfo(info);
                    try
                    {
                        unitOfWork.Commit();

                        ViewBag.Message = new GenericMessageViewModel
                        {
                            Message = "Đã thêm thành công",
                            MessageType = GenericMessages.success
                        };
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                        LoggingService.Error(ex);
                        ModelState.AddModelError(string.Empty,
                            LocalizationService.GetResourceString("Errors.GenericMessage"));
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
            return RedirectToAction("SoyteList", "Common", new { area = "admin" });
        }

        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult EditInfoSoyte(Guid id)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var info = _contactService.GetInfoSoyte(id);
                var vm = new SoyteModel() { Id = info.Id, Name = info.Title, Url = info.Url, Status = info.Status, Type = info.Type };
                return View(vm);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult EditInfoSoyte(SoyteModel model)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {

                    var info = _contactService.GetInfoSoyte(model.Id);

                    // Map everything in model except properties hidden on page
                    info.Url = model.Url;
                    info.Title = model.Name;
                    info.Type = model.Type;
                    info.Status = model.Status;
                    info.CreateDate = DateTime.Now;
                    //  info.NgayApdung = DateTime.Now;
                    info.UserId = LoggedOnReadOnlyUser.Id;

                    _contactService.UpdateSoyteInfon(info);
                    try
                    {
                        unitOfWork.Commit();

                        ViewBag.Message = new GenericMessageViewModel
                        {
                            Message = "Đã update thành công",
                            MessageType = GenericMessages.success
                        };
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                        LoggingService.Error(ex);
                        ModelState.AddModelError(string.Empty,
                            LocalizationService.GetResourceString("Errors.GenericMessage"));
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
            return RedirectToAction("SoyteList", "Common", new { area = "admin" });
        }

        #region BangGiaDichVu
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult ManageHospitalFee()
        {
            var rs = _hospitalFeeService.GetBangiaDichvu();
            return View(rs);
        }

        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult UploadHospitalFee(Guid id)
        {

            var entity = _hospitalFeeService.GetBangiaDichvubyId(id);

            var vm = new UploadHospitalFeeViewModel(entity);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult UploadHospitalFee(UploadHospitalFeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    if (vm.Files != null)
                    {

                        var uploadFolderPath =
                            HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath,
                                "HospitalFee/" + DateTime.Now.ToString("dd/MM/yyyy").Replace("/", "-") + "/" + vm.Type));
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
                                        Message = "Vui lòng kiểm tra lại thông tin file",

                                    }, JsonRequestBehavior.AllowGet);
                                }

                                var model = new BangiaDichvu()
                                {
                                    Id = vm.Id,
                                    FileName = vm.FileName,
                                    FileUrl = uploadResult.UploadedFileUrl,
                                    Type = vm.Type

                                };
                                _hospitalFeeService.UpdateBangiaDichvu(model);
                            }
                            else
                            {
                                return RedirectToAction("UploadHospitalFee", vm);
                            }
                        }

                        try
                        {
                            unitOfWork.Commit();

                            ViewBag.Message = new GenericMessageViewModel
                            {
                                Message = "Đã update thành công",
                                MessageType = GenericMessages.success
                            };
                        }
                        catch (Exception ex)
                        {
                            unitOfWork.Rollback();
                            LoggingService.Error(ex);
                            ModelState.AddModelError(string.Empty, LocalizationService.GetResourceString("Errors.GenericMessage"));
                        }
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

            return RedirectToAction("ManageHospitalFee", "Common", new { area = "admin" });
        }

        #endregion

        #region Department
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult ManageDepartment(int? p, string search)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var pageIndex = p ?? 1;
                var rs = string.IsNullOrEmpty(search) ? _doctorService.GetAllDepartment(pageIndex, SiteConstants.Instance.AdminListPageSize) :
                    _doctorService.SearchDepartment(search, pageIndex, SiteConstants.Instance.AdminListPageSize);

                // Redisplay list of Departments
                var vm = new DepartmentPageModel()
                {
                    Department = rs,
                    PageIndex = rs.PageIndex,
                    TotalPages = rs.TotalPages,
                    TotalCount = rs.TotalCount,
                    Search = search
                };

                return View(vm);
            }
        }
        [ChildActionOnly]
        public PartialViewResult GetMainDepartments()
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var viewModel = new ListDepartmentsViewModel
                {
                    Departments = _doctorService.GetAllDepartments().OrderBy(x => x.Name)
                };
                return PartialView(viewModel);
            }
        }

        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult CreateDepartment()
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var vm = new DepartmentViewModel
                {
                    AllDepartments = _doctorService.GetBaseSelectListDepartments(_doctorService.GetListDepartment())
                };
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult CreateDepartment(DepartmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    try
                    {
                        var model = vm.ToEntity();
                        var listDoctor = new List<Doctor>();

                        if (vm.ListDoctorId != null)
                        {
                            foreach (var doctorId in vm.ListDoctorId)
                            {
                                var doctor = _doctorService.Get(doctorId);
                                listDoctor.Add(doctor);
                            }

                            model.ListDoctor = listDoctor;
                        }

                        if (vm.Files != null)
                        {

                            var uploadFolderPath =
                                HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath, "Department" + "/" + model.Id));
                            if (!Directory.Exists(uploadFolderPath))
                            {
                                Directory.CreateDirectory(uploadFolderPath);
                            }

                            // Loop through each file and get the file info and save to the users folder and Db
                            var file = vm.Files[0];
                            if (file != null)
                            {
                                // If successful then upload the file
                                var uploadResult = AppHelpers.UploadFile(file, uploadFolderPath, LocalizationService, true);

                                if (!uploadResult.UploadSuccessful)
                                {
                                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                                    {
                                        Message = uploadResult.ErrorMessage,
                                        MessageType = GenericMessages.danger
                                    };
                                    return View(vm);
                                }

                                // Save image to data
                                model.ImageUrl = uploadResult.UploadedFileName;
                            }

                        }

                        if (vm.ParentDepartment != null)
                        {
                            var parentDepartment = _doctorService.GetDepartmentById(vm.ParentDepartment.Value);
                            model.ParentDepartment = parentDepartment;
                            SortPath(model, parentDepartment);
                        }

                        _doctorService.InsertDepartment(model);

                        // We use temp data because we are doing a redirect
                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Department Created",
                            MessageType = GenericMessages.success
                        };
                        unitOfWork.Commit();
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "There was an error creating the department");
            }

            return RedirectToAction("ManageDepartment");
        }

        private DepartmentViewModel CreateEditDepartmentViewModel(Department department)
        {
            var viewModel = new DepartmentViewModel();

            viewModel.Id = department.Id;
            viewModel.Name = department.Name;
            viewModel.Description = department.Description;
            viewModel.Slug = department.Slug;
            viewModel.ImageUrl = department.ImageUrl;
            viewModel.Slogan = department.Slogan;
            viewModel.ParentDepartment =
                department.ParentDepartment == null ? Guid.Empty : department.ParentDepartment.Id;
            viewModel.AllDepartments = _doctorService.GetBaseSelectListDepartments(_doctorService.GetListDepartment()
                .Where(x => x.Id != department.Id)
                .ToList());
            viewModel.ListDoctor = department.ListDoctor;
            viewModel.ListDoctorId = department.ListDoctor.Select(x => x.Id).ToList();
            viewModel.OrderNumber = department.OrderNumber;
            return viewModel;
        }

        [HttpGet]
        [Authorize(Roles = AppConstants.AdminRoleName)]
        public ActionResult UpdateDepartment(Guid id)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var department = _doctorService.GetDepartmentById(id);
                var vm = CreateEditDepartmentViewModel(department);

                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName)]
        public ActionResult UpdateDepartment(DepartmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    try
                    {
                        var department = _doctorService.GetDepartmentById(vm.Id);

                        if (vm.ListDoctorId != null)
                        {
                            vm.ListDoctor = new List<Doctor>();
                            foreach (var doctorId in vm.ListDoctorId)
                            {
                                var doctor = _doctorService.Get(doctorId);
                                vm.ListDoctor.Add(doctor);
                            }
                            department.ListDoctor = vm.ListDoctor;
                        }


                        //// Check they are not trying to add a subdepartment of this department as the parent or it will break
                        //if (department.Path != null && vm.ParentDepartment != null)
                        //{
                        //    var parentDepartments = department.Path.Split(',').Where(x => !string.IsNullOrEmpty(x))
                        //        .Select(x => new Guid(x)).ToList();
                        //    if (parentDepartments.Contains((Guid)vm.ParentDepartment))
                        //    {
                        //        // Remove the parent department, but still let them create the department
                        //        vm.ParentDepartment = null;
                        //    }
                        //}
                        if (department.ParentDepartment == null)
                        {
                            if (vm.ParentDepartment != null)
                            {
                                var parentDepartment = _doctorService.GetDepartmentById(vm.ParentDepartment.Value);
                                department.ParentDepartment = parentDepartment;
                                SortPath(department, parentDepartment);
                            }
                        }
                        else
                        {
                            if (vm.ParentDepartment != null && vm.ParentDepartment != department.ParentDepartment.Id)
                            {
                                var parentDepartment = _doctorService.GetDepartmentById(vm.ParentDepartment.Value);
                                department.ParentDepartment = parentDepartment;
                                SortPath(department, parentDepartment);
                            }
                        }

                        // Sort image out first
                        if (vm.Files != null)
                        {
                            // Before we save anything, check the user already has an upload folder and if not create one
                            var uploadFolderPath =
                                HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath, "Department" + "/" + vm.Id));
                            if (!Directory.Exists(uploadFolderPath))
                            {
                                Directory.CreateDirectory(uploadFolderPath);
                            }

                            // Loop through each file and get the file info and save to the users folder and Db
                            var file = vm.Files[0];
                            if (file != null)
                            {
                                // If successful then upload the file
                                var uploadResult = AppHelpers.UploadFile(file, uploadFolderPath,
                                    LocalizationService, true);

                                if (!uploadResult.UploadSuccessful)
                                {
                                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                                    {
                                        Message = uploadResult.ErrorMessage,
                                        MessageType = GenericMessages.danger
                                    };
                                    return View(vm);
                                }

                                // Save image to data
                                department.ImageUrl = uploadResult.UploadedFileName;
                            }

                        }


                        department.Name = vm.Name;
                        department.Slug = vm.Slug;
                        department.Description = vm.Description;
                        department.Slogan = vm.Slogan;
                        department.OrderNumber = vm.OrderNumber;
                        if (vm.ParentDepartment != null)
                        {
                            // Set the parent department
                            var parentDepartment = _doctorService.GetDepartmentById(vm.ParentDepartment.Value);
                            department.ParentDepartment = parentDepartment;

                            // Append the path from the parent department
                            SortPath(parentDepartment, parentDepartment);
                        }
                        else
                        {
                            // Must access property (trigger lazy-loading) before we can set it to null (Entity Framework bug!!!)
                            var triggerEfLoad = department.ParentDepartment;
                            department.ParentDepartment = null;

                            // Also clear the path
                            department.Path = null;
                        }

                        _doctorService.UpdateSlugFromName(department);

                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Department Updated",
                            MessageType = GenericMessages.success
                        };

                        //vm = CreateEditDepartmentViewModel(department);

                        unitOfWork.Commit();
                    }
                    catch (Exception ex)
                    {
                        LoggingService.Error(ex);
                        unitOfWork.Rollback();

                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Department Update Failed",
                            MessageType = GenericMessages.danger
                        };
                        return View(vm);
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
            return RedirectToAction("ManageDepartment");
        }

        private void SortPath(Department department, Department parentDepartment)
        {
            // Append the path from the parent department
            var path = string.Empty;
            if (!string.IsNullOrEmpty(parentDepartment.Path))
            {
                path = string.Concat(parentDepartment.Path, ",", parentDepartment.Id.ToString());
            }
            else
            {
                path = parentDepartment.Id.ToString();
            }

            department.Path = path;
        }
        public JsonResult GetKeySearch(string q)
        {

            var data = _doctorService.SearchDoctor(q).Take(20).ToList();

            var result = new HttpContentResult<dynamic>();
            result.TotalItem = data.Count();
            result.Data = data;
            result.StatusCode = "200";
            result.Message = "Success Load";
            result.Result = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = AppConstants.AdminRoleName)]
        public ActionResult DeleteDepartmentConfirmation(Guid id)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var department = _doctorService.GetDepartmentById(id);
                var subDepartments = _doctorService.GetAllSubDepartmetns(id).ToList();
                var viewModel = new DeleteDepartmentViewModel
                {
                    Id = department.Id,
                    Department = department,
                    SubDepartments = subDepartments
                };

                return View(viewModel);
            }
        }

        [Authorize(Roles = AppConstants.AdminRoleName)]
        public ActionResult DeleteDepartment(Guid id)
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    var department = _doctorService.GetDepartmentById(id);
                    _doctorService.DeleteDepartment(department);
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Department Deleted",
                        MessageType = GenericMessages.success
                    };
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                }

                return RedirectToAction("ManageDepartment");
            }
        }

        //public ActionResult DeleteDoctor(Guid doctorId, Guid departmentId)
        //{
        //    using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
        //    {
        //        try
        //        {
        //            var department = _doctorService.GetDepartmentById(departmentId);

        //            var doctor = _doctorService.Get(doctorId);
        //            department.ListDoctor.Remove(doctor);
        //            _doctorService.UpdateSlugFromName(department);

        //            // We use temp data because we are doing a redirect
        //            TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
        //            {
        //                Message = "Cập nhật nhân sự quản lý thành công!",
        //                MessageType = GenericMessages.success
        //            };
        //            unitOfWork.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            unitOfWork.Rollback();
        //            TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
        //            {
        //                Message = "Cập nhật nhân sự quản lý thất bại!",
        //                MessageType = GenericMessages.warning
        //            };
        //        }
        //    }

        //    return RedirectToAction("UpdateDepartment", new{ Id = departmentId});
        //}

        #endregion
        #region Doctor
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult ManageDoctor(string departmentSlug, string departmentManage_Slug, string doctor, int p = 1)
        {
            ViewBag.ListDepartment = _doctorService.GetListDepartment();
            ViewBag.DepartmentSlug = departmentSlug;
            ViewBag.DepartmentManage_Slug = departmentManage_Slug;
            ViewBag.Doctor = doctor;
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var rs = _doctorService.GetDoctorSearch(departmentSlug, departmentManage_Slug, doctor, p, 10);
                return View(rs);
            }
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult CreateDoctor()
        {
            ViewBag.ListDepartment = _doctorService.GetListDepartment();
            DoctorViewModel doctorViewModel = new DoctorViewModel();
            return View(doctorViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult CreateDoctor(DoctorViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    var doctor = vm.ToEntity();
                    if (vm.Files != null)
                    {
                        var uploadFolderPath =
                            HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath,
                                "ImageDoctors"));
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

                                doctor.Avatar = uploadResult.UploadedFileName;
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

                    if (doctor.DepartmentId != Guid.Empty)
                    {
                        doctor.DepartmentManage = _doctorService.GetDepartmentById(doctor.DepartmentId);
                        doctor.Department = doctor.DepartmentManage?.Name;
                    }
                    else if (doctor.DepartmentId == Guid.Empty && vm.DepartmentManage != Guid.Empty)
                    {
                        doctor.DepartmentManage = _doctorService.GetDepartmentById(vm.DepartmentManage);
                        doctor.Department = doctor.DepartmentManage?.Name;
                    }

                    _doctorService.Add(doctor);
                    try
                    {
                        unitOfWork.Commit();
                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Tạo mới thông tin bác sĩ thành công!",
                            MessageType = GenericMessages.success
                        };
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Tạo mới thông tin bác sĩ thất bại!",
                            MessageType = GenericMessages.success
                        };
                        LoggingService.Error(ex);
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
            return RedirectToAction("ManageDoctor");
        }

        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult UpdateDoctor(string slug)
        {
            ViewBag.ListDepartment = _doctorService.GetListDepartment();
            var doctor = _doctorService.GetDoctor(slug);
            var vm = new DoctorViewModel(doctor);
            vm.DepartmentManage = doctor.DepartmentManage?.ParentDepartment != null ? doctor.DepartmentManage?.ParentDepartment.Id ?? Guid.NewGuid() : doctor.DepartmentManage.Id;
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult UpdateDoctor(DoctorViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    var doctor = _doctorService.Get(vm.Id);
                    if (!vm.Name.Equals(doctor.Name))
                    {
                        doctor.Slug = "";
                    }

                    doctor.Name = vm.Name;
                    doctor.PositionName = vm.PositionName;
                    if (vm.IsDelete == true)
                    {
                        doctor.Status = "D";
                    }

                    doctor.Specicality = vm.Specicality;
                    if (vm.DepartmentId != Guid.Empty)
                    {
                        //doctor.DepartmentManage = vm.Department
                        doctor.DepartmentId = vm.DepartmentId;
                        doctor.DepartmentManage = _doctorService.GetDepartmentById(vm.DepartmentId);
                        doctor.Department = doctor.DepartmentManage?.Name;
                    }
                    else if (vm.DepartmentId == Guid.Empty && vm.DepartmentManage != Guid.Empty)
                    {
                        doctor.DepartmentManage = _doctorService.GetDepartmentById(vm.DepartmentManage);
                        doctor.Department = doctor.DepartmentManage?.Name;
                    }

                    doctor.DescriptionHistory = vm.DescriptionHistory;
                    doctor.BoardCertifications = vm.BoardCertifications;


                    if (vm.Files != null && doctor.Status == "A")
                    {
                        var uploadFolderPath =
                            HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath,
                                "ImageDoctors"));
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

                                doctor.Avatar = uploadResult.UploadedFileName;
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

                    _doctorService.Update(doctor);
                    try
                    {
                        unitOfWork.Commit();
                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Cập nhật thông tin bác sĩ thành công!",
                            MessageType = GenericMessages.success
                        };
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                        LoggingService.Error(ex);
                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Cập nhật thông tin bác sĩ thất bại!",
                            MessageType = GenericMessages.success
                        };
                        //RedirectToAction("ManageDoctor");
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
            return RedirectToAction("ManageDoctor");
        }

        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult DeleteDoctor(Guid id)
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    var doctor = _doctorService.Get(id);
                    _doctorService.Delete(doctor);
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Xóa thông tin bác sĩ thành công!",
                        MessageType = GenericMessages.success
                    };
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Xóa thông tin bác sĩ thất bại!",
                        MessageType = GenericMessages.success
                    };
                }

                return RedirectToAction("ManageDoctor");
            }
        }

        #endregion

        #region Recruitment
        [Authorize(Roles = AppConstants.AdminRoleName + ",TuyenDung,HoiDap,VietBai")]
        public ActionResult ManageRecruitment(string search, bool? isPending, int? p)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                // Set the page index
                var pageIndex = p ?? 1;

                ViewBag.Search = search;
                if (isPending == null)
                {
                    isPending = false;
                }
                ViewBag.IsPending = isPending;

                var rs = _recruitmentService.GetAdminAllPageRecruitment(search, isPending.Value, pageIndex, 10);
                return View(rs);
            }
        }

        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai,TuyenDung")]
        public ActionResult CreateRecruitment()
        {
            var vm = new RecruitmentViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai,TuyenDung")]
        public ActionResult CreateRecruitment(RecruitmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    var recruitment = vm.ToEntity();

                    var uploadFolderPath =
                        HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath,
                            "Recruitment" + "/" + recruitment.DateCreated.ToString("yyyyMMdd")));
                    if (!Directory.Exists(uploadFolderPath))
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }

                    if (vm.Files[0] != null)
                    {
                        // Loop through each file and get the file info and save to the users folder and Db
                        // var file = vm.Files[0];
                        if (vm.Files.Length > 0)
                        {
                            List<string> filedata = new List<string>();
                            foreach (var file in vm.Files)
                            {
                                var uploadResult = AppHelpers.UploadFile(file, uploadFolderPath, LocalizationService);

                                if (!uploadResult.UploadSuccessful)
                                {
                                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                                    {
                                        Message = uploadResult.ErrorMessage,
                                        MessageType = GenericMessages.danger
                                    };
                                    return View(vm);
                                }

                                filedata.Add(uploadResult.UploadedFileUrl);

                            }

                            // If successful then upload the file
                            recruitment.FileUrl = string.Join(";", filedata);

                            // Save file to data

                        }
                    }

                    if (vm.FileImages != null)
                    {
                        var fileImage = vm.FileImages[0];
                        if (fileImage != null)
                        {
                            // If successful then upload the file
                            var uploadResult =
                                AppHelpers.UploadFile(fileImage, uploadFolderPath, LocalizationService, true);

                            if (!uploadResult.UploadSuccessful)
                            {
                                TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                                {
                                    Message = uploadResult.ErrorMessage,
                                    MessageType = GenericMessages.danger
                                };
                                return View(vm);
                            }

                            // Save image to data
                            recruitment.ImageUrl = uploadResult.UploadedFileUrl;
                        }
                    }

                    recruitment.LastUpdated = DateTime.Now;
                    recruitment.IsPending = true;
                    _recruitmentService.Add(recruitment);
                    vm.Id = recruitment.Id;
                    try
                    {
                        unitOfWork.Commit();
                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Tạo mới tin tuyển dụng thành công!",
                            MessageType = GenericMessages.success
                        };
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Tạo mới tin tuyển dụng thất bại!",
                            MessageType = GenericMessages.success
                        };
                        LoggingService.Error(ex);
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
            //return RedirectToAction("ManageRecruitment", new { isPending = recruitment.IsPending });
            return RedirectToAction("ReviewRecruitment", "Home", new { area = "", id = vm.Id });
            
        }

        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai,TuyenDung")]
        public ActionResult UpdateRecruitment(Guid id)
        {
            var recruitment = _recruitmentService.Get(id);
            var vm = new RecruitmentViewModel(recruitment);
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai,TuyenDung")]
        public ActionResult UpdateRecruitment(RecruitmentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    var recruitment = _recruitmentService.Get(vm.Id);


                    var uploadFolderPath =
                        HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath,
                            "Recruitment" + "/" + recruitment.DateCreated.ToString("yyyyMMdd")));
                    if (!Directory.Exists(uploadFolderPath))
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }

                    if (vm.Files[0] != null)
                    {
                        // Loop through each file and get the file info and save to the users folder and Db
                        // var file = vm.Files[0];
                        if (vm.Files.Length > 0)
                        {
                            List<string> filedata = new List<string>();
                            foreach (var file in vm.Files)
                            {
                                var uploadResult = AppHelpers.UploadFile(file, uploadFolderPath, LocalizationService);

                                if (!uploadResult.UploadSuccessful)
                                {
                                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                                    {
                                        Message = uploadResult.ErrorMessage,
                                        MessageType = GenericMessages.danger
                                    };
                                    return View(vm);
                                }

                                filedata.Add(uploadResult.UploadedFileUrl);

                            }

                            // If successful then upload the file
                            recruitment.FileUrl = string.Join(";", filedata);

                            // Save file to data

                        }
                    }

                    if (vm.FileImages != null)
                    {
                        var fileImage = vm.FileImages[0];
                        if (fileImage != null)
                        {
                            // If successful then upload the file
                            var uploadResult =
                                AppHelpers.UploadFile(fileImage, uploadFolderPath, LocalizationService, true);

                            if (!uploadResult.UploadSuccessful)
                            {
                                TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                                {
                                    Message = uploadResult.ErrorMessage,
                                    MessageType = GenericMessages.danger
                                };
                                return View(vm);
                            }

                            // Save image to data
                            recruitment.ImageUrl = uploadResult.UploadedFileUrl;
                        }
                    }


                    if (!vm.Name.Equals(recruitment.Name))
                    {
                        recruitment.Slug = "";
                    }

                    recruitment.Name = vm.Name;
                    recruitment.FromSalary = vm.FromSalary;
                    recruitment.ToSalary = vm.ToSalary;
                    recruitment.Amount = vm.Amount;
                    recruitment.Description = vm.Description;
                    recruitment.Requirement = vm.Requirement;
                    recruitment.Benefit = vm.Benefit;
                    recruitment.ListPosition = vm.ListPosition;

                    _recruitmentService.Update(recruitment);
                    vm.Id = recruitment.Id;
                    try
                    {
                        unitOfWork.Commit();
                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Cập nhật tin tuyển dụng thành công!",
                            MessageType = GenericMessages.success
                        };
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                        LoggingService.Error(ex);
                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Cập nhật tin tuyển dụng thất bại!",
                            MessageType = GenericMessages.success
                        };
                        //RedirectToAction("ManageDoctor");
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

            //return RedirectToAction("ManageRecruitment", new { isPending = recruitment.IsPending });
            return RedirectToAction("ReviewRecruitment", "Home", new { area = "", id = vm.Id });
        }

        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai,TuyenDung")]
        public ActionResult DeleteRecruitment(Guid id)
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    var recruitment = _recruitmentService.Get(id);
                    recruitment.LastUpdated = DateTime.Now;
                    recruitment.UserDelete = System.Web.HttpContext.Current.User.Identity.Name;
                    recruitment.IsPending = true;
                    _recruitmentService.Update(recruitment);
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Xóa tin tuyển dụng thành công!",
                        MessageType = GenericMessages.success
                    };
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                }

                return RedirectToAction("ManageRecruitment", new { isPending = false });
            }
        }

        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai,TuyenDung")]
        public ActionResult ResetPendingRecruitment(Guid id)
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    var recruitment = _recruitmentService.Get(id);
                    recruitment.IsPending = false;
                    _recruitmentService.Update(recruitment);
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Xóa tin tuyển dụng thành công!",
                        MessageType = GenericMessages.success
                    };
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                }

                return RedirectToAction("ManageRecruitment", new { isPending = false });
            }
        }

        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai,TuyenDung")]
        public ActionResult ActiveRecruitment(Guid id, bool isActive)
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    var recruitment = _recruitmentService.Get(id);
                    recruitment.IsActive = isActive;
                    _recruitmentService.Update(recruitment);
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Thành công!",
                        MessageType = GenericMessages.success
                    };
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                }

                return RedirectToAction("ManageRecruitment", new { isPending = false });
            }
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai,TuyenDung")]
        public ActionResult ActiveReviewRecruitment(Guid id)
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    var recruitment = _recruitmentService.Get(id);
                    recruitment.IsPending = false;
                    _recruitmentService.Update(recruitment);
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Thành công!",
                        MessageType = GenericMessages.success
                    };
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                }

                return RedirectToAction("Tuyendung", "Home", new { area = "" });
            }
        }
        #endregion

        #region CV

        [Authorize(Roles = AppConstants.AdminRoleName + ",TuyenDung,VietBai")]
        public ActionResult ManageCv(string recruitmentId, string search, bool isDelete, int? p)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                // Set the page index
                var pageIndex = p ?? 1;

                var list = _recruitmentService.GetListRecruitment();
                if (list.Count == 0)
                {
                    return View();
                }
                ViewBag.ListRecruitment = list;

                if (recruitmentId == null)
                {
                    recruitmentId = list.First().Id.ToString();
                    var recruitment = _recruitmentService.Get(new Guid(recruitmentId));
                    ViewBag.RecruitmentId = recruitmentId;
                    ViewBag.RecruitmentName = recruitment.Name;
                    ViewBag.Search = search;
                    ViewBag.IsDelete = isDelete;
                    var rs = _recruitmentService.GetAllPageApplyCV(recruitmentId, search, isDelete, pageIndex, 10);
                    return View(rs);
                }
                else
                {
                    var recruitment = _recruitmentService.Get(new Guid(recruitmentId));
                    ViewBag.RecruitmentId = recruitment.Id;
                    ViewBag.RecruitmentName = recruitment.Name;
                    ViewBag.Search = search;
                    ViewBag.IsDelete = isDelete;
                    var rs = _recruitmentService.GetAllPageApplyCV(recruitment.Id.ToString(), search, isDelete, pageIndex, 10);
                    return View(rs);
                }


            }
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai,TuyenDung")]
        public ActionResult XuatExcelTuyenDung(string recruitmentId, string search, string recruitmentName)
        {
            var gv = new GridView();
            var list = _recruitmentService.GetAllApplyCVByReId(recruitmentId, search);
            gv.DataSource = list.Select(x => new ApplyCvExcelViewModel(x)).OrderBy(x => x.TrangThai).ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", $"attachment; filename= thongbaotuyendung.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "UTF-8";
            Response.HeaderEncoding = Encoding.UTF8;
            Response.ContentEncoding = Encoding.UTF8;

            Response.BinaryWrite(Encoding.UTF8.GetPreamble());

            // Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();

            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter);
            Response.Flush();
            Response.End();
            return View("ManageCv");
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai,TuyenDung")]
        public ActionResult EditCv(Guid id)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var cv = _recruitmentService.GetApplyCV(id);
                var vm = new AdminApplyCvViewModel(cv);
                return View(vm);
            }
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai,TuyenDung")]
        public ActionResult XemCv(Guid id)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var cv = _recruitmentService.GetApplyCV(id);
                var vm = new AdminApplyCvViewModel(cv);
                return View(vm);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai,TuyenDung")]
        public ActionResult EditCv(AdminApplyCvViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    var cv = _recruitmentService.GetApplyCV(vm.Id);


                    // Map everything in model except properties hidden on page
                    cv.ReplyContent = vm.ReplyContent;
                    cv.UserReply = System.Web.HttpContext.Current.User.Identity.Name;
                    vm.IsDelete = cv.IsDelete;
                    vm.Recruitment.Id = cv.Recruitment.Id;
                    try
                    {
                        _recruitmentService.UpdateApplyCV(cv);
                        SendEmailApplyCVEmail(new MailTo()
                        {
                            Title = "Hồ sơ của bạn đã được phản hồi",
                            Name = cv.FullName,
                            Content = $"Nội dung trả lời: {cv.ReplyContent ?? "Đã nhận được hồ sơ."}",
                            Email = cv.Email

                        });
                        unitOfWork.Commit();
                        ViewBag.Message = new GenericMessageViewModel
                        {
                            Message = "Trả lời hồ sơ thành công!",
                            MessageType = GenericMessages.success
                        };
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                        LoggingService.Error(ex);
                        ModelState.AddModelError(string.Empty,
                            LocalizationService.GetResourceString("Errors.GenericMessage"));
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
            return RedirectToAction("ManageCv", new { isDelete = vm.IsDelete, recruitmentId = vm.Recruitment.Id });
        }
        [Authorize(Roles = AppConstants.AdminRoleName)]
        public ActionResult DeleteCv(Guid id)
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    var cv = _recruitmentService.GetApplyCV(id);
                    cv.IsDelete = true;
                    cv.LastUpdated = DateTime.Now;
                    cv.UserDelete = System.Web.HttpContext.Current.User.Identity.Name;
                    _recruitmentService.UpdateApplyCV(cv);
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Xóa hồ sơ ứng tuyển thành công!",
                        MessageType = GenericMessages.success
                    };
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                }

                return RedirectToAction("ManageCv", new { isDelete = false });
            }
        }
        #endregion

        #region GoiKhamBenh
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult ManageMedicalPackage(int? p)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var pageIndex = p ?? 1;
                var rs = _medicalPackageService.GetAllMedicalPackage(pageIndex, SiteConstants.Instance.AdminListPageSize);

                // Redisplay list of Departments
                var vm = new MedicalPackagePageModel()
                {
                    MedicalPackage = rs,
                    PageIndex = rs.PageIndex,
                    TotalPages = rs.TotalPages,
                    TotalCount = rs.TotalCount
                };

                return View(vm);
            }
        }
        [ChildActionOnly]
        public PartialViewResult GetAllMedicalPackages()
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var viewModel = new ListMedicalPackagesViewModel
                {
                    MedicalPackages = _medicalPackageService.GetAllMedicalPackages().OrderBy(x => x.Name)
                };
                return PartialView(viewModel);
            }
        }

        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult CreateMedicalPackage()
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var vm = new MedicalPackageViewModel
                {
                    AllMedicalPackages = _medicalPackageService.GetBaseSelectListDepartments(_medicalPackageService.GetListMedicalPackage())
                };
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult CreateMedicalPackage(MedicalPackageViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    try
                    {
                        var model = vm.ToEntity();


                        if (vm.Files != null)
                        {

                            var uploadFolderPath =
                                HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath, "GoiKhamBenh" + "/" + model.Id));
                            if (!Directory.Exists(uploadFolderPath))
                            {
                                Directory.CreateDirectory(uploadFolderPath);
                            }

                            // Loop through each file and get the file info and save to the users folder and Db
                            var file = vm.Files[0];
                            if (file != null)
                            {
                                // If successful then upload the file
                                var uploadResult = AppHelpers.UploadFile(file, uploadFolderPath, LocalizationService, true);

                                if (!uploadResult.UploadSuccessful)
                                {
                                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                                    {
                                        Message = uploadResult.ErrorMessage,
                                        MessageType = GenericMessages.danger
                                    };
                                    return View(vm);
                                }

                                // Save image to data
                                model.FileUrl = uploadResult.UploadedFileName;
                            }

                        }

                        if (vm.Parent != null)
                        {
                            var parent = _medicalPackageService.GetMedicalPackageById(vm.Parent.Value);
                            model.Parent = parent;
                            SortPathMedicalPackage(model, parent);
                        }

                        _medicalPackageService.InsertMedicalPackage(model);

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
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Thất bại!");
            }

            return RedirectToAction("ManageMedicalPackage");
        }


        private MedicalPackageViewModel CreateEditMedicalPackageViewModel(MedicalPackage entity)
        {
            var viewModel = new MedicalPackageViewModel();

            viewModel.Id = entity.Id;
            viewModel.Name = entity.Name;
            viewModel.Slug = entity.Slug;
            viewModel.FileUrl = entity.FileUrl;
            viewModel.Parent = entity.Parent == null ? Guid.Empty : entity.Parent.Id;
            viewModel.AllMedicalPackages = _medicalPackageService.GetBaseSelectListDepartments(_medicalPackageService.GetListMedicalPackage()
                .Where(x => x.Id != entity.Id)
                .ToList());
            viewModel.OrderNumber = entity.OrderNumber;
            return viewModel;
        }

        [HttpGet]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult UpdateMedicalPackage(Guid id)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var entity = _medicalPackageService.GetMedicalPackageById(id);
                var vm = CreateEditMedicalPackageViewModel(entity);

                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult UpdateMedicalPackage(MedicalPackageViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    try
                    {
                        var entity = _medicalPackageService.GetMedicalPackageById(vm.Id);

                        if (entity.Parent == null)
                        {
                            if (vm.Parent != null)
                            {
                                var parent = _medicalPackageService.GetMedicalPackageById(vm.Parent.Value);
                                entity.Parent = parent;
                                SortPathMedicalPackage(entity, parent);
                            }
                        }
                        else
                        {
                            if (vm.Parent != null && vm.Parent != entity.Parent.Id)
                            {
                                var parent = _medicalPackageService.GetMedicalPackageById(vm.Parent.Value);
                                entity.Parent = parent;
                                SortPathMedicalPackage(entity, parent);
                            }
                        }

                        // Sort image out first
                        if (vm.Files != null)
                        {
                            // Before we save anything, check the user already has an upload folder and if not create one
                            var uploadFolderPath =
                                HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath, "Department" + "/" + vm.Id));
                            if (!Directory.Exists(uploadFolderPath))
                            {
                                Directory.CreateDirectory(uploadFolderPath);
                            }

                            // Loop through each file and get the file info and save to the users folder and Db
                            var file = vm.Files[0];
                            if (file != null)
                            {
                                // If successful then upload the file
                                var uploadResult = AppHelpers.UploadFile(file, uploadFolderPath,
                                    LocalizationService, true);

                                if (!uploadResult.UploadSuccessful)
                                {
                                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                                    {
                                        Message = uploadResult.ErrorMessage,
                                        MessageType = GenericMessages.danger
                                    };
                                    return View(vm);
                                }

                                // Save image to data
                                entity.FileUrl = uploadResult.UploadedFileName;
                            }

                        }


                        entity.Name = vm.Name;
                        entity.Slug = vm.Slug;
                        entity.OrderNumber = vm.OrderNumber;
                        if (vm.Parent != null)
                        {
                            // Set the parent department
                            var parent = _medicalPackageService.GetMedicalPackageById(vm.Parent.Value);
                            entity.Parent = parent;

                            // Append the path from the parent department
                            SortPathMedicalPackage(parent, parent);
                        }
                        else
                        {
                            // Must access property (trigger lazy-loading) before we can set it to null (Entity Framework bug!!!)
                            var triggerEfLoad = entity.Parent;
                            entity.Parent = null;

                            // Also clear the path
                            entity.Path = null;
                        }

                        _medicalPackageService.UpdateSlugFromName(entity);

                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Thành công!",
                            MessageType = GenericMessages.success
                        };

                        //vm = CreateEditDepartmentViewModel(department);

                        unitOfWork.Commit();
                    }
                    catch (Exception ex)
                    {
                        LoggingService.Error(ex);
                        unitOfWork.Rollback();

                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Thất bại!",
                            MessageType = GenericMessages.danger
                        };
                        return View(vm);
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
            return RedirectToAction("ManageMedicalPackage");
        }

        private void SortPathMedicalPackage(MedicalPackage entity, MedicalPackage parent)
        {
            // Append the path from the parent department
            var path = string.Empty;
            if (!string.IsNullOrEmpty(parent.Path))
            {
                path = string.Concat(parent.Path, ",", parent.Id.ToString());
            }
            else
            {
                path = parent.Id.ToString();
            }

            entity.Path = path;
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult DeleteMedicalPackageConfirmation(Guid id)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                var entity = _medicalPackageService.GetMedicalPackageById(id);
                var sub = _medicalPackageService.GetAllSubMedicalPackages(id).ToList();
                var viewModel = new DeleteMedicalPackageViewModel
                {
                    Id = entity.Id,
                    MedicalPackage = entity,
                    SubMedicalPackages = sub
                };

                return View(viewModel);
            }
        }

        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult DeleteMedicalPackage(Guid id)
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    var entity = _medicalPackageService.GetMedicalPackageById(id);
                    _medicalPackageService.DeleteMedicalPackage(entity);
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Thành công!",
                        MessageType = GenericMessages.success
                    };
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                }

                return RedirectToAction("ManageMedicalPackage");
            }
        }


        #endregion


        #region Education
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult ManageEducation(string search, bool? isPending, int p = 1)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                ViewBag.Search = search;
                if (isPending == null)
                {
                    isPending = false;
                }
                ViewBag.IsPending = isPending;


                var rs = _educationService.GetAllPageEducation(search, isPending.Value, p, 10);
                return View(rs);
            }
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult CreateEducation()
        {
            var vm = new EducationViewModel();
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult CreateEducation(EducationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    if (vm.Files != null)
                    {

                        var uploadFolderPath =
                            HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath,
                                "Education"));
                        if (!Directory.Exists(uploadFolderPath))
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        // Loop through each file and get the file info and save to the users folder and Db
                        var file = vm.Files[0];
                        if (file != null)
                        {
                            // If successful then upload the file
                            var uploadResult = AppHelpers.UploadFile(file, uploadFolderPath, LocalizationService, true);

                            if (!uploadResult.UploadSuccessful)
                            {
                                TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                                {
                                    Message = uploadResult.ErrorMessage,
                                    MessageType = GenericMessages.danger
                                };
                                return View(vm);
                            }

                            // Save image to data
                            vm.ImageUrl = uploadResult.UploadedFileName;
                        }

                    }

                    var education = vm.ToEntity();
                    _educationService.Add(education);

                    try
                    {
                        unitOfWork.Commit();
                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Tạo mới tin đào tạo thành công!",
                            MessageType = GenericMessages.success
                        };
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Tạo mới tin đào tạo thất bại!",
                            MessageType = GenericMessages.success
                        };
                        LoggingService.Error(ex);
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
            return RedirectToAction("ManageEducation", new { isPending = vm.IsPending });
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult UpdateEducation(Guid id)
        {
            var education = _educationService.Get(id);
            var vm = new EducationViewModel(education);
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult UpdateEducation(EducationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    var education = _educationService.Get(vm.Id);

                    if (vm.Files != null)
                    {
                        // Before we save anything, check the user already has an upload folder and if not create one
                        var uploadFolderPath =
                            HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath,
                                "Education"));
                        if (!Directory.Exists(uploadFolderPath))
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        // Loop through each file and get the file info and save to the users folder and Db
                        var file = vm.Files[0];
                        if (file != null)
                        {
                            // If successful then upload the file
                            var uploadResult = AppHelpers.UploadFile(file, uploadFolderPath,
                                LocalizationService, true);

                            if (!uploadResult.UploadSuccessful)
                            {
                                TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                                {
                                    Message = uploadResult.ErrorMessage,
                                    MessageType = GenericMessages.danger
                                };
                                return View(vm);
                            }

                            // Save image to data
                            education.ImageUrl = uploadResult.UploadedFileName;
                        }

                    }


                    if (!vm.Name.Equals(education.Name))
                    {
                        education.Slug = "";
                    }

                    education.Name = vm.Name;
                    education.Description = vm.Description;
                    education.IsPending = false;

                    _educationService.Update(education);
                    try
                    {
                        unitOfWork.Commit();
                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Cập nhật tin đào tạo thành công!",
                            MessageType = GenericMessages.success
                        };
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                        LoggingService.Error(ex);
                        TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                        {
                            Message = "Cập nhật tin đào tạo thất bại!",
                            MessageType = GenericMessages.success
                        };
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
            return RedirectToAction("ManageEducation", new { isPending = vm.IsPending });
        }

        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult DeleteEducation(Guid id)
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    var education = _educationService.Get(id);
                    education.IsPending = true;
                    education.LastUpdated = DateTime.Now;
                    education.UserDelete = System.Web.HttpContext.Current.User.Identity.Name;
                    _educationService.Update(education);
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Xóa tin đào tạo thành công!",
                        MessageType = GenericMessages.success
                    };
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                }

                return RedirectToAction("ManageEducation", new { isPending = false });
            }
        }
        [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
        public ActionResult ResetPendingEducation(Guid id)
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    var education = _educationService.Get(id);
                    education.IsPending = false;
                    _educationService.Update(education);
                    TempData[AppConstants.MessageViewBagName] = new GenericMessageViewModel
                    {
                        Message = "Xóa tin đào tạo thành công!",
                        MessageType = GenericMessages.success
                    };
                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                }

                return RedirectToAction("ManageEducation", new { isPending = false });
            }
        }
        #endregion
        private void SendEmailConfirmationEmail(MailTo to)
        {
            var settings = SettingsService.GetSettings();
            var manuallyAuthoriseMembers = settings.ManuallyAuthoriseNewMembers;
            var memberEmailAuthorisationNeeded = settings.NewMemberEmailConfirmation == true;
            if (manuallyAuthoriseMembers == false && memberEmailAuthorisationNeeded)
            {
                if (!string.IsNullOrEmpty(to.Email))
                {
                    // SEND AUTHORISATION EMAIL
                    var sb = new StringBuilder();
                    var confirmationLink = string.Concat(StringUtils.ReturnCurrentDomain(), $"/hoi-dap/?id={to.Id}");
                    sb.AppendFormat("<p>{0}</p>", string.Format(to.Content));

                    sb.AppendFormat("<p>{0}</p>", string.Format("<p><a href=\"{0}\">{0}</a></p>", confirmationLink));
                    var email = new Email
                    {

                        EmailTo = to.Email,
                        NameTo = to.Name,
                        Subject = to.Title
                    };
                    email.Body = _emailService.EmailTemplate(email.NameTo, sb.ToString());
                    _emailService.SendMail(email);

                }
            }
        }
        private void SendEmailApplyCVEmail(MailTo to)
        {
            LoggingService.Error($"Email==>{to.Email}");
            var settings = SettingsService.GetSettings();
            var manuallyAuthoriseMembers = settings.ManuallyAuthoriseNewMembers;
            var memberEmailAuthorisationNeeded = settings.NewMemberEmailConfirmation == true;
            if (manuallyAuthoriseMembers == false && memberEmailAuthorisationNeeded)
            {
                if (!string.IsNullOrEmpty(to.Email))
                {
                    // SEND AUTHORISATION EMAIL
                    var sb = new StringBuilder();
                    sb.AppendFormat("<p>{0}</p>", string.Format(to.Content));

                    var email = new Email
                    {

                        EmailTo = to.Email,
                        NameTo = to.Name,
                        Subject = to.Title
                    };
                    email.Body = _emailService.EmailTemplate(email.NameTo, sb.ToString());
                    LoggingService.Error($"Email==>{email.Body}");
                    _emailService.SendMail(email);

                }
            }
        }
    }
}