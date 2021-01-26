using FluentMigrator;

namespace UrlBitlyClone.DatabaseMigrations.Migrations
{
    [TimestampedMigration(2021, 01, 25, 17, 01)]
    public class _20210125_AddSerilog : Migration
    {
        public override void Down()
        {
            Delete.Table("Logs").InSchema("Logging");
            Delete.Schema("Logging");
        }

        public override void Up()
        {

            Create.Schema("Logging");

            Execute.Sql($@"CREATE TABLE [Logging].[Serilog] (
                       [Id] int IDENTITY(1,1) NOT NULL,
                       [Message] nvarchar(max) NULL,
                       [MessageTemplate] nvarchar(max) NULL,
                       [Level] nvarchar(128) NULL,
                       [TimeStamp] datetime NOT NULL,
                       [Exception] nvarchar(max) NULL,
                       [Properties] nvarchar(max) NULL
                       CONSTRAINT [PK_Serilog] PRIMARY KEY CLUSTERED ([Id] ASC) 
                    );");
        }
    }
}
