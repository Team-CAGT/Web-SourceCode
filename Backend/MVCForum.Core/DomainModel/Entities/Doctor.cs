using System;
using System.Collections.Generic;
using MVCForum.Utilities;

namespace MVCForum.Domain.DomainModel
{
    public partial class Doctor : Entity
    {
        public Doctor()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Avatar { get; set; }
        public string Specicality { get; set; }
        public string Department { get; set; }
        public Guid DepartmentId { get; set; }
        public string Filter { get; set; }
        public string DescriptionHistory { get; set; }
        public string BoardCertifications { get; set; }
        public string TimeWorking { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string PositionName { get; set; }
        public bool IsSearch { get; set; }
        public virtual Department DepartmentManage { get; set; }
    }
}
