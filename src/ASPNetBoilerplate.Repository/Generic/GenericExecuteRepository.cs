using System.Data;
using System.Data.SqlClient;
using System.Text;
using ASPNetBoilerplate.Repository.Helpers;
using Dapper;

namespace ASPNetBoilerplate.Repository.Generic
{
    /// <summary>
    /// Represents generic repository which contains methods to execute raw SQL queries
    /// </summary>
    public abstract class GenericExecuteRepository
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericExecuteRepository" /> class.
        /// </summary>
        /// <param name="connectionString">Database Connection String</param>
        protected GenericExecuteRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Gets or sets Database Connection String
        /// </summary>
        /// <value>
        /// Database Connection String
        /// </value>
        protected string ConnectionString { get => _connectionString; set => _connectionString = value; }

        #region Dapper SQL methods

        /// <summary>
        /// Execute a SqlQuery with optional parameters
        /// </summary>
        /// <param name="query">Sql Query to execute</param>
        /// <param name="parameters">Sql Parameters</param>
        /// <param name="commandType">Sql Command Type</param>
        /// <param name="commandTimeOut">Sql Command Time out</param>
        /// <returns>
        /// Number of Rows affected
        /// </returns>
        public virtual int ExecuteNonQuery(string query, object parameters = null, CommandType commandType = CommandType.Text, int commandTimeOut = 60)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Execute(query, parameters, commandType: commandType, commandTimeout: commandTimeOut);
            }
        }

        /// <summary>
        /// Performs Sql BulkCopy
        /// </summary>
        /// <param name="destinationTable">Destination Table's Name</param>
        /// <param name="sourceDt">Source DataTable</param>
        /// <exception cref="System.ArgumentNullException">sourceDt</exception>
        public virtual void BulkCopy(string destinationTable, DataTable sourceDt)
        {
            if (sourceDt is null)
            {
                throw new ArgumentNullException(nameof(sourceDt));
            }

            using (var connection = new SqlConnection(ConnectionString))
            using (var blkCpy = new SqlBulkCopy(connection) { DestinationTableName = destinationTable })
            {
                var createTableScript =
                    new StringBuilder($"CREATE TABLE {destinationTable} ([Id] bigint IDENTITY(1,1))");
                foreach (DataColumn dc in sourceDt.Columns)
                {
                    createTableScript.AppendLine(
                        $"ALTER TABLE {destinationTable} ADD [{dc.ColumnName}] {SqlHelper.GetSqlColumnTypeForGivenCSharpType(dc.DataType)}");

                    blkCpy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                }

                using (var createTableCmd = new SqlCommand(createTableScript.ToString(), connection))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    createTableCmd.ExecuteNonQuery();
                }

                blkCpy.WriteToServer(sourceDt);
            }
        }

        /// <summary>
        /// Deletes the multiple.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="ids">The ids of entities to be deleted.</param>
        public virtual void DeleteMultiple(string tableName, IEnumerable<int> ids)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    var deleteStatement = $@"
                        DELETE 
                        FROM 
                             {tableName}
                        WHERE 
                             [Id] IN({string.Join(",", ids)})";

                    connection.Execute(deleteStatement, transaction: transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        #endregion Dapper SQL methods
    }
}
