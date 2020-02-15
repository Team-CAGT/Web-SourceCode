using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Services.Data.Mapping
{
    public class MedicalPackageMapping : EntityTypeConfiguration<MedicalPackage>
    {
        public MedicalPackageMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(200);
            Property(x => x.Slug).IsRequired().HasMaxLength(200).HasColumnAnnotation("Index",
                new IndexAnnotation(new IndexAttribute("IX_MedicalPackage_Slug", 1) { IsUnique = true }));
            Property(x => x.OrderNumber).IsOptional();

            HasOptional(x => x.Parent)
                .WithMany()
                .Map(x => x.MapKey("Parent_Id"));
            // Ignores

            Ignore(x => x.Level);
        }
    }
}
