using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCForum.Utilities;

namespace MVCForum.Domain.DomainModel.Entities
{
    public partial class ProfileUser : Entity
    {
        public ProfileUser()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public string PositionName { get; set; }
        public string Note { get; set; }
        public virtual MembershipUser User { get; set; }
    }
}
