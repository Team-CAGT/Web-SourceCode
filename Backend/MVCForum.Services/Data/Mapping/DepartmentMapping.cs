using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using MVCForum.Domain.DomainModel;

namespace MVCForum.Services.Data.Mapping
{
    public class DepartmentMapping : EntityTypeConfiguration<Department>
    {
        public DepartmentMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(200);
            Property(x => x.Slug).IsRequired().HasMaxLength(200).HasColumnAnnotation("Index",
                new IndexAnnotation(new IndexAttribute("IX_Department_Slug", 1) {IsUnique = true}));
            Property(x => x.Description).IsOptional();
            Property(x => x.OrderNumber).IsOptional();

            HasOptional(x => x.ParentDepartment)
                .WithMany()
                .Map(x => x.MapKey("Department_Id"));
            // Ignores

            Ignore(x => x.Level);
        }
    }
}
