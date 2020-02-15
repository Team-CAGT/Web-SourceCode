using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Website.Areas.Admin.ViewModels
{

   
    public class UploadHospitalFeeViewModel
    {
        public Guid Id { get; set; }
        public HttpPostedFileBase[] Files { get; set; }
        public string FileName { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Type { get; set; }

        public UploadHospitalFeeViewModel()
        {
            
        }
        public UploadHospitalFeeViewModel(BangiaDichvu entity)
        {
            Id = entity.Id;
            FileName = entity.FileName;
            DateCreated = entity.DateCreated;
            Type = entity.Type;
        }
    }
}