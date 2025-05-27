using FluentMigrator;

namespace BodyMetrica.Infrastructure.DataAccess.Migrations;

[Migration(202412151425)]
public class AddBloodPressureLogTable : Migration
{
    private const string TableName = "BloodPressureLogs";

    public override void Up()
    {
        Create.Table(TableName)
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("OwnerId").AsInt32()
            .WithColumn("Systolic").AsInt32().NotNullable()
            .WithColumn("Diastolic").AsInt32().NotNullable()
            .WithColumn("Pulse").AsInt32().NotNullable()
            .WithColumn("Note").AsString().Nullable()
            .WithColumn("RecordDate").AsDateTimeOffset().NotNullable();

    }

    public override void Down()
    {
        Delete.Table(TableName);
    }
}