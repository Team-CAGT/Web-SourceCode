using System;
using System.Collections.Generic;
using MVCForum.Domain.DomainModel;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Domain.Interfaces.Services
{
    public interface IRecruitmentService
    {
        #region Recruitment

        Recruitment Get(Guid id);
        List<Recruitment> GetListRecruitment();
        Recruitment Add(Recruitment entity);
        PagedList<Recruitment> GetAllPageRecruitment(string search, bool isPending, int pageIndex, int pageSize);
        void Update(Recruitment entity);
        PagedList<Recruitment> GetAdminAllPageRecruitment(string search, bool isPending, int pageIndex, int pageSize);
        #endregion

        #region CV

        ApplyCV GetApplyCV(Guid id);
        ApplyCV AddApplyCV(ApplyCV entity);
        PagedList<ApplyCV> GetAllPageApplyCV(string recruitmentId, string search, bool isDelete, int pageIndex, int pageSize);
        void UpdateApplyCV(ApplyCV entity);
        List<ApplyCV> GetAllApplyCVByReId(string recruitmentId, string search);

        #endregion
    }
}
