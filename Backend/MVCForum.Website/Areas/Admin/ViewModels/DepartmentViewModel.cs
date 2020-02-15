using MVCForum.Domain.Constants;
using MVCForum.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCForum.Website.Areas.Admin.ViewModels
{
    public class ListDepartmentsViewModel
    {
        public IEnumerable<Department> Departments { get; set; }
    }
    public class DepartmentPageModel
    {
        public PagedList<Department> Department { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string Search { get; set; }
    }
    public class DepartmentViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [DisplayName("Tên Khoa/ Phòng")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public string Slug { get; set; }
        [DataType(DataType.MultilineText)]
        [UIHint(AppConstants.EditorType), AllowHtml]
        public string Slogan { get; set; }

        [DisplayName("Hình ảnh")]
        public HttpPostedFileBase[] Files { get; set; }
        public string ImageUrl { get; set; }

        [DisplayName("Giới thiệu")]
        [DataType(DataType.MultilineText)]
        [UIHint(AppConstants.EditorType), AllowHtml]
        public string Description { get; set; }

        [DisplayName("Khối phòng")]
        public Guid? ParentDepartment { get; set; }
        public List<SelectListItem> AllDepartments { get; set; }

        [DisplayName("Nhân sự quản lý")]
        public List<Guid> ListDoctorId { get; set; }
        public List<Doctor> ListDoctor { get; set; }
        [DisplayName("Số thứ tự")]
        public int OrderNumber { get; set; }
        public DepartmentViewModel()
        {

        }

        public Department ToEntity()
        {
            var department = new Department();
            department.Id = Guid.NewGuid();
            department.Name = Name;
            department.Description = Description;
            department.Slogan = Slogan;
            department.OrderNumber = OrderNumber;

            return department;
        }
    }
    public class DeleteDepartmentViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }
        public Department Department { get; set; }
        public List<Department> SubDepartments { get; set; }
    }
}