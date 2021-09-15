using Bloomcoding.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloomcoding.Dal.EntityConfiguration
{
    public class GroupConfig : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .HasMany(x => x.Users)
                .WithMany(x => x.Groups)
                .UsingEntity(x => x.ToTable("UserGroups"));
        }
    }
}
