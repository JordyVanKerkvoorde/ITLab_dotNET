using ITLab29.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data.Mapping
{

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(t => t.Email).IsRequired();
            builder.Property(t => t.UserId).IsRequired();
            builder.Property(t => t.FirstName).IsRequired();
            builder.Property(t => t.UserType).IsRequired();
            builder.Property(t => t.UserStatus).IsRequired();
            builder.HasOne(t => t.Avatar).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
