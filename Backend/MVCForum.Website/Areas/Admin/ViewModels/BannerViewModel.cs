using MVCForum.Domain.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCForum.Domain.Constants;

namespace MVCForum.Website.Areas.Admin.ViewModels
{
    public class BannerViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [StringLength(600)]
        [Required]

        public string Name { get; set; }

        [Required]
        public string LinkWeb { get; set; }
        [Required]
        public string Type { get; set; }
        public List<SelectListItem> AllBannerType { get; set; }
      
     
        [Range(0, int.MaxValue)]
        public int Position { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        
        public string Date { get; set; }

        public string Status { get; set; } = "A";//Default

        public HttpPostedFileBase[] Files { get; set; }



        [System.ComponentModel.DisplayName("Image")]
        public string ImageLink { get; set; }
        [DataType(DataType.MultilineText)]
        [UIHint(AppConstants.EditorType), AllowHtml]
        public string Title { get; set; }
      
        public Banner ToEntity()
        {
            var entity = new Banner();
            //entity.EndDate = EndDate;
            entity.Id = Id;
            entity.ImageLink = ImageLink;
            
            entity.Name = Name;
            entity.Position = Position;
            //entity.StartDate = StartDate;
            entity.Status = Status;
            entity.Type = Type;
            entity.LinkWeb = LinkWeb;
            entity.Title = Title;
            return entity;

        }
        public void MapFromEntity(Banner entity)
        {
            this.Id = entity.Id;
            this.ImageLink = entity.ImageLink;
      
            this.Name = entity.Name;
            this.Position = entity.Position;
            this.StartDate = entity.StartDate;
            this.Status = entity.Status;
            this.Type = entity.Type;
            this.LinkWeb = entity.LinkWeb;
            this.EndDate = entity.EndDate;
            this.Title = entity.Title;
        }
    }
}