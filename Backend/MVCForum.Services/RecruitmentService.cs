using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MVCForum.Domain.DomainModel;
using MVCForum.Domain.DomainModel.Entities;
using MVCForum.Domain.Interfaces;
using MVCForum.Domain.Interfaces.Services;
using MVCForum.Services.Data.Context;

namespace MVCForum.Services
{
    public class RecruitmentService : IRecruitmentService
    {

        private readonly MVCForumContext _context;
        public RecruitmentService(IMVCForumContext context)
        {
            _context = context as MVCForumContext;
        }

        #region Recruitment

        public Recruitment Get(Guid id)
        {
            return _context.Recruitment.FirstOrDefault(x => x.Id == id);
        }
        public List<Recruitment> GetListRecruitment()
        {
            return _context.Recruitment.OrderByDescending(x => x.IsPending).ThenBy(x=>x.DateCreated).ToList();
        }
        public Recruitment Add(Recruitment entity)
        {
            //entity.Slug = ServiceHelpers.CreateUrl(entity.Name.Trim());
            // url slug generator
            entity.Slug = ServiceHelpers.GenerateSlug(entity.Name, GetBySlugLike(ServiceHelpers.CreateUrl(entity.Name)), null);
            return _context.Recruitment.Add(entity);
        }
        public IList<Recruitment> GetBySlugLike(string slug)
        {
            return _context.Recruitment
                .Where(x => x.Slug.Contains(slug))
                .ToList();
        }
        public PagedList<Recruitment> GetAllPageRecruitment(string search, bool isPending, int pageIndex, int pageSize)
        {
            if (!string.IsNullOrEmpty(search))
            {
                search = ServiceHelpers.CreateUrl(search.Trim());
            }
            var rs = _context.Recruitment.Where(x=> (string.IsNullOrEmpty(search) || x.Slug.Contains(search)) && x.IsPending == false )
                .OrderByDescending(x=>x.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new PagedList<Recruitment>(rs, pageIndex, 10, rs.Count);
        }
        public PagedList<Recruitment> GetAdminAllPageRecruitment(string search, bool isPending, int pageIndex, int pageSize)
        {
            if (!string.IsNullOrEmpty(search))
            {
                search = ServiceHelpers.CreateUrl(search.Trim());
            }
            var total = _context.Recruitment.Where(x => (string.IsNullOrEmpty(search) || x.Slug.Contains(search)) && x.IsPending == isPending).AsNoTracking().Count();
            
            var rs = _context.Recruitment.Where(x => (string.IsNullOrEmpty(search) || x.Slug.Contains(search)) && x.IsPending == isPending)
                .OrderByDescending(x => x.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new PagedList<Recruitment>(rs, pageIndex, 10, total);
        }
        public void Update(Recruitment entity)
        {
            entity.Slug = ServiceHelpers.CreateUrl(entity.Name.Trim());
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        #endregion


        #region CV

        public ApplyCV GetApplyCV(Guid id)
        {
            return _context.ApplyCV.Include(x=>x.Recruitment).FirstOrDefault(x => x.Id == id);
        }

        public ApplyCV AddApplyCV(ApplyCV entity)
        {
            entity.Slug = ServiceHelpers.CreateUrl(entity.FullName.Trim());
            return _context.ApplyCV.Add(entity);
        }

        public PagedList<ApplyCV> GetAllPageApplyCV(string recruitmentId, string search, bool isDelete, int pageIndex, int pageSize)
        {
            if (!string.IsNullOrEmpty(search))
            {
                search = ServiceHelpers.CreateUrl(search.Trim());
            }
          
            if (string.IsNullOrEmpty(recruitmentId))
            {
                var total = _context.ApplyCV.Count(x => string.IsNullOrEmpty(search) || x.Slug.Contains(search) && x.IsDelete.Equals(isDelete));

                var rs = _context.ApplyCV.Where(x => string.IsNullOrEmpty(search) || x.Slug.Contains(search) && x.IsDelete.Equals(isDelete))
                    .Include(x=>x.Recruitment)
                    .OrderByDescending(x => x.DateCreated)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return new PagedList<ApplyCV>(rs, pageIndex, 10, total);
            }
            else
            {
                var total = _context.ApplyCV.Count(x => (string.IsNullOrEmpty(search) || x.Slug.Contains(search)) && x.Recruitment.Id.ToString().Equals(recruitmentId) && x.IsDelete.Equals(isDelete));

                var rs = _context.ApplyCV.Where(x => (string.IsNullOrEmpty(search) || x.Slug.Contains(search)) && x.Recruitment.Id.ToString().Equals(recruitmentId) && x.IsDelete.Equals(isDelete))
                    .Include(x => x.Recruitment)
                    .OrderByDescending(x => x.DateCreated)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return new PagedList<ApplyCV>(rs, pageIndex, 10, total);
            }
        }

        public void UpdateApplyCV(ApplyCV entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public List<ApplyCV> GetAllApplyCVByReId(string recruitmentId, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                search = ServiceHelpers.CreateUrl(search.Trim());
            }
            
                return _context.ApplyCV.Where(x => (string.IsNullOrEmpty(search) || x.Slug.Contains(search)) && x.Recruitment.Id.ToString().Equals(recruitmentId))
                    .Include(x => x.Recruitment)
                    .OrderByDescending(x => x.DateCreated)
                    .ToList();
            }

        #endregion
    }
}
