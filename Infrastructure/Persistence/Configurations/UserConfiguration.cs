using Domain.Users;
using Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        CreateUsersTable(builder);
        ConfigureUserMovieListIdsTable(builder);
    }

    private void ConfigureUserMovieListIdsTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(u => u.MovieListIds, movieListBuilder =>
        {
            movieListBuilder.ToTable("User_MovieListIds");

            movieListBuilder.WithOwner()
                .HasForeignKey("UserId");

            movieListBuilder.HasKey("Id");

            movieListBuilder
                .Property( ml => ml.Value)
                .HasColumnName("MovieListId")
                .ValueGeneratedNever();
        });

        builder.Metadata
            .FindNavigation(nameof(User.MovieListIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void CreateUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder
            .Property(u => u.FirstName)
            .HasMaxLength(50);

        builder
            .Property(u => u.LastName)
            .HasMaxLength(50);

        builder
            .Property(u => u.Email)
            .HasMaxLength(150);

        builder
            .Property(u => u.Password);

        builder
            .Property(u => u.EmailVerified);

        builder
            .Property(u => u.streamedListId)
            .HasConversion(
                id => id.Value,
                value => StreamedListId.Create(value));

        builder
            .Property(u => u.CreatedDateTime);

        builder
            .Property(u => u.UpdatedDateTime);
    }


}
