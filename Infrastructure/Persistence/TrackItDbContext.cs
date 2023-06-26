using Domain.Users;
using Domain.Users.Entity;
using Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class TrackItDbContext : DbContext
{
    public TrackItDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<StreamedList> StreamedLists { get; set; } = null!;
    public DbSet<MovieList> Movies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.ApplyConfigurationsFromAssembly(typeof(TrackItDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}


