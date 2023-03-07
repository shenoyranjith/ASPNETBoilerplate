namespace ASPNetBoilerplate.Repository.Helpers
{
    /// <summary>
    /// Sql Helpers
    /// </summary>
    public static class SqlHelper
    {
        /// <summary>
        /// Gets the type of the SQL column type for given c sharp.
        /// </summary>
        /// <param name="cSharpDataType">Type of the c sharp data.</param>
        /// <returns></returns>
        public static string GetSqlColumnTypeForGivenCSharpType(Type cSharpDataType)
        {
            switch (cSharpDataType.Name.ToString().ToUpperInvariant())
            {
                case "BOOL":
                case "BOOLEAN":
                    return "bit";
                case "BYTE":
                    return "tinyint";
                case "CHAR":
                    return "char(1)";
                case "DATETIME":
                    return "datetime";
                case "DATETIMEOFFSET":
                    return "datetimeoffset";
                case "DECIMAL":
                    return "decimal(18,6)";
                case "DOUBLE":
                    return "float";
                case "INT":
                case "int32":
                    return "int";
                case "LONG":
                case "INT64":
                    return "bigint";
                case "SMALLINT":
                    return "smallint";
                case "BYTE[]":
                    return "varbinary(20)";
                case "TIMESPAN":
                    return "time";
                default:
                    return "nvarchar(max)";
            }
        }
    }
}
