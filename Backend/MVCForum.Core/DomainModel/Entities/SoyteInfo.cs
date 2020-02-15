using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCForum.Utilities;

namespace MVCForum.Domain.DomainModel.Entities
{
    
    public partial class SoyteInfo : Entity
    {
        public SoyteInfo()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public string Url { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime NgayApdung { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }


    }
}
