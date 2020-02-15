using System;
using MVCForum.Utilities;

namespace MVCForum.Domain.DomainModel.Entities
{
    public partial class ApplyCV : Entity
    {
        public ApplyCV()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Qualification { get; set; }
        public string Slug { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PositionName { get; set; }
        public string PhoneNumber { get; set; }
        public string FileUrl { get; set; }
        public string HinhAnhChungChi { get; set; }
        public string ReplyContent { get; set; }
        public string Status { get; set; }
        public bool IsDelete { get; set; }
        public string UserDelete { get; set; }
        public string UserReply { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual Recruitment Recruitment { get; set; }
    }
}
