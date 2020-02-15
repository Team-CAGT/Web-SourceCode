using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Services.Data.Mapping
{
    public class ApplyCVMapping : EntityTypeConfiguration<ApplyCV>
    {
        public ApplyCVMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.FullName).IsRequired().HasMaxLength(128);
            Property(x => x.Slug).IsRequired().HasMaxLength(450);
            Property(x => x.DateCreated).IsRequired();
            Property(x => x.Address).IsRequired().HasMaxLength(500);
            Property(x => x.Email).IsRequired().HasMaxLength(256);
            Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);
            Property(x => x.Status).IsRequired().HasMaxLength(50);
            Property(x => x.DateCreated).IsRequired();
            Property(x => x.IsDelete).IsOptional();
            HasOptional(t => t.Recruitment).WithMany(t => t.ListCv).Map(m => m.MapKey("Recruitment_Id")).WillCascadeOnDelete(false);
        }
    }
}
