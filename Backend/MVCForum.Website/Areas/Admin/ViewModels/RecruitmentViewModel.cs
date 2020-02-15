using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCForum.Domain.Constants;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Website.Areas.Admin.ViewModels
{
    public class RecruitmentViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int FromSalary { get; set; }
        public int ToSalary { get; set; }
        public int Amount { get; set; }
        [DisplayName("Tệp tin đính kèm")]
        public HttpPostedFileBase[] Files { get; set; }
        [DisplayName("Hình ảnh")]
        public HttpPostedFileBase[] FileImages { get; set; }
        public string ImageUrl { get; set; }
        [DataType(DataType.MultilineText)]
        [UIHint(AppConstants.EditorType), AllowHtml]
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]
        [UIHint(AppConstants.EditorType), AllowHtml]
        public string Requirement { get; set; }
        [DataType(DataType.MultilineText)]
        [UIHint(AppConstants.EditorType), AllowHtml]
        public string Benefit { get; set; }
        public bool IsPending { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public string ListPosition { get; set; }
        public string FileUrl { get; set; }

        public RecruitmentViewModel()
        {
        }

        public RecruitmentViewModel(Recruitment entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Slug = entity.Slug;
            FromSalary = entity.FromSalary;
            ToSalary = entity.ToSalary;
            Amount = entity.Amount;
            ImageUrl = entity.ImageUrl;
            Description = entity.Description;
            Requirement = entity.Requirement;
            Benefit = entity.Benefit;
            IsPending = entity.IsPending;
            IsActive = entity.IsActive;
            DateCreated = entity.DateCreated;
            ListPosition = entity.ListPosition;
            FileUrl = entity.FileUrl;
        }

        public Recruitment ToEntity()
        {
            var recruitment = new Recruitment();
            recruitment.Id = Guid.NewGuid();
            recruitment.Name = Name;
            recruitment.FromSalary = 0;
            recruitment.ToSalary = 0;
            recruitment.Amount = 0;
            recruitment.Requirement = Requirement;
            recruitment.Benefit = Benefit;
            recruitment.ImageUrl = ImageUrl;
            recruitment.IsActive = false;
            recruitment.Description = Description;
            recruitment.DateCreated = DateTime.Now;
            recruitment.ListPosition = ListPosition;
            recruitment.FileUrl = FileUrl;

            return recruitment;
        }
    }
}