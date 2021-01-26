using FluentMigrator;

namespace UrlBitlyClone.DatabaseMigrations.Migrations
{
    /// <summary>
    /// The table names are terrible names.
    /// </summary>
    /// <seealso cref="FluentMigrator.Migration" />
    [TimestampedMigration(2021, 01, 26, 12, 02)]
    public class _20210126_FixTableNames : Migration
    {
        public override void Up()
        {
            Rename.Table("UrlShortening").InSchema("Url").To("UrlShortForm").InSchema("Url");
            Rename.Column("UrlShorteningId").OnTable("UrlShortForm").InSchema("Url").To("UrlShortFormId");
        }

        public override void Down()
        {
            Rename.Table("UrlShortForm").InSchema("Url").To("UrlShortening").InSchema("Url");
            Rename.Column("UrlShortFormId").OnTable("UrlShortForm").InSchema("Url").To("UrlShorteningId");
        }
    }
}
