using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCForum.Utilities;

namespace MVCForum.Domain.DomainModel.Entities
{
    
    public partial class ContactForm : Entity
    {
        public ContactForm()
        {
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }

    }
}
