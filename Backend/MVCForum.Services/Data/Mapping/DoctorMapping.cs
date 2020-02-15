using System.Data.Entity.ModelConfiguration;
using MVCForum.Domain.DomainModel;

namespace MVCForum.Services.Data.Mapping
{
    public class DoctorMapping : EntityTypeConfiguration<Doctor>
    {
        public DoctorMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(200);
            Property(x => x.Slug).IsRequired().HasMaxLength(200);
            Property(x => x.Specicality).IsRequired().HasMaxLength(200);
            Property(x => x.Status).IsRequired().HasMaxLength(50);
            //Property(x => x.IsSearch).IsRequired();
            HasOptional(t => t.DepartmentManage).WithMany(t => t.ListDoctor).Map(m => m.MapKey("DepartmentManage_Id")).WillCascadeOnDelete(false);
        }
    }
}
