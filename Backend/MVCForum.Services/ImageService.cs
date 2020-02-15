using System;
using System.Collections.Generic;
using System.Linq;
using MVCForum.Domain.DomainModel.Entities;
using MVCForum.Domain.Interfaces.Services;
using MVCForum.Services.Data.Context;
using System.Data.Entity;
using MVCForum.Domain.Interfaces;

namespace MVCForum.Services
{
    public partial class ImageService : IImageService
    {
        private readonly MVCForumContext _context;
        public ImageService(IMVCForumContext context)
        {
            _context = context as MVCForumContext;
        }


        public Image Add(Image image)
        {
            return _context.Image.Add(image);
        }

        public void Delete(Image image)
        {
            _context.Image.Remove(image);
        }

        public IList<Image> GetAll()
        {
            return _context.Image.ToList();
        }
        

        //public IList<Image> GetAllByUser(Guid membershipUserId)
        //{
        //    return _context.Image.Where(x => x.MembershipUser.Id == membershipUserId).ToList();
        //}

        public Image Get(Guid id)
        {
            return _context.Image.FirstOrDefault(x => x.Id == id);
        }
    }
}
