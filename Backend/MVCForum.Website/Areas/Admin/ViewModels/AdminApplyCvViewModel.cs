using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCForum.Domain.Constants;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Website.Areas.Admin.ViewModels
{
    public class AdminApplyCvViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Qualification { get; set; }
        public string Slug { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PositionName { get; set; }
        public string PhoneNumber { get; set; }
        public string FileUrl { get; set; }
        public string HinhAnhChungChi { get; set; }
        [DataType(DataType.MultilineText)]
        [UIHint(AppConstants.EditorType), AllowHtml]
        public string ReplyContent { get; set; }
        public string Status { get; set; }
        public bool IsDelete { get; set; }
        public string UserDelete { get; set; }
        public string UserReply { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual Recruitment Recruitment { get; set; }

        public AdminApplyCvViewModel()
        {

        }

        public AdminApplyCvViewModel(ApplyCV entity)
        {
            FullName = entity.FullName;
            Qualification = entity.Qualification;
            Slug = entity.Slug;
            Address = entity.Address;
            Email = entity.Email;
            PositionName = entity.PositionName;
            PhoneNumber = entity.PhoneNumber;
            FileUrl = entity.FileUrl;
            HinhAnhChungChi = entity.HinhAnhChungChi;
            ReplyContent = entity.ReplyContent;
            Status = entity.Status;
            IsDelete = entity.IsDelete;
            UserDelete = entity.UserDelete;
            UserReply = entity.UserReply;
            DateCreated = entity.DateCreated;
            DateOfBirth = entity.DateOfBirth;
            LastUpdated = DateTime.Now;
            Recruitment = entity.Recruitment;
        }
    }
}