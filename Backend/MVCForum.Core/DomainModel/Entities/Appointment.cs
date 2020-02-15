using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCForum.Utilities;

namespace MVCForum.Domain.DomainModel.Entities
{
    
    public partial class Appointment : Entity
    {
        public Appointment()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsCompany { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }

    }
}
