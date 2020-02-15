using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCForum.Utilities;

namespace MVCForum.Domain.DomainModel.Entities
{
    public partial class Education : Entity
    {
        public Education()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public bool IsPending { get; set; }
        public string UserDelete { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
