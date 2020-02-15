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
    public class EducationService : IEducationService
    {

        private readonly MVCForumContext _context;
        public EducationService(IMVCForumContext context)
        {
            _context = context as MVCForumContext;
        }

        #region Education

        public Education Get(Guid id)
        {
            return _context.Education.FirstOrDefault(x => x.Id == id);
        }
        public List<Education> GetListEducation()
        {
            return _context.Education.OrderByDescending(x => x.Name).ToList();
        }
        public Education Add(Education entity)
        {
            entity.Slug = ServiceHelpers.CreateUrl(entity.Name.Trim());
            return _context.Education.Add(entity);
        }

        public PagedList<Education> GetAllPageEducation(string search, bool isPending, int pageIndex, int pageSize)
        {
            if (!string.IsNullOrEmpty(search))
            {
                search = ServiceHelpers.CreateUrl(search.Trim());
            }
            var rs = _context.Education.Where(x=> (string.IsNullOrEmpty(search) || x.Slug.Contains(search)) && x.IsPending == isPending)
                .OrderByDescending(x=>x.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new PagedList<Education>(rs, pageIndex, 10, rs.Count);
        }
        public void Update(Education entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        #endregion
    }
}
