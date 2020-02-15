using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCForum.Domain.Constants;
using MVCForum.Domain.DomainModel;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Website.Areas.Admin.ViewModels
{
    public class ListMedicalPackagesViewModel
    {
        public IEnumerable<MedicalPackage> MedicalPackages { get; set; }
    }
    public class MedicalPackagePageModel
    {
        public PagedList<MedicalPackage> MedicalPackage { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
    public class MedicalPackageViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [DisplayName("Tiêu đề")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public string Slug { get; set; }

        [DisplayName("Hình ảnh")]
        public HttpPostedFileBase[] Files { get; set; }
        public string FileUrl { get; set; }

        [DisplayName("Chuyên mục")]
        public Guid? Parent { get; set; }
        public List<SelectListItem> AllMedicalPackages { get; set; }

        [DisplayName("Số thứ tự")]
        public float OrderNumber { get; set; }

        public MedicalPackageViewModel()
        {

        }

        public MedicalPackage ToEntity()
        {
            var MedicalPackage = new MedicalPackage();
            MedicalPackage.Id = Guid.NewGuid();
            MedicalPackage.Name = Name;
            MedicalPackage.OrderNumber = OrderNumber;

            return MedicalPackage;
        }
    }
    public class DeleteMedicalPackageViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }
        public MedicalPackage MedicalPackage { get; set; }
        public List<MedicalPackage> SubMedicalPackages { get; set; }
    }
}