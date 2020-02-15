using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCForum.Domain.DomainModel.Entities;

namespace MVCForum.Services.Data.Mapping
{
    public class ProfileUserMapping : EntityTypeConfiguration<ProfileUser>
    {
        public ProfileUserMapping()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(128);
            Property(x => x.Slug).IsRequired().HasMaxLength(450);
            Property(x => x.Address).IsRequired();
            Property(x => x.Gender).IsRequired();
            Property(x => x.Qualification).IsRequired();
            Property(x => x.Experience).HasMaxLength(450);
            Property(x => x.Address).IsRequired();
            Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);
            Property(x => x.PositionName).HasMaxLength(500);
            Property(x => x.Note).HasMaxLength(500);
            HasRequired(x => x.User)
                .WithOptional().Map(m => m.MapKey("User_Id"));
        }
    }
}
