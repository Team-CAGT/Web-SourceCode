using MVCForum.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCForum.Domain.DomainModel.Entities;
using System.Web.Mvc;
using MVCForum.Domain.DomainModel.General;
using MVCForum.Domain.Model;

namespace MVCForum.Domain.Interfaces.Services
{
    public interface IDoctorService
    {
        #region Doctor

        /// <summary>
        /// get all Doctor enabled
        /// </summary>
        /// <returns></returns>
        IList<Doctor> GetallDoctor(string search, string specicality);

        /// <summary>
        /// search Doctor with name
        /// </summary>
        /// <returns></returns>
        List<DataDoctorModel> SearchDoctor(string search);

        /// <summary>
        /// Delete a Doctor
        /// </summary>
        /// <param name="doctor"></param>
        void Delete(Doctor doctor);
        /// <summary>
        /// Get a banner by name
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        Doctor GetDoctor(string slug);

        Doctor Get(Guid id);

        /// <summary>
        /// All Doctor
        /// </summary>
        /// <returns></returns>
        IEnumerable<Doctor> GetAll();

        Doctor Add(Doctor doctor);
        void Update(Doctor updatedoctor);
        PagedList<Doctor> GetDoctorSearch(string slug, string slugManage,string doctor, int pageIndex, int pageSize);
        #endregion

        #region Department
        List<SelectListItem> GetBaseSelectListDepartments(List<Department> allowedDepartments);
        List<SelectListItem> GetBaseSelectListDoctors(List<Doctor> doctors);
        List<Department> GetListDepartment();
        List<Department> GetAllDepartments();
        IEnumerable<Department> GetAllSubDepartmetns(Guid parentId);
        PagedList<Department> GetAllDepartment(int pageIndex, int pageSize);
        PagedList<Department> SearchDepartment(string search, int pageIndex, int pageSize);
        DepartmentWithSubDepartments GetBySlugWithSubDepartments(string slug);
        List<Department> GetListDepartmentBySlug(string search);
        Department GetDepartmentById(Guid id);
        void UpdateSlugFromName(Department entity);
        Department SanitizeDepartment(Department entity);
        Department InsertDepartment(Department entity);
        IList<Department> GetDepartmentBySlugLike(string slug);
        void DeleteDepartment(Department department);
        List<Department> GetAllSubDepartmentBySlug(string slug,out string department);
        List<Department> GetAllDepartmentWithDoctor();
        #endregion
    }
}
