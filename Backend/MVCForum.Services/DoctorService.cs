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
using MVCForum.Domain.DomainModel.General;
using MVCForum.Domain.Model;

namespace MVCForum.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly MVCForumContext _context;
        public DoctorService(IMVCForumContext context)
        {
            _context = context as MVCForumContext;
        }
        #region Doctor
        public Doctor Add(Doctor doctor)
        {

            doctor.Status = "A";
            doctor.Slug = ServiceHelpers.GenerateSlug(doctor.Name, GetDoctorBySlugLike(ServiceHelpers.CreateUrl(doctor.Name)), null);
            return _context.Doctor.Add(doctor);
        }

        public void Delete(Doctor doctor)
        {
            doctor.Status = "D";
            _context.Entry(doctor).State = System.Data.Entity.EntityState.Modified;
        }

        public Doctor Get(Guid id)
        {
            return _context.Doctor.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Doctor> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Doctor> GetallDoctor(string search, string specicality)
        {
            var txtSearh = StringUtils.RemoveSign4VietnameseString(search.Trim()).ToUpper().Replace(" ", "-");
            var list = _context.Doctor.Where(x => x.Status == "A"
                                                  && (string.IsNullOrEmpty(search) || x.Slug.ToUpper().Contains(txtSearh))
                                                  && (string.IsNullOrEmpty(specicality) || x.Department.Equals(specicality))).ToList();
            return list.Where(x => x.Status.Equals("A")).ToList();
        }

        public List<DataDoctorModel> SearchDoctor(string search)
        {
            var txtSearh = StringUtils.RemoveSign4VietnameseString(search.Trim()).ToUpper().Replace(" ", "-");
            return _context.Doctor.Where(x => x.Status == "A"
                                                  && (string.IsNullOrEmpty(search) || x.Slug.ToUpper().Contains(txtSearh))
                                                  && x.PositionName != null).ToList().Select(x => new DataDoctorModel(x)).ToList();
            // return list.Where(x => x.Status.Equals("A")).ToList();
        }

        public Doctor GetDoctor(string slug)
        {
            return _context.Doctor.Where(x => x.Slug == slug && x.Status == "A").Include(x => x.DepartmentManage.ParentDepartment).First();
        }

        public void Update(Doctor updatedoctor)
        {
            if (string.IsNullOrEmpty(updatedoctor.Slug))
            {
                updatedoctor.Slug = ServiceHelpers.GenerateSlug(updatedoctor.Name, GetDoctorBySlugLike(ServiceHelpers.CreateUrl(updatedoctor.Name)), null);
            }
            _context.Entry(updatedoctor).State = System.Data.Entity.EntityState.Modified;
        }
        public IList<Doctor> GetDoctorBySlugLike(string slug)
        {
            return _context.Doctor

                .Where(x => x.Slug.Contains(slug))
                .ToList();
        }

        public PagedList<Doctor> GetDoctorSearch(string slug, string slugManage, string doctor, int pageIndex, int pageSize)
        {
            if (!string.IsNullOrEmpty(doctor))
            {
                doctor = ServiceHelpers.CreateUrl(doctor.Trim());
            }
            
            var subDepartment = _context.Department.FirstOrDefault(x => x.Slug == slug);
            var department = _context.Department.FirstOrDefault(x => x.Slug == slugManage);

            var query = _context.Doctor.AsNoTracking().Where(x => (string.IsNullOrEmpty(slug) || x.DepartmentManage.Slug == slug) && (string.IsNullOrEmpty(doctor) || x.Slug.Contains(doctor))).OrderByDescending(x => x.Name);

            if (subDepartment != null && department != null)
            {
                query = _context.Doctor.AsNoTracking().Where(x => (x.DepartmentManage.Id == subDepartment.Id) && (string.IsNullOrEmpty(doctor) || x.Slug.Contains(doctor))).OrderByDescending(x => x.Name);
            }

            if (subDepartment == null && department != null)
            {
                var childDepartment = _context.Department.AsNoTracking().AsEnumerable().Where(x => x.ParentDepartment != null && x.ParentDepartment.Id == department.Id).ToList();
                query = childDepartment.Count == 0 ? _context.Doctor.AsNoTracking().Where(x => (x.DepartmentManage.Id == department.Id) && (string.IsNullOrEmpty(doctor) || x.Slug.Contains(doctor))).OrderByDescending(x => x.Name) : _context.Doctor.AsNoTracking().Where(x => (x.DepartmentManage.ParentDepartment.Id == department.Id) && (string.IsNullOrEmpty(doctor) || x.Slug.Contains(doctor))).OrderByDescending(x => x.Name);
            }
            

            var rs = query
                .OrderBy(x=>x.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Where(x=>x.Status.Equals("A") && x.IsSearch)
                .ToList();
            return new PagedList<Doctor>(rs, pageIndex, 10, query.Count());
        }
        #endregion

        #region Department

        public List<SelectListItem> GetBaseSelectListDepartments(List<Department> allowedDepartments)
        {
            var departments = new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            foreach (var department in allowedDepartments)
            {
                var departmentName = string.Concat(LevelDashes(department.Level), department.Level > 1 ? " " : "", department.Name);
                departments.Add(new SelectListItem { Text = departmentName, Value = department.Id.ToString() });
            }
            return departments;
        }
        public List<SelectListItem> GetBaseSelectListDoctors(List<Doctor> doctors)
        {
            var list = new List<SelectListItem> { new SelectListItem { Text = "", Value = "" } };
            foreach (var doctor in doctors)
            {
                list.Add(new SelectListItem { Text = doctor.Name, Value = doctor.Id.ToString() });
            }
            return list;
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

        public List<Department> GetListDepartment()
        {
            return _context.Department.OrderByDescending(x => x.Name).ToList();
        }

        public PagedList<Department> GetAllDepartment(int pageIndex, int pageSize)
        {
            var query = _context.Department;
            var results = query
                .OrderByDescending(x => x.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<Department>(results, pageIndex, pageSize, query.Count());
        }
        /// <summary>
        /// Return all departments
        /// </summary>
        /// <returns></returns>
        public List<Department> GetAllDepartments()
        {
            // Cache per request for speed - As this is hit constantly for permissions
            if (HttpContext.Current != null)
            {
                const string key = "get-all-departments";
                if (!HttpContext.Current.Items.Contains(key))
                {
                    // These are now in order
                    var orderedDepartments = new List<Department>();

                    var allDepartments = _context.Department
                        .Include(x => x.ParentDepartment)
                        .AsNoTracking()
                        .OrderBy(x => x.Name)
                        .ToList();
                    foreach (var parentDepartment in allDepartments.Where(x => x.ParentDepartment == null).OrderBy(x => x.Name))
                    {
                        // Add the main department
                        parentDepartment.Level = 1;
                        orderedDepartments.Add(parentDepartment);

                        // Add subdepartments under this
                        orderedDepartments.AddRange(GetSubDepartments(parentDepartment, allDepartments));
                    }

                    HttpContext.Current.Items.Add(key, orderedDepartments);
                }
                return (List<Department>)HttpContext.Current.Items[key];
            }
            return _context.Department
                .Include(x => x.ParentDepartment)
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToList();
        }

        public List<Department> GetSubDepartments(Department department, List<Department> allDepartments, int level = 2)
        {
            var departmentsToReturn = new List<Department>();
            var departments = allDepartments.Where(x => x.ParentDepartment != null && x.ParentDepartment.Id == department.Id).OrderBy(x => x.Name);
            foreach (var de in departments)
            {
                de.Level = level;
                departmentsToReturn.Add(de);
                departmentsToReturn.AddRange(GetSubDepartments(de, allDepartments, level + 1));
            }

            return departmentsToReturn;
        }
        public PagedList<Department> SearchDepartment(string search, int pageIndex, int pageSize)
        {
            var query = _context.Department;
            var txtSearh = StringUtils.RemoveSign4VietnameseString(search.Trim()).ToUpper().Replace(" ", "-");
            var results = query
                .Where(x => (string.IsNullOrEmpty(search) || x.Slug.ToUpper().Contains(txtSearh)))
                .OrderByDescending(x => x.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedList<Department>(results, pageIndex, pageSize, query.Count());
        }

        public List<Department> GetListDepartmentBySlug(string search)
        {
            var convertToSlug = ServiceHelpers.CreateUrl(search);
            return _context.Department.Where(x => x.Slug.Contains(convertToSlug)).ToList();
        }

        public Department GetDepartmentById(Guid id)
        {
            return _context.Department.Include(x => x.ParentDepartment).Include(x => x.ListDoctor).FirstOrDefault(x => x.Id == id);
        }

        public Department InsertDepartment(Department entity)
        {
            // Sanitize
            entity = SanitizeDepartment(entity);

            // url slug generator
            entity.Slug = ServiceHelpers.GenerateSlug(entity.Name, GetDepartmentBySlugLike(ServiceHelpers.CreateUrl(entity.Name)), null);

            // Add the department
            return _context.Department.Add(entity);
        }

        public Department SanitizeDepartment(Department department)
        {
            // Sanitize any strings in a department
            department.Description = StringUtils.GetSafeHtml(department.Description);
            department.Name = HttpUtility.HtmlDecode(StringUtils.SafePlainText(department.Name));
            return department;
        }
        /// <summary>
        /// Keep slug in line with name
        /// </summary>
        /// <param name="category"></param>
        public void UpdateSlugFromName(Department department)
        {
            // Sanitize
            department = SanitizeDepartment(department);

            var updateSlug = true;

            // Check if slug has changed as this could be an update
            if (!string.IsNullOrEmpty(department.Slug))
            {
                var departmentBySlug = GetBySlugWithSubDepartments(department.Slug);
                if (departmentBySlug.Department.Id == department.Id)
                {
                    updateSlug = false;
                }
            }

            if (updateSlug)
            {
                department.Slug = ServiceHelpers.GenerateSlug(department.Name, GetDepartmentBySlugLike(department.Slug), department.Slug);
            }
        }

        /// <summary>
        /// Return model with Sub categories
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public DepartmentWithSubDepartments GetBySlugWithSubDepartments(string slug)
        {
            slug = StringUtils.SafePlainText(slug);
            var department = (from de in _context.Department
                              where de.Slug == slug
                              select new DepartmentWithSubDepartments
                              {
                                  Department = de,
                                  SubDepartments = (from des in _context.Department
                                                    where des.ParentDepartment.Id == de.Id
                                                    select des)
                              }).FirstOrDefault();

            return department;
        }

        public IList<Department> GetDepartmentBySlugLike(string slug)
        {
            return _context.Department
                    .Where(x => x.Slug.Contains(slug))
                    .ToList();
        }

        public IEnumerable<Department> GetAllSubDepartmetns(Guid parentId)
        {
            return _context.Department
                .Where(x => x.ParentDepartment.Id == parentId)
                .OrderBy(x => x.Name)
                .ToList();
        }

        /// <summary>
        /// Delete a department
        /// </summary>
        /// <param name="department"></param>
        public void DeleteDepartment(Department department)
        {
            _context.Department.Remove(department);
        }

        public List<Department> GetAllSubDepartmentBySlug(string slug, out string department)
        {
            var parentDepartment = _context.Department.FirstOrDefault(x => x.Slug == slug);
            department = parentDepartment.Name;
            return _context.Department.AsNoTracking().Where(x => x.ParentDepartment.Id == parentDepartment.Id).Include(x => x.ListDoctor).ToList();
        }
        public List<Department> GetAllDepartmentWithDoctor()
        {
            return _context.Department.AsNoTracking().Include(x => x.ListDoctor).ToList();
        }


        #endregion
    }
}
