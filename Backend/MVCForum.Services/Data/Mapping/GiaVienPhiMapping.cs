using System.Data.Entity.ModelConfiguration;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Services.Data.Mapping
{
    public class GiaVienPhiMapping : EntityTypeConfiguration<GiaVienPhi>
    {
        public GiaVienPhiMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.FileName).IsRequired().HasMaxLength(50);
            Property(x => x.DateCreated).IsRequired();
            Property(x => x.FileUrl).IsRequired().HasMaxLength(500);
        }
    }
    public class GiaDichVuMapping : EntityTypeConfiguration<GiaDichVu>
    {
        public GiaDichVuMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.FileName).IsRequired().HasMaxLength(50);
            Property(x => x.DateCreated).IsRequired();
            Property(x => x.FileUrl).IsRequired().HasMaxLength(500);
        }
    }
    public class GiaVatTuThuocMapping : EntityTypeConfiguration<GiaVatTuThuoc>
    {
        public GiaVatTuThuocMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.FileName).IsRequired().HasMaxLength(50);
            Property(x => x.DateCreated).IsRequired();
            Property(x => x.FileUrl).IsRequired().HasMaxLength(500);
        }
    }
}
