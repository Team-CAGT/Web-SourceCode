using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCForum.Domain.DomainModel.Entities
{
    public class MedicalPackage : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string FileUrl { get; set; }
        public float OrderNumber { get; set; }
        public string Path { get; set; }
        public int Level { get; set; }
        public virtual MedicalPackage Parent { get; set; }
    }
}
