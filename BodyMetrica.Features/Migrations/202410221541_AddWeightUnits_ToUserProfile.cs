using FluentMigrator;

namespace BodyMetrica.Infrastructure.DataAccess.Migrations;

[Migration(202410221541)]
public class AddWeightUnitsToUserProfile : Migration
{
    public override void Up()
    {
        Alter.Table("Users")
            .AddColumn("WeightUnits").AsString(10).WithDefaultValue("kg");
    }

    public override void Down()
    {
        Delete.Column("WeightUnits").FromTable("Users");
    }
}