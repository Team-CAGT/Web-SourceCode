using MVCForum.Domain.Interfaces.Services;
using System.Web.Mvc;
using MVCForum.Domain.Interfaces.UnitOfWork;
using MVCForum.Website.ViewModels;
using System.IO;
using System.Web.Hosting;
using MVCForum.Domain.Constants;
using MVCForum.Website.Application;
using MVCForum.Website.Areas.Admin.ViewModels;
using System;
using MVCForum.Domain.DomainModel.Enums;

namespace MVCForum.Website.Controllers
{
    public class ImageController : BaseController
    {
        private readonly IImageService _imageService;
        private readonly ICacheService _cacheService;
        public ImageController(ILoggingService loggingService,
            IUnitOfWorkManager unitOfWorkManager,
            IMembershipService membershipService, 
            ILocalizationService localizationService,
            IRoleService roleService,
            IImageService imageService,
            ICacheService cacheService,
            ISettingsService settingsService) : base(loggingService, 
                unitOfWorkManager, membershipService, localizationService,
                roleService, settingsService)
        {
            _imageService = imageService;
            _cacheService = cacheService;
        }

        public ActionResult Index()
        {
            var vm = new ImageViewModel();
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateImage(ImageViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    var image = vm.ToEntity();
                    if(vm.Files !=null)
                    {
                        var uploadFolderPath = HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath,
                                 "Image"));
                        if (!Directory.Exists(uploadFolderPath))
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }
                        foreach (var imageUpload in vm.Files)
                        {
                            if (imageUpload != null)
                            {
                                // If successful then upload image
                                var uploadResult = AppHelpers.UploadFile(imageUpload, uploadFolderPath,
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
                                        Message = "Vui lòng kiểm tra lại thông tin hình ảnh",

                                    }, JsonRequestBehavior.AllowGet);
                                }
                                image.ImageUrl = uploadResult.UploadedFileName;
                                
                            }
                        }
                    }
                    _imageService.Add(image);
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
                            Message = "Vui lòng kiểm tra lại thông tin hình ảnh",

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json(new
                {
                    Result = false,
                    Message = "Vui lòng kiểm tra lại thông tin hình ảnh",

                }, JsonRequestBehavior.AllowGet);
            }
            return View(vm);
        }

        [ChildActionOnly]
        public ActionResult Images(int? amountToShow)
        {
            using (UnitOfWorkManager.NewUnitOfWork())
            {
                if (amountToShow == null)
                {
                    amountToShow = 5;
                }



                var cacheKey = string.Concat("Images", UsersRole.Id, amountToShow);
                var viewModel = _cacheService.Get<ImagesViewModel>(cacheKey);
                if (viewModel == null)
                {
                    var images = _imageService.GetAll();
                    // create the view model
                    viewModel = new ImagesViewModel
                    {
                        Images = images,
                    };
                    _cacheService.Set(cacheKey, viewModel, CacheTimes.FiveMinute);
                }

                return PartialView(viewModel);
            }
        }
    }
}