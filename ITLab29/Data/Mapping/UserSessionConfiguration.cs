using ITLab29.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data.Mapping {
    public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession> {
        public void Configure(EntityTypeBuilder<UserSession> builder) {
            builder.ToTable("UserSession");
            builder.Property(t => t.UserId).IsRequired();
            builder.Property(t => t.SessionId).IsRequired();
            builder.HasOne(t => t.User).WithMany();
            builder.HasOne(t => t.Session).WithMany();
        }
    }
}
