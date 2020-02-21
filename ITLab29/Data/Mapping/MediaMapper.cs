using ITLab29.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLab29.Data.Mapping
{
    public class MediaMapper : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.HasKey(t => t.MediaId);
            builder.Property(t => t.MediaId).ValueGeneratedOnAdd();
            builder.Property(t => t.Path).IsRequired();
        }
    }
}
