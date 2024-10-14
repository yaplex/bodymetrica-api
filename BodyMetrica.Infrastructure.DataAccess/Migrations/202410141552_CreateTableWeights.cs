using FluentMigrator;

namespace BodyMetrica.Infrastructure.DataAccess.Migrations;

[Migration(202410141552)]
public class CreateTableWeights : Migration
{
    public override void Up()
    {
        Create.Table("BodyMass")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("WeightInKg").AsDecimal().NotNullable()
            .WithColumn("RecordDate").AsDateTimeOffset().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("BodyMass");
    }
}