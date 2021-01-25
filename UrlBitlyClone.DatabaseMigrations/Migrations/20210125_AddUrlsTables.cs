using FluentMigrator;

namespace UrlBitlyClone.DatabaseMigrations.Migrations
{
    [TimestampedMigration(2021, 01, 25, 17, 00)]
    public class _20210125_AddUrlsTables : Migration
    {
        public override void Up()
        {
            Create.Schema("Url");

            Create.Table("UrlShortening").InSchema("Url")
                .WithColumn("UrlShorteningId").AsInt64().PrimaryKey().Identity()
                .WithColumn("FullUrl").AsAnsiString(int.MaxValue).NotNullable()
                .WithColumn("ShortenedUrl").AsAnsiString(int.MaxValue).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("UrlShortening").InSchema("Url");
            Delete.Schema("Url");
        }
    }
}
