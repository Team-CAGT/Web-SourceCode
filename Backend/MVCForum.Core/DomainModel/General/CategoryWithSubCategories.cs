﻿using System.Collections.Generic;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Domain.DomainModel.General
{
    /// <summary>
    /// This is a special domain model and not mapped to the database
    /// its use to make an efficient call to get a category with subcategories and only
    /// used for display purposes. This is due to the database structure and EF getting in a 
    /// mess when we try to map sub categories by doing a join on itself (Category table)
    /// </summary>
    public partial class CategoryWithSubCategories
    {
        public Category Category { get; set; }
        public IEnumerable<Category> SubCategories { get; set; } 
    }
    public partial class DepartmentWithSubDepartments
    {
        public Department Department { get; set; }
        public IEnumerable<Department> SubDepartments { get; set; }
    }
    public partial class ParentWithSubMedicalPackages
    {
        public MedicalPackage MedicalPackage { get; set; }
        public IEnumerable<MedicalPackage> SubMedicalPackages { get; set; }
    }
}
