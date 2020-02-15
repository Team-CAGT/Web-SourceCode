using MVCForum.Domain.DomainModel.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MVCForum.Services.Data.Mapping
{
    public class ImageMapping : EntityTypeConfiguration<Image>
    {
        public ImageMapping()
            {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            Property(x => x.Title).IsRequired().HasMaxLength(50);
            Property(x => x.DateCreated).IsRequired();
            Property(x => x.ImageUrl).HasMaxLength(500);
    }
    }
}
