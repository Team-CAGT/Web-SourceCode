using System;
using System.Collections.Generic;
using MVCForum.Utilities;

namespace MVCForum.Domain.DomainModel.Entities
{
    public partial class Recruitment : Entity
    {
        public Recruitment()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int FromSalary { get; set; }
        public int ToSalary { get; set; }
        public string ImageUrl { get; set; }
        public string FileUrl { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string Requirement { get; set; }
        public string Benefit { get; set; }
        public bool IsPending { get; set; }
        public bool IsActive { get; set; }
        public string ListPosition { get; set; }
        public string UserDelete { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual List<ApplyCV> ListCv { get; set; }
    }
}
