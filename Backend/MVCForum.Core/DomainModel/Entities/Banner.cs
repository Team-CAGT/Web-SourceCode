using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCForum.Utilities;

namespace MVCForum.Domain.DomainModel.Entities
{
    
    public partial class Banner : Entity
    {
        public Banner()
        {
            Id = GuidComb.GenerateComb();
         

        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string LinkWeb { get; set; }
        public int Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }

    }
}
