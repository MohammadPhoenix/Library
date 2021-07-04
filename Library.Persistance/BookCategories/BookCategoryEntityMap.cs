using Library.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistance.EF.BookCategories
{
    class BookCategoryEntityMap : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Title).IsRequired();
        }
    }
}
