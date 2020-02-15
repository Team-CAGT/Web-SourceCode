using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using MVCForum.Domain.Interfaces;
using MVCForum.Domain.Interfaces.Services;
using MVCForum.Services.Data.Context;
using MVCForum.Domain.DomainModel;
using MVCForum.Utilities;
using System.Web.Mvc;
using System.Text;
using System.Web;
using MVCForum.Domain.DomainModel.Entities;
using MVCForum.Domain.DomainModel.General;

namespace MVCForum.Services
{
    public class MedicalPackageService : IMedicalPackageService
    {
        private readonly MVCForumContext _context;
        public MedicalPackageService(IMVCForumContext context)
        {
            _context = context as MVCForumContext;
        }

        public List<SelectListItem> GetBaseSelectListDepartments(List<MedicalPackage> allowedMedicalPackages)
        {
            var medicalPackages = new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            foreach (var item in allowedMedicalPackages)
            {
                var Name = string.Concat(LevelDashes(item.Level), item.Level > 1 ? " " : "", item.Name);
                medicalPackages.Add(new SelectListItem { Text = Name, Value = item.Id.ToString() });
            }
            return medicalPackages;
        }
        private static string LevelDashes(int level)
        {
            if (level > 1)
            {
                var sb = new StringBuilder();
                for (var i = 0; i < level - 1; i++)
                {
                    sb.Append("-");
                }
                return sb.ToString();
            }
            return string.Empty;
        }
        public List<MedicalPackage> GetListMedicalPackage()
        {
            return _context.MedicalPackage.OrderByDescending(x => x.Name).ToList();
        }

        public List<MedicalPackage> GetAllMedicalPackages()
        {
            // Cache per request for speed - As this is hit constantly for permissions
            if (HttpContext.Current != null)
            {
                const string key = "get-all-medicalPackage";
                if (!HttpContext.Current.Items.Contains(key))
                {
                    // These are now in order
                    var orderedMedicalPackages = new List<MedicalPackage>();

                    var allMedicalPackages = _context.MedicalPackage
                        .Include(x => x.Parent)
                        .AsNoTracking()
                        .OrderBy(x => x.OrderNumber)
                        .ToList();
                    foreach (var parent in allMedicalPackages.Where(x => x.Parent == null).OrderBy(x => x.OrderNumber))
                    {
                        // Add the main department
                        parent.Level = 1;
                        orderedMedicalPackages.Add(parent);

                        // Add subdepartments under this
                        orderedMedicalPackages.AddRange(GetSubMedicalPackages(parent, allMedicalPackages));
                    }

                    HttpContext.Current.Items.Add(key, orderedMedicalPackages);
                }
                return (List<MedicalPackage>)HttpContext.Current.Items[key];
            }
            return _context.MedicalPackage
                .Include(x => x.Parent)
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToList();
        }
        public List<MedicalPackage> GetSubMedicalPackages(MedicalPackage medicalPackage, List<MedicalPackage> allMedicalPackages, int level = 2)
        {
            var medicalPackagesToReturn = new List<MedicalPackage>();
            var medicalPackages = allMedicalPackages.Where(x => x.Parent != null && x.Parent.Id == medicalPackage.Id).OrderBy(x => x.Name);
            foreach (var item in medicalPackages)
            {
                item.Level = level;
                medicalPackagesToReturn.Add(item);
                medicalPackagesToReturn.AddRange(GetSubMedicalPackages(item, allMedicalPackages, level + 1));
            }

            return medicalPackagesToReturn;
        }
        public IEnumerable<MedicalPackage> GetAllSubMedicalPackages(Guid parentId)
        {
            return _context.MedicalPackage
                .Where(x => x.Parent.Id == parentId)
                .OrderBy(x => x.Name)
                .ToList();
        }

        public PagedList<MedicalPackage> GetAllMedicalPackage(int pageIndex, int pageSize)
        {
            var query = _context.MedicalPackage;
            var results = query
                .OrderByDescending(x => x.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new PagedList<MedicalPackage>(results, pageIndex, pageSize, query.Count());
        }

        public ParentWithSubMedicalPackages GetParentBySlugWithSubMedicalPackages(string slug)
        {
            slug = StringUtils.SafePlainText(slug);
            var medicalPackage = (from item in _context.MedicalPackage
                                  where item.Slug == slug
                                  select new ParentWithSubMedicalPackages
                                  {
                                      MedicalPackage = item,
                                      SubMedicalPackages = (from medicalPackages in _context.MedicalPackage
                                                            where medicalPackages.Parent.Id == item.Id
                                                            select medicalPackages)
                                  }).FirstOrDefault();

            return medicalPackage;
        }

        public MedicalPackage GetMedicalPackageById(Guid id)
        {
            return _context.MedicalPackage.Include(x => x.Parent).FirstOrDefault(x => x.Id == id);
        }

        public void UpdateSlugFromName(MedicalPackage entity)
        {
            // Sanitize
            entity = SanitizeMedicalPackage(entity);

            var updateSlug = true;

            // Check if slug has changed as this could be an update
            if (!string.IsNullOrEmpty(entity.Slug))
            {
                var medicalPackageBySlug = GetParentBySlugWithSubMedicalPackages(entity.Slug);
                if (medicalPackageBySlug.MedicalPackage.Id == entity.Id)
                {
                    updateSlug = false;
                }
            }

            if (updateSlug)
            {
                entity.Slug = ServiceHelpers.GenerateSlug(entity.Name, GetMedicalPackageBySlugLike(entity.Slug), entity.Slug);
            }
        }
        public MedicalPackage SanitizeMedicalPackage(MedicalPackage entity)
        {
            entity.Name = HttpUtility.HtmlDecode(StringUtils.SafePlainText(entity.Name));
            return entity;
        }
        public MedicalPackage InsertMedicalPackage(MedicalPackage entity)
        {
            // Sanitize
            entity = SanitizeMedicalPackage(entity);

            // url slug generator
            entity.Slug = ServiceHelpers.GenerateSlug(entity.Name, GetMedicalPackageBySlugLike(ServiceHelpers.CreateUrl(entity.Name)), null);

            // Add the department
            return _context.MedicalPackage.Add(entity);
        }

        public IList<MedicalPackage> GetMedicalPackageBySlugLike(string slug)
        {
            return _context.MedicalPackage
                .Where(x => x.Slug.Contains(slug))
                .ToList();
        }

        public void DeleteMedicalPackage(MedicalPackage entity)
        {
            _context.MedicalPackage.Remove(entity);
        }

        public List<MedicalPackage> GetAllSubMedicalPackageBySlug(string slug, out string medicalPackage)
        {
            var parent = _context.MedicalPackage.Where(x=>x.Parent == null).FirstOrDefault(x => x.Slug == slug);
            medicalPackage = parent.Name;
            return _context.MedicalPackage.AsNoTracking().Where(x => x.Parent.Id == parent.Id).ToList();
        }
    }
}
