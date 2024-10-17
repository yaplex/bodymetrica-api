using FluentMigrator;

namespace BodyMetrica.Infrastructure.DataAccess.Migrations;

[Migration(202410161617)]
public class CreateTable_WeightLogs : Migration
{
    public override void Up()
    {
        Create.Table("WeightLogs")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("UserId").AsInt32()
            .WithColumn("WeightInKg").AsDecimal().NotNullable()
            .WithColumn("RecordDate").AsDateTimeOffset().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("WeightLogs");
    }
}