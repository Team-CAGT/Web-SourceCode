using MVCForum.Domain.Constants;
using MVCForum.Domain.DomainModel.Entities;
using MVCForum.Domain.Interfaces.Services;
using MVCForum.Domain.Interfaces.UnitOfWork;
using MVCForum.Website.Application;
using MVCForum.Website.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace MVCForum.Website.Areas.Admin.Controllers
{
    [Authorize(Roles = AppConstants.AdminRoleName + ",VietBai")]
    public class BannerController : BaseAdminController
    {
        private readonly IBannerService _bannerService;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public BannerController(ILoggingService loggingService,
                                       IUnitOfWorkManager unitOfWorkManager,
                                      IMembershipService membershipService,
                                      ILocalizationService localizationService,
                                      IBannerService bannerService,
                                      ISettingsService settingsService)
            : base(loggingService, unitOfWorkManager, membershipService, localizationService, settingsService)
        {
            _bannerService = bannerService;
        }

        // GET: Admin/MedihubBanners
        public ActionResult Index()
        {
            return View(_bannerService.GetAll());
        }

        // GET: Admin/MedihubBanners/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = _bannerService.Get(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: Admin/MedihubBanners/Create
        public ActionResult Create()
        {
            return View(new BannerViewModel());
        }

        // POST: Admin/MedihubBanners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BannerViewModel medihubBanner)
        {
            if (ModelState.IsValid)
            {
                var newBanner = medihubBanner.ToEntity();
                newBanner.Id = Guid.NewGuid();
                var date = medihubBanner.Date.Split('-');
                newBanner.StartDate = DateTime.ParseExact(date[0].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                newBanner.EndDate = DateTime.ParseExact(date[1].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                if (medihubBanner.Files != null)
                {
                    // Before we save anything, check the user already has an upload folder and if not create one
                    var uploadFolderPath = HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath, "Banner" + "/" + newBanner.Id));
                    if (!Directory.Exists(uploadFolderPath))
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }

                    // Loop through each file and get the file info and save to the users folder and Db
                    var file = medihubBanner.Files[0];
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
                            return View(medihubBanner);
                        }

                        // Save avatar to user
                        newBanner.ImageLink = uploadResult.UploadedFileUrl;
                    }

                }


                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    try
                    {
                        _bannerService.Add(newBanner);
                        unitOfWork.Commit();
                    }
                    catch (Exception)
                    {
                        unitOfWork.Rollback();
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(medihubBanner);
        }

        // GET: Admin/MedihubBanners/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner medihubBanner = _bannerService.Get(id);
            if (medihubBanner == null)
            {
                return HttpNotFound();
            }
            var viewmodel = new BannerViewModel();
            var startDate = medihubBanner.StartDate.ToString("MM/dd/yyyy");
            var endDate = medihubBanner.EndDate.ToString("MM/dd/yyyy");
            viewmodel.Date = string.Join(" - ", startDate, endDate);
            viewmodel.MapFromEntity(medihubBanner);
            return View(viewmodel);
        }

        // POST: Admin/MedihubBanners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BannerViewModel medihubBanner)
        {
            if (ModelState.IsValid)
            {

                using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
                {
                    try
                    {
                        var banner = _bannerService.Get(medihubBanner.Id);
                        var date = medihubBanner.Date.Split('-');
                        banner.StartDate = DateTime.ParseExact(date[0].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        banner.EndDate = DateTime.ParseExact(date[1].Trim(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

                        if (medihubBanner.Files != null)
                        {
                            // Before we save anything, check the user already has an upload folder and if not create one
                            var uploadFolderPath =
                                HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath, "Banner" + "/" + banner.Id));
                            if (!Directory.Exists(uploadFolderPath))
                            {
                                Directory.CreateDirectory(uploadFolderPath);
                            }

                            // Loop through each file and get the file info and save to the users folder and Db
                            var file = medihubBanner.Files[0];
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
                                    return View(medihubBanner);
                                }

                                // Save image to data
                                banner.ImageLink = uploadResult.UploadedFileUrl;
                            }

                        }

                        banner.Name = medihubBanner.Name;
                        banner.LinkWeb = medihubBanner.LinkWeb;
                        banner.Position = medihubBanner.Position;
                        banner.Type = medihubBanner.Type;
                        banner.Title = medihubBanner.Title;

                        _bannerService.Update(banner);
                        unitOfWork.Commit();
                    }
                    catch (Exception)
                    {
                        unitOfWork.Rollback();
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(medihubBanner);
        }

        // GET: Admin/MedihubBanners/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner medihubBanner = _bannerService.Get(id);
            if (medihubBanner == null)
            {
                return HttpNotFound();
            }
            var viewmodel = new BannerViewModel();
            viewmodel.MapFromEntity(medihubBanner);
            return View(viewmodel);
        }

        // POST: Admin/MedihubBanners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Banner medihubBanner = _bannerService.Get(id);

            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    _bannerService.Delete(medihubBanner);
                    unitOfWork.SaveChanges();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }
    }
}