using System;
using System.Collections.Generic;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Domain.Interfaces.Services
{
    public partial interface IImageService
    {
        Image Add(Image image);
        void Delete(Image image);
        IList<Image> GetAll();
        Image Get(Guid id);
    }
}
