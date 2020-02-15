using System;
using System.Collections.Generic;
using MVCForum.Domain.DomainModel;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Domain.Interfaces.Services
{
    public interface IEducationService
    {
        #region Education

        Education Get(Guid id);
        List<Education> GetListEducation();
        Education Add(Education entity);
        PagedList<Education> GetAllPageEducation(string search, bool isPending, int pageIndex, int pageSize);
        void Update(Education entity);

        #endregion
    }
}
