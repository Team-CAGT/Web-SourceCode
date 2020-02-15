using MVCForum.Domain.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCForum.Website.ViewModels
{
    public class ImageViewModel
    {


        public string Name { get; set; }
        public string Title { get; set; }
        public HttpPostedFileBase[] Files { get; set; }
        public string ImageUrl { get; set; }
        public Image ToEntity()
        {
            var image = new Image();
            image.Name = Name;
            image.Title = Title;
            image.ImageUrl = ImageUrl;
            image.DateCreated = DateTime.Now;
            return image;
        }
    }
    public class ImagesViewModel
    {
       public IList<Image> Images { get; set; }
    }

}