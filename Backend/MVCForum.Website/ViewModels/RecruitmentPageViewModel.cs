using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCForum.Domain.DomainModel;
using MVCForum.Domain.DomainModel.Entities;
using MVCForum.Website.Areas.Admin.ViewModels;

namespace MVCForum.Website.ViewModels
{
    public class RecruitmentPageViewModel
    {
        public List<Recruitment> Recruitments { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int? TotalPages { get; set; }
    }

    public class EducationPageViewModel
    {
        public List<Education> Educations { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int? TotalPages { get; set; }
    }
    public class ScientificResearchPageViewModel
    {
        public List<Topic> Topics { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public int? TotalPages { get; set; }
    }
}