using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Website.Areas.Admin.ViewModels
{
    public class QuestionExcelViewModel
    {
        public string TieuDe { get; set; }
        public string Ten { get; set; }
        public string Tuoi { get; set; }
        public string SoDT { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string CauHoi { get; set; }
        public string CauTraLoi { get; set; }
        public DateTime NgayTao { get; set; }

        public QuestionExcelViewModel(Question entity)
        {
            TieuDe = entity.Title;
            Ten = entity.Name;
            Tuoi = entity.Age;
            SoDT = entity.Phone;
            DiaChi = entity.Address;
            Email = entity.Email;
            CauHoi = entity.ContentQuestion;
            CauTraLoi = entity.ReplyContent;
            NgayTao = entity.CreateDate;
        }
    }

    public class ContactExcelViewModel
    {
        public string Ten { get; set; }
        public string SoDT { get; set; }
        public string Email { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayTao { get; set; }
        public string TrangThai { get; set; }

        public ContactExcelViewModel(ContactForm entity)
        {
            Ten = entity.Name;
            NoiDung = entity.Content;
            SoDT = entity.Phone;
            TrangThai = entity.Status == "NEW" ? "Mới" : "Đã liên hệ";
            Email = entity.Email;
            NgayTao = entity.CreateDate;
        }
    }
    public class ApplyCvExcelViewModel
    {
        public string Ten { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string NgaySinh { get; set; }
        public string SoDT { get; set; }
        public string TrinhDoChuyenMon { get; set; }
        public string ViTri { get; set; }
        public DateTime NgayNop { get; set; }
        public string TrangThai { get; set; }

        public ApplyCvExcelViewModel(ApplyCV entity)
        {
            Ten = entity.FullName;
            DiaChi = entity.Address;
            NgaySinh = entity.DateOfBirth.ToString("dd/MM/yyyy");
            SoDT = entity.PhoneNumber;
            Email = entity.Email;
            TrinhDoChuyenMon = entity.Qualification;
            ViTri = entity.PositionName;
            NgayNop = entity.DateCreated;
            TrangThai = entity.UserReply != null ? "Đã phản hồi" : "Chưa phản hồi";
        }
    }
}