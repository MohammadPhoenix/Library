using Library.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistance.EF.Books
{
    class BookEntityMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Title).HasMaxLength(50).IsRequired();
            builder.Property(_ => _.Writer).HasMaxLength(50).IsRequired();
            builder.Property(_ => _.BookCategoryId).IsRequired();
            builder.Property(_ => _.MinimumAge).IsRequired();
            builder.Property(_ => _.MaximumAge).IsRequired();
            builder.HasOne(_ => _.BookCategory).WithMany().HasForeignKey(_ => _.BookCategoryId);
        }
    }
}
