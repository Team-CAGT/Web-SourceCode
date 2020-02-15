using System.Data.Entity.ModelConfiguration;

using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Services.Data.Mapping
{
    public class BannerMapping : EntityTypeConfiguration<Banner>
    {
        public BannerMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(200);
          
            Property(x => x.Status).IsRequired().HasMaxLength(50);
            Property(x => x.Title).HasMaxLength(200);
        }
    }
    public class QuestionMapping : EntityTypeConfiguration<Question>
    {
        public QuestionMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Title).HasMaxLength(200);
            Property(x => x.Name).IsRequired().HasMaxLength(200);
            Property(x=>x.Age).IsRequired();
            Property(x => x.Phone).HasMaxLength(50);
            Property(x => x.Address).HasMaxLength(50);
            Property(x => x.Email).HasMaxLength(50);
            Property(x => x.Status).IsRequired().HasMaxLength(50);
        }
    }
    public class ContactFormMapping : EntityTypeConfiguration<ContactForm>
    {
        public ContactFormMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(200);
            Property(x => x.Phone).IsRequired().HasMaxLength(50);
            Property(x => x.Email).IsRequired().HasMaxLength(50);
            Property(x => x.Status).IsRequired().HasMaxLength(50);
        }
    }
    public class AppointmentMapping : EntityTypeConfiguration<Appointment>
    {
        public AppointmentMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(200);
            Property(x => x.Phone).IsRequired().HasMaxLength(50);
            Property(x => x.Email).IsRequired().HasMaxLength(50);
            Property(x => x.Status).IsRequired().HasMaxLength(50);
            Property(x => x.Type).IsRequired().HasMaxLength(300);
            Property(x => x.CompanyName).HasMaxLength(300);
        }
    }
    public class SoyteInfoMapping : EntityTypeConfiguration<SoyteInfo>
    {
        public SoyteInfoMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Title).HasMaxLength(500);
            Property(x => x.CreateDate).IsRequired();
            Property(x => x.NgayApdung).IsRequired();
            Property(x => x.Url).HasMaxLength(500);
            Property(x => x.Status).IsRequired().HasMaxLength(50);
        }
    }
}
