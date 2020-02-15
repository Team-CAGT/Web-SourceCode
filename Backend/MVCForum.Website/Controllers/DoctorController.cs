using MVCForum.Domain.Constants;
using MVCForum.Domain.Interfaces.Services;
using MVCForum.Domain.Interfaces.UnitOfWork;
using MVCForum.Website.Application;
using MVCForum.Website.Areas.Admin.ViewModels;
using MVCForum.Website.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace MVCForum.Website.Controllers
{

    public class DoctorController : BaseController
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(ILoggingService loggingService, IUnitOfWorkManager unitOfWorkManager, IMembershipService membershipService,
            ILocalizationService localizationService, IRoleService roleService, ISettingsService settingsService, IDoctorService doctorService) :
            base(loggingService, unitOfWorkManager, membershipService, localizationService, roleService, settingsService)
        {
            _doctorService = doctorService;
        }
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BanGiamdoc()
        {
            return View();
        }
        public ActionResult Lichkhambenh()
        {
            return View();
        }
        public ActionResult SearchDoctor(string specicality, string name)
        {
            if(string.IsNullOrEmpty(specicality) && string.IsNullOrEmpty(name))
            {
                return View();
            }
            var list = _doctorService.GetallDoctor(name, specicality);
            var vm = list.Select(x => new DoctorViewModel(x)).ToList();
            return View(vm);
        }
        [ChildActionOnly]
        public ActionResult SearchDoctorForm()
        {
            
            return PartialView();
        }
      
        public ActionResult SearchDoctorResult(string specicality, string name)
        {
            var list = _doctorService.GetallDoctor(name, specicality);
            var vm = list.Select(x => new DoctorViewModel(x)).ToList();
         
            return PartialView(vm);
        }
        public ActionResult ProfileDoctor(string slug)
        {
            var doctor = _doctorService.GetDoctor(slug);
            var vm = new DoctorViewModel(doctor);
            return View(vm);
        }
        [ChildActionOnly]
        public ActionResult PopularSpecicality()
        {

            return PartialView();
        }
        // GET: Doctor
        [Authorize]
        public ActionResult CreateDoctor()
        {
            ViewBag.ListDepartment = _doctorService.GetListDepartment();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDoctor(DoctorViewModel vm)
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
                if (doctor.DepartmentId != null)
                {
                    doctor.DepartmentManage= _doctorService.GetDepartmentById(doctor.DepartmentId);
                }
                _doctorService.Add(doctor);
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
        public ActionResult EditDoctor(string slug)
        {
            ViewBag.ListDepartment = _doctorService.GetListDepartment();
            var doctor = _doctorService.GetDoctor(slug);
            var vm = new DoctorViewModel(doctor);
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDoctor(DoctorViewModel vm)
        {

            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                var doctor = _doctorService.Get(vm.Id);
             
                doctor.Name = vm.Name;
                doctor.PositionName = vm.PositionName;
                if(vm.IsDelete == true)
                {
                    doctor.Status = "D";
                }
            
                doctor.Specicality = vm.Specicality;
                if (!string.IsNullOrEmpty(vm.Department))
                {
                    //doctor.DepartmentManage = vm.Department
                    doctor.DepartmentId = vm.DepartmentId;
                    doctor.DepartmentManage = _doctorService.GetDepartmentById(vm.DepartmentId);
                    doctor.Department = doctor.DepartmentManage.Name;
                }
                doctor.DescriptionHistory = vm.DescriptionHistory;
                doctor.BoardCertifications = vm.BoardCertifications;


                if (vm.Files != null && doctor.Status =="A")
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

    }
}