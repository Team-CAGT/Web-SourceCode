using MVCForum.Domain.DomainModel;
using MVCForum.Domain.DomainModel.Attributes;
using MVCForum.Domain.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCForum.Website.Areas.Admin.ViewModels
{

    public class ContactFormViewModel
    {
        public PagedList<ContactForm> ContactForms { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string Search { get; set; }
    }
    public class QuestionsViewModel
    {
        public PagedList<Question> Questions { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string Search { get; set; }
    }
    public class AppointmentsViewModel
    {
        public PagedList<Appointment> Appointments { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string Search { get; set; }
    }
    public class SoyteInfoViewModel
    {
        public PagedList<SoyteInfo> SoyteInfo { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string Search { get; set; }
    }
    public class SoyteModel
    {
        [HiddenInput]
        public Guid Id { get; set; }
        [Required]
        [StringLength(600)]
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public List<SelectListItem> AllTypes { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public SoyteModel()
        {
            AllTypes = new List<SelectListItem>();
            AllTypes.Add(new SelectListItem { Value = "VANBAN", Text = "Văn bản" });
            AllTypes.Add(new SelectListItem { Value = "THUMOI", Text = "Thư mời" });
            AllTypes.Add(new SelectListItem { Value = "THONGBAO", Text = "Thông báo" });
            StatusList = new List<SelectListItem>();
            StatusList.Add(new SelectListItem { Value = "A", Text = "Hoạt động" });
            StatusList.Add(new SelectListItem { Value = "D", Text = "Tắt" });
            
        }
    }
   
}