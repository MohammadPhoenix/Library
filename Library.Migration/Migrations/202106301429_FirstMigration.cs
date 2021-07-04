using FluentMigrator;

namespace Library.Migration.Migrations
{
    [Migration(202106301429)]
    public class _202106301429_FirstMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("BookCategories")
                .WithColumn("Id").AsInt16().NotNullable().PrimaryKey().Identity()
                .WithColumn("Title").AsString(50).NotNullable();

            Create.Table("Books")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Title").AsString(50).NotNullable()
                .WithColumn("Writer").AsString(50).NotNullable()
                .WithColumn("BookCategoryId").AsInt16().NotNullable()
                .WithColumn("MinimumAge").AsByte().NotNullable()
                .WithColumn("MaximumAge").AsByte().NotNullable();

            Create.Table("MemberShips")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("FullName").AsString(50).NotNullable()
                .WithColumn("BirthDate").AsDateTime().NotNullable()
                .WithColumn("Address").AsString().NotNullable();

            Create.Table("LendingManagments")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("MemberShipId").AsInt32().NotNullable()
                .WithColumn("BookId").AsInt32().NotNullable()
                .WithColumn("AuthorizedDeliveryDate").AsDateTime().NotNullable()
                .WithColumn("DeliveryDate").AsDateTime().Nullable();

            Create.ForeignKey("FK_Books_BookCategories")
                .FromTable("Books").ForeignColumn("BookCategoryId").ToTable("BookCategories").PrimaryColumn("Id");

            Create.ForeignKey("FK_LendingManagments_MemberShips")
                .FromTable("LendingManagments").ForeignColumn("MemberShipId").ToTable("MemberShips").PrimaryColumn("Id");

            Create.ForeignKey("FK_LendingManagments_Books")
                .FromTable("LendingManagments").ForeignColumn("BookId").ToTable("Books").PrimaryColumn("Id");
        }
        public override void Down()
        {
            Delete.ForeignKey("FK_LendingManagments_Books").OnTable("LendingManagments");
            Delete.ForeignKey("FK_LendingManagments_MemberShips").OnTable("LendingManagments");
            Delete.ForeignKey("FK_Books_BookCategories").OnTable("Books");
            Delete.Table("LendingManagments");
            Delete.Table("MemberShips");
            Delete.Table("Books");
            Delete.Table("BookCategories");
        }


    }
}
