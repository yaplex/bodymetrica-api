using FluentMigrator;

namespace BodyMetrica.Infrastructure.DataAccess.Migrations;

[Migration(202410222132)]
public class AddUserProfileProperties : Migration
{
    public override void Up()
    {
        Alter.Table("Users")
            .AddColumn("Name").AsString(250).Nullable()
            .AddColumn("Picture").AsString(1250).Nullable()
            .AddColumn("Email").AsString(500).Nullable()
            .AddColumn("EmailVerified").AsBoolean().Nullable()
            ;
    }

    public override void Down()
    {
        Delete.Column("Name")
            .Column("Picture")
            .Column("Email")
            .Column("EmailVerified")
            .FromTable("Users");
    }
}