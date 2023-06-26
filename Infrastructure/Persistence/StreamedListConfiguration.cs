//using Domain.Users.Entity;
//using Domain.Users.ValueObjects;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.Persistence;

//public class StreamedListConfiguration : IEntityTypeConfiguration<StreamedList>
//{
//    public void Configure(EntityTypeBuilder<StreamedList> builder)
//    {
//        builder
//            .HasKey(x => x.Id);

//        builder
//           .Property(x => x.Id)
//           .ValueGeneratedNever()
//           .HasConversion(
//           id => id.Value,
//           value => StreamedListId.Create(value));

//        builder.OwnsMany(x => x.MovieIds, miBuilder =>
//        {
//            miBuilder.ToTable("StreamedList_MovieIds");

//            miBuilder.WithOwner().HasForeignKey("StreamedListId");

//            miBuilder.HasKey(new[] {"StreamedListId", "Value"} );

//            miBuilder.Property<int>("Value").HasColumnName("TmdbID").IsRequired();


//        });
//    }
//}
