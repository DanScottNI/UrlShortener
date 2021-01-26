using FluentMigrator;

namespace UrlBitlyClone.DatabaseMigrations.Migrations
{
    /// <summary>
    /// Places a unique constraint on the shortened url column.
    /// </summary>
    [TimestampedMigration(2021, 01, 26, 08, 26)]
    public class _20210126_UniqueConstraint : Migration
    {
        public override void Up()
        {
            Alter.Table("UrlShortening").InSchema("Url").AlterColumn("ShortenedUrl").AsAnsiString(10);
            Create.UniqueConstraint("UC_ShortenedUrl").OnTable("UrlShortening").WithSchema("Url").Column("ShortenedUrl");
        }

        public override void Down()
        {
            Delete.UniqueConstraint("UC_ShortenedUrl").FromTable("UrlShortening").InSchema("Url");
            Alter.Table("UrlShortening").InSchema("Url").AlterColumn("ShortenedUrl").AsAnsiString(int.MaxValue);
        }
    }
}
