using MVCForum.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCForum.Website.ViewModels
{
    public class DoctorViewModel
    {
        public Guid Id { get; set; }
        public Guid DepartmentManage { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Avatar { get; set; }
        public string Specicality { get; set; }
        public string Department { get; set; }
        public Guid DepartmentId { get; set; }
        public string DescriptionHistory { get; set; }
        public string BoardCertifications { get; set; }
        public string TimeWorking { get; set; }
        public string Status { get; set; }
        public List<string> DescriptionHistorys { get; set; }
        public HttpPostedFileBase[] Files { get; set; }
        public string PositionName { get; set; }
        public bool IsDelete { get; set; }
        //public Guid DepartmentManageId { get; set; }
        public DoctorViewModel()
        {

        }
        public DoctorViewModel(Doctor doctor)
        {
            Id = doctor.Id;
            Name = doctor.Name;
            Slug = doctor.Slug;
            Avatar = doctor.Avatar;
            Specicality = doctor.Specicality;
            Department = doctor.Department;
            DepartmentId = doctor.DepartmentId;
            DescriptionHistory = doctor.DescriptionHistory;
            BoardCertifications = doctor.BoardCertifications;
            TimeWorking = doctor.TimeWorking;
            Status = doctor.Status;
            PositionName = doctor.PositionName;
            if (!string.IsNullOrEmpty(doctor.DescriptionHistory))
            {
                DescriptionHistorys = doctor.DescriptionHistory.Split(';').ToList();
            }

        }
        public Doctor  ToEntity()
        {
            Doctor doctor = new Doctor();
            doctor.Name = Name;
            doctor.Slug = Slug;
            doctor.Avatar = Avatar;
            doctor.Specicality = Specicality;
            doctor.Department = Department;
            doctor.DepartmentId = DepartmentId;
            doctor.DescriptionHistory =DescriptionHistory;
            doctor.BoardCertifications = BoardCertifications;
            doctor.TimeWorking = TimeWorking;
            doctor.Status =Status;
            doctor.PositionName =PositionName;
            return doctor;
        }
    }
}