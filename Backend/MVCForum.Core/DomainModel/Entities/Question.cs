using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCForum.Utilities;

namespace MVCForum.Domain.DomainModel.Entities
{
    
    public partial class Question : Entity
    {
        public Question()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ContentQuestion { get; set; }
        public string UrlFile { get; set; }
        public string ReplyContent { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Chuyenmuc { get; set; }
        public string Status { get; set; }

    }
}
