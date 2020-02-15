using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Website.ViewModels
{
    public class RecruitmentCVViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Qualification { get; set; }
        public string Slug { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PositionName { get; set; }
        public string PhoneNumber { get; set; }
        public HttpPostedFileBase[] Files { get; set; }
        public HttpPostedFileBase[] Images { get; set; }
        public string FileUrl { get; set; }
        public string HinhAnhChungChi { get; set; }
        public string Status { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DateCreated { get; set; }
        public  string DateOfBirth { get; set; }
        public Recruitment Recruitment { get; set; }
        public Guid RecruitmentId { get; set; }
        public RecruitmentCVViewModel()
        {
        }

        public RecruitmentCVViewModel(ApplyCV entity)
        {
            Id = entity.Id;
            FullName = entity.FullName;
            Slug = entity.Slug;
            Address = entity.Address;
            Email = entity.Email;
            PositionName = entity.PositionName;
            PhoneNumber = entity.PhoneNumber;
            Status = entity.Status;
            FileUrl = entity.FileUrl;
            HinhAnhChungChi = entity.HinhAnhChungChi;
            IsDelete = entity.IsDelete;
            Recruitment = entity.Recruitment;
            DateCreated = entity.DateCreated;
            DateOfBirth = entity.DateOfBirth.ToString("dd/MM/yyyy");
            Qualification = entity.Qualification;
        }

        public ApplyCV ToEntity()
        {
            var recruitment = new ApplyCV();
            recruitment.Id = Guid.NewGuid();
            recruitment.FullName = FullName;
            recruitment.Address = Address;
            recruitment.Email = Email;
            recruitment.PositionName = PositionName;
            recruitment.Status = "NEW";
            recruitment.PhoneNumber = PhoneNumber;
            recruitment.FileUrl = FileUrl;
            recruitment.HinhAnhChungChi = HinhAnhChungChi;
            recruitment.IsDelete = false;
            recruitment.DateCreated = DateTime.Now;
            recruitment.DateOfBirth = DateTime.ParseExact(DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            recruitment.LastUpdated = DateTime.Now;
            recruitment.Qualification = Qualification;
            recruitment.Recruitment = Recruitment;

            return recruitment;
        }
    }

    
}