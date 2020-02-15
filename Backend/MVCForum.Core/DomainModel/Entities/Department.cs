using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCForum.Domain.DomainModel
{
    public class Department : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Slogan { get; set; }
        public string Path { get; set; }
        public virtual Department ParentDepartment { get; set; }
        public virtual List<Doctor> ListDoctor { get; set; }
        public int Level { get; set; }
        public int OrderNumber { get; set; }
    }
}
