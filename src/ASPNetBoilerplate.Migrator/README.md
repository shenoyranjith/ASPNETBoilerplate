# Setup Database

## Create Database
```
USE master
GO
CREATE LOGIN dev with password = 'Pa55w0rd'
go
CREATE DATABASE ASPNetBoilerplateDB
GO
ALTER DATABASE ASPNetBoilerplateDB SET RECOVERY SIMPLE;
GO
Use ASPNetBoilerplateDB
GO
CREATE USER [dev] FOR LOGIN [dev]
GO
ALTER ROLE [db_owner] ADD MEMBER [dev]
GO
```

## Add Migrations

1. Run the following in powershell to add a migraion file

```
Add-FluentMigration <Migration_Name>
```

2. Add the up and down scripts

## Run Migrations

1. Open powershell and run

```
set ASPNETCORE_ENVIRONMENT=<Config>
```

2. Publish the solution using

```
dotnet publish -c <Config>
```

3. Run the migration using

```
.\bin\<Config>\net7.0\publish\ASPNetBoilerplate.Migrator.exe [true] ['all' || migration_number]
```

OR

Just run the migrate.ps1 file.
```
migrate.ps1 [Debug || Release] [true] ['all' || migration_number]
```
