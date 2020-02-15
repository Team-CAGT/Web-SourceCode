using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Services.Data.Mapping
{
    public class RecruitmentMapping : EntityTypeConfiguration<Recruitment>
    {
        public RecruitmentMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(450);
            Property(x => x.DateCreated).IsRequired();
            Property(x => x.Slug).IsRequired().HasMaxLength(450)
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("IX_Recruitment_Slug", 1) { IsUnique = true }));
        }
    }
}
