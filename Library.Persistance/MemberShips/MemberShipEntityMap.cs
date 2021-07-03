using Library.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistance.EF.MemberShips
{
    class MemberShipEntityMap : IEntityTypeConfiguration<MemberShip>
    {
        public void Configure(EntityTypeBuilder<MemberShip> builder)
        {
            builder.ToTable("MemberShips");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FullName).HasMaxLength(50).IsRequired();
            builder.Property(c => c.BirthDate).IsRequired();
            builder.Property(c => c.Address).IsRequired();
        }
    }
}
