using Domain.Users.Entity;
using Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;

public class MovieListConfiguration : IEntityTypeConfiguration<MovieList>
{
    public void Configure(EntityTypeBuilder<MovieList> builder)
    {
        builder
           .HasKey(x => x.Id);

        builder
           .Property(x => x.Id)
           .ValueGeneratedNever()
           .HasConversion(
           id => id.Value,
           value => MovieListId.Create(value));

        builder.OwnsMany(x => x.MovieIds, miBuilder =>
        {
            miBuilder.ToTable("MovieList_Movies");
            miBuilder.WithOwner().HasForeignKey("MovieListId");

            miBuilder.HasKey("MovieListId", "Value");

            //Value generated never, if removed, the list is not found after making changes.
            miBuilder.Property(x => x.Value).ValueGeneratedNever();

        });


        builder.Navigation(x => x.MovieIds)
            .Metadata.SetField("_movieIds");

        builder.Metadata.FindNavigation(nameof(MovieList.MovieIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
