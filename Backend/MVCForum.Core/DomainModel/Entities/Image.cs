using System;
using MVCForum.Utilities;

namespace MVCForum.Domain.DomainModel.Entities
{
    public partial class Image : Entity
    {
        public Image(){
            Id = GuidComb.GenerateComb();
        }
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public string ImageUrl { get; set; }
    }
}
