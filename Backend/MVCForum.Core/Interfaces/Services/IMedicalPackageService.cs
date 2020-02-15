using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVCForum.Domain.DomainModel;
using MVCForum.Domain.DomainModel.Entities;
using MVCForum.Domain.DomainModel.General;

namespace MVCForum.Domain.Interfaces.Services
{
    public interface IMedicalPackageService
    {
        List<SelectListItem> GetBaseSelectListDepartments(List<MedicalPackage> allowedMedicalPackages);
        List<MedicalPackage> GetListMedicalPackage();
        List<MedicalPackage> GetAllMedicalPackages();
        IEnumerable<MedicalPackage> GetAllSubMedicalPackages(Guid parentId);
        PagedList<MedicalPackage> GetAllMedicalPackage(int pageIndex, int pageSize);
        ParentWithSubMedicalPackages GetParentBySlugWithSubMedicalPackages(string slug);
        MedicalPackage GetMedicalPackageById(Guid id);
        void UpdateSlugFromName(MedicalPackage entity);
        MedicalPackage SanitizeMedicalPackage(MedicalPackage entity);
        MedicalPackage InsertMedicalPackage(MedicalPackage entity);
        IList<MedicalPackage> GetMedicalPackageBySlugLike(string slug);
        void DeleteMedicalPackage(MedicalPackage entity);
        List<MedicalPackage> GetAllSubMedicalPackageBySlug(string slug, out string medicalPackage);
    }
}
