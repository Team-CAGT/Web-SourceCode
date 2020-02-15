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
    public class EducationViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public HttpPostedFileBase[] Files { get; set; }
        public string ImageUrl { get; set; }
        [DataType(DataType.MultilineText)]
        [UIHint(AppConstants.EditorType), AllowHtml]
        public string Description { get; set; }
        public bool IsPending { get; set; }
        public DateTime DateCreated { get; set; }

        public EducationViewModel()
        {
        }

        public EducationViewModel(Education entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Slug = entity.Slug;
            ImageUrl = entity.ImageUrl;
            Description = entity.Description;
            IsPending = entity.IsPending;
            DateCreated = entity.DateCreated;
        }

        public Education ToEntity()
        {
            var recruitment = new Education();
            recruitment.Id = Guid.NewGuid();
            recruitment.Name = Name;
            recruitment.ImageUrl = ImageUrl;
            recruitment.Description = Description;
            recruitment.DateCreated = DateTime.Now;

            return recruitment;
        }
    }
}