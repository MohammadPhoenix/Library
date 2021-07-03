using Library.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistance.EF.LendingManagments
{
    class LendingManagmentEntityMap : IEntityTypeConfiguration<LendingManagment>
    {
        public void Configure(EntityTypeBuilder<LendingManagment> builder)
        {
            builder.ToTable("LendingManagments");
            builder.HasKey(c=>c.Id);
            builder.Property(c => c.MemberShipId).IsRequired();
            builder.Property(c=>c.BookId).IsRequired();
            builder.Property(c => c.AuthorizedDeliveryDate).IsRequired();
            builder.Property(c => c.DeliveryDate);
            builder.HasOne(c => c.Member).WithMany().HasForeignKey(c => c.MemberShipId);
            builder.HasOne(c => c.Book).WithMany().HasForeignKey(c => c.BookId);

        }
    }
}
