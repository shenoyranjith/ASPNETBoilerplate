using FluentMigrator;

namespace ASPNetBoilerplate.Migrator.Migrations
{
    [Migration(20230306220822)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            // Users table
            Create
                .Table("Users")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey("Users_PK")
                .WithColumn("Username").AsString().NotNullable()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().Nullable()
                .WithColumn("EmailAddress").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("UserRole").AsInt32().NotNullable()
                .WithColumn("LoginAttempts").AsInt32().Nullable()
                .WithColumn("CreatedAt").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("UpdatedAt").AsDateTime2().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);

            // Insert admin user
            var adminRow = new Dictionary<string, object>()
                {
                    { "Username","admin" },
                    { "Firstname", "Admin" },
                    { "EmailAddress", "admin@example.com" },
                    { "Password", "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8" }, //password
                    { "UserRole", "0" }
                };
            Insert
                .IntoTable("Users")
                .Row(adminRow);
        }

        public override void Down()
        {
            // Users table
            Delete.Table("Users");
        }
    }
}
