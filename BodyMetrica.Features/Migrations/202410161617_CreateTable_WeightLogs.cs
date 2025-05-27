using FluentMigrator;

namespace BodyMetrica.Infrastructure.DataAccess.Migrations;

[Migration(202410161617)]
public class CreateTable_WeightLogs : Migration
{
    public override void Up()
    {
        Create.Table("WeightLogs")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("OwnerId").AsInt32()
            .WithColumn("WeightInKg").AsDecimal(10, 5).NotNullable()
            .WithColumn("RecordDate").AsDateTimeOffset().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("WeightLogs");
    }
}