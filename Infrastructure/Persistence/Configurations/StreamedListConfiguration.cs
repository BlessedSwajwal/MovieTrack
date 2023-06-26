using Domain.Users.Entity;
using Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;

public class StreamedListConfiguration : IEntityTypeConfiguration<StreamedList>
{
    public void Configure(EntityTypeBuilder<StreamedList> builder)
    {
        builder
           .HasKey(x => x.Id);

        builder
           .Property(x => x.Id)
           .ValueGeneratedNever()
           .HasConversion(
           id => id.Value,
           value => StreamedListId.Create(value));

        builder
            .OwnsMany(x => x.MovieIds, mi =>
            {
                mi.WithOwner().HasForeignKey("OwnerId");
                mi.HasKey("OwnerId", "Value");

                mi.Property(x => x.Value).ValueGeneratedNever();
            });

        builder.Navigation(x => x.MovieIds)
            .Metadata.SetField("_movieIds");

        builder.Metadata.FindNavigation(nameof(StreamedList.MovieIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
