using FluentMigrator;

namespace BodyMetrica.Infrastructure.DataAccess.Migrations;

[Migration(202410161703)]
public class CreateUserTable : Migration
{
    public override void Up()
    {
        Create.Table("ApplicationUsers")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("ExternalId").AsString().NotNullable().Indexed()
            .WithColumn("WeightUnits").AsString().NotNullable().Indexed()
            .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable();

    }

    public override void Down()
    {
        Delete.Table("ApplicationUsers");
    }
}