-- Drops all tables in a database. Run remove all FK's first
-- Source: https://stackoverflow.com/questions/27606518/how-to-drop-all-tables-from-a-database-with-one-sql-query

DECLARE @sql NVARCHAR(max)=''

SELECT @sql += ' Drop table ' + QUOTENAME(TABLE_SCHEMA) + '.'+ QUOTENAME(TABLE_NAME) + '; '
FROM   INFORMATION_SCHEMA.TABLES
WHERE  TABLE_TYPE = 'BASE TABLE'

Exec Sp_executesql @sql
