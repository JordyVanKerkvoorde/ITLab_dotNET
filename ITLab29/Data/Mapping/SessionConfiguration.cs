using ITLab29.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data.Mapping
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Session");
            builder.HasOne(t => t.Responsible).WithOne();
            builder.HasOne(t => t.Location).WithOne();
            builder.HasMany(t => t.Media).WithOne();
            builder.HasMany(t => t.Guests).WithOne();
        }
    }
}
