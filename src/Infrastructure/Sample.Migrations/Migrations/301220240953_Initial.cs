using FluentMigrator;

namespace Sample.Migrations.Migrations;

[Migration(301220240953)]
public class _301220240953_Initial : Migration
{
    public override void Up()
    {
        Create.Table("Users")
             .WithColumn("Id").AsInt64().NotNullable().Identity()
             .WithColumn("Name").AsString().NotNullable()
             .WithColumn("Email").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Users");
    }
}
