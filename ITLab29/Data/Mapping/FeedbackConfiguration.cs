using ITLab29.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data.Mapping
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasOne(t => t.User).WithMany();
            builder.HasKey(t => t.FeedbackId);
        }
    }
}
