using ASPNetBoilerplate.Domain.Entities;
using ASPNetBoilerplate.Repository.Generic.Interfaces;
using Dapper;
using DapperExtensions;
using System.Data;
using ASPNetBoilerplate.Domain.Interfaces;
using System.Data.SqlClient;
using ASPNetBoilerplate.Repository.Helpers;

namespace ASPNetBoilerplate.Repository.Generic
{
    /// <summary>
    /// Contains methods to access database for Entity operations
    /// </summary>
    /// <typeparam name="TEntity">Entity inherited from BaseEntity</typeparam>
    /// <seealso cref="ASPNetBoilerplate.Repository.Generic.Interfaces.IGenericRepository&lt;TEntity&gt;" />
    /// <seealso cref="ASPNetBoilerplate.Repository.Generic.GenericExecuteRepository" />
    /// <seealso cref="ASPNetBoilerplate.Repository.Generic.Interfaces.IGenericRepository&lt;TEntity&gt;" />
    public abstract class GenericRepository<TEntity> : GenericExecuteRepository, IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        /// <summary>
        /// The disposed value
        /// </summary>
        private bool _disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}" /> class.
        /// Constructor which initialises Connection String based on ConnectionResolver
        /// </summary>
        /// <param name="connectionResolver">Implementation of IConnectionResolver</param>
        protected GenericRepository(IConnectionResolver connectionResolver)
            : base(connectionResolver.ConnectionString)
        {
            PageSort = new List<ISort>
            {
                Predicates.Sort<TEntity>(entity => entity.Id, true)
            };
            PageOffset = 0;
            PageLimit = 100;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="GenericRepository{TEntity}" /> class.
        /// Calls Dispose with false
        /// </summary>
        ~GenericRepository()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        /// <summary>
        /// Gets or sets the page sort.
        /// </summary>
        /// <value>
        /// The page sort.
        /// </value>
        private IList<ISort> PageSort { get; set; }

        /// <summary>
        /// Gets or sets the page offset.
        /// </summary>
        /// <value>
        /// The page offset.
        /// </value>
        private int PageOffset { get; set; }

        /// <summary>
        /// Gets or sets the page limit.
        /// </summary>
        /// <value>
        /// The page limit.
        /// </value>
        private int PageLimit { get; set; }

        /// <summary>
        /// Sets the page offset for pagination
        /// </summary>
        /// <param name="pageOffset">Page offset integer value</param>
        /// <inheritdoc cref="IGenericRepository{TEntity}.SetPageOffset(int)" />
        public void SetPageOffset(int pageOffset)
        {
            PageOffset = pageOffset;
        }

        /// <summary>
        /// Sets the page limit for pagination
        /// </summary>
        /// <param name="pageLimit">Page limit integer value</param>
        /// <inheritdoc cref="IGenericRepository{TEntity}.SetPageLimit(int)" />
        public void SetPageLimit(int pageLimit)
        {
            PageLimit = pageLimit;
        }

        /// <summary>
        /// Sets the page sort for pagination
        /// </summary>
        /// <param name="sortData">Sort info of type IList{ISort}</param>
        /// <inheritdoc cref="IGenericRepository{TEntity}.SetPageSort(IList{ISort})" />
        public void SetPageSort(IList<ISort> sortData)
        {
            PageSort = sortData;
        }

        /// <summary>
        /// Sets  the  page limit,offset and sort information for pagination
        /// </summary>
        /// <param name="pageOffset">Page offset integer value</param>
        /// <param name="pageLimit">Page limit integer value</param>
        /// <param name="sortData">Sort info of type IList{ISort}</param>
        /// <inheritdoc cref="IGenericRepository{TEntity}.SetPageSort(IList{ISort})" />
        public void SetPagingParameters(int pageOffset, int pageLimit, IList<ISort> sortData)
        {
            PageOffset = pageOffset;
            PageLimit = pageLimit;
            PageSort = sortData;
        }

        /// <summary>
        /// Sets  the  page limit and offset for pagination
        /// </summary>
        /// <param name="pageOffset">Page offset integer value</param>
        /// <param name="pageLimit">Page limit integer value</param>
        /// <inheritdoc cref="IGenericRepository{TEntity}.SetPagingParameters(int,int)" />
        public void SetPagingParameters(int pageOffset, int pageLimit)
        {
            PageOffset = pageOffset;
            PageLimit = pageLimit;
        }

        #region Get methods

        /// <summary>
        /// Returns the entity matched by Id
        /// </summary>
        /// <param name="id">The Id of the entity</param>
        /// <returns>
        /// Entity inherited from BaseEntity
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.Get(int)" />
        public TEntity Get(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Get<TEntity>(id);
            }
        }

        /// <summary>
        /// GetMultiple
        /// </summary>
        /// <param name="query">query</param>
        /// <param name="dynamicParameters">dynamicParameters</param>
        /// <param name="commandType">commandType</param>
        /// <returns>
        /// PageResult - TEntity
        /// </returns>
        public PageResult<TEntity> GetMultiple(string query, DynamicParameters dynamicParameters, CommandType commandType)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.QueryMultiple(
                    query,
                    dynamicParameters,
                    commandType: commandType);
                return new PageResult<TEntity>()
                {
                    TotalCount = result.Read<int>().First(),
                    Data = result.Read<TEntity>().ToList()
                };
            }
        }

        /// <summary>
        /// Returns the entities matched by entityIds
        /// </summary>
        /// <param name="entityIds">The Ids of the entities</param>
        /// <returns>
        /// Entities
        /// </returns>
        public IEnumerable<TEntity> Get(IEnumerable<int> entityIds)
        {
            var predicateGroup = new PredicateGroup
            {
                Operator = GroupOperator.Or,
                Predicates = entityIds.Select(e => { return Predicates.Field<TEntity>(dp => dp.Id, Operator.Eq, e); }).ToList<IPredicate>()
            };

            return GetByPredicate(predicateGroup);
        }

        /// <summary>
        /// Returns an enumerable list of entities that matched the predicate
        /// </summary>
        /// <param name="predicate">Predicate used to filter the entities</param>
        /// <returns>
        /// Enumerable list of entities that matched the predicate
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetByPredicate(IFieldPredicate)" />
        public IEnumerable<TEntity> GetByPredicate(IFieldPredicate predicate)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.GetPage<TEntity>(predicate, PageSort, PageOffset, PageLimit).ToList();
            }
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetByPredicate(IPredicateGroup predicateGroup)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.GetPage<TEntity>(predicateGroup, PageSort, PageOffset, PageLimit).ToList();
            }
        }

        /// <summary>
        /// Defines async version of <see cref="M:ASPNetBoilerplate.Repository.Generic.Interfaces.IGenericRepository`1.Get(System.Int32)" />
        /// </summary>
        /// <param name="id">Id of the Entity</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation. Returns Entity inherited from BaseEntity on completion
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAsync(int)" />
        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await GetAsync(id, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Defines async version of <see cref="M:ASPNetBoilerplate.Repository.Generic.Interfaces.IGenericRepository`1.Get(System.Int32)" /> with a cancellation token
        /// </summary>
        /// <param name="id">Id of the Entity</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /></param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAsync(int,CancellationToken)" />
        public virtual async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Get(id), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the paginated list of all entities
        /// </summary>
        /// <returns>
        /// IEnumerable of Entity inherited from BaseEntity
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAll" />
        public IEnumerable<TEntity> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.GetPage<TEntity>(null, PageSort, PageOffset, PageLimit).ToList();
            }
        }

        /// <summary>
        /// Defines async version of <see cref="M:ASPNetBoilerplate.Repository.Generic.Interfaces.IGenericRepository`1.GetAll" />
        /// </summary>
        /// <returns>
        /// IEnumerable of Entity inherited from BaseEntity
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAllAsync()" />
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await GetAllAsync(CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Defines async version of <see cref="M:ASPNetBoilerplate.Repository.Generic.Interfaces.IGenericRepository`1.GetAll" /> with a cancellation token
        /// </summary>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /></param>
        /// <returns>
        /// IEnumerable of Entity inherited from BaseEntity
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.GetAllAsync(CancellationToken)" />
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Task.Run(() => GetAll(), cancellationToken).ConfigureAwait(false);
        }

        #endregion Get methods

        #region Add Methods

        /// <summary>
        /// Inserts the entity into the database
        /// </summary>
        /// <param name="entity">Entity of type BaseEntity</param>
        /// <returns>
        /// Id of the inserted entity of type Int64
        /// </returns>
        /// <inheritdoc cref="Add" />
        public int Add(TEntity entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Insert(entity);
            }
        }

        /// <summary>
        /// Defines async version of <see cref="M:ASPNetBoilerplate.Repository.Generic.Interfaces.IGenericRepository`1.Add(`0)" />
        /// </summary>
        /// <param name="entity">Entity of type BaseEntity</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation. Returns Id of the inserted entity of type Int64 on completion.
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.AddAsync(TEntity)" />
        public virtual async Task<long> AddAsync(TEntity entity)
        {
            return await AddAsync(entity, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Defines async version of <see cref="M:ASPNetBoilerplate.Repository.Generic.Interfaces.IGenericRepository`1.Add(`0)" /> with a cancellation token
        /// </summary>
        /// <param name="entity">Entity of type BaseEntity</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /></param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation. Returns Id of the inserted entity of type Int64 on completion.
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.AddAsync(TEntity,CancellationToken)" />
        public virtual async Task<long> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Add(entity), cancellationToken).ConfigureAwait(false);
        }

        #endregion Add Methods

        #region Delete methods

        /// <inheritdoc/>
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var entity = connection.Get<TEntity>(id);
                connection.Delete(entity);
            }
        }

        /// <summary>
        /// Defines async version of <see cref="M:ASPNetBoilerplate.Repository.Generic.Interfaces.IGenericRepository`1.Delete(System.Int32)" />
        /// </summary>
        /// <param name="id">Id of the entity of type Int32</param>
        /// <inheritdoc cref="IGenericRepository{TEntity}.DeleteAsync(int)" />
        public virtual async Task DeleteAsync(int id)
        {
            await DeleteAsync(id, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Defines async version of <see cref="M:ASPNetBoilerplate.Repository.Generic.Interfaces.IGenericRepository`1.Delete(System.Int32)" /> with a cancellation token
        /// </summary>
        /// <param name="id">Id of the entity of type Int32</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /></param>
        /// <inheritdoc cref="IGenericRepository{TEntity}.DeleteAsync(int,CancellationToken)" />
        public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await Task.Run(() => Delete(id), cancellationToken).ConfigureAwait(false);
        }

        #endregion Delete methods

        #region Update methods

        /// <summary>
        /// Updates the entity in the database
        /// </summary>
        /// <param name="entity">Entity inherited from BaseEntity</param>
        /// <returns>
        /// Boolean value indicating success or failure
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.Update" />
        public bool Update(TEntity entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Update(entity);
            }
        }

        /// <summary>
        /// Defines async version of <see cref="M:ASPNetBoilerplate.Repository.Generic.Interfaces.IGenericRepository`1.Update(`0)" />
        /// </summary>
        /// <param name="entity">Entity of type BaseEntity</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation. Returns Boolean value indicating success or failure on completion.
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.UpdateAsync(TEntity)" />
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            return await Task.Run(() => Update(entity), CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Defines async version of <see cref="M:ASPNetBoilerplate.Repository.Generic.Interfaces.IGenericRepository`1.Update(`0)" /> with a cancellation token
        /// </summary>
        /// <param name="entity">Entity of type BaseEntity</param>
        /// <param name="cancellationToken"><see cref="T:System.Threading.CancellationToken" /></param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation. Returns Boolean value indicating success or failure on completion.
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.UpdateAsync(TEntity,CancellationToken)" />
        public virtual async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return await Task.Run(() => Update(entity), cancellationToken).ConfigureAwait(false);
        }

        #endregion Update methods

        #region Count Methods

        /// <summary>
        /// Returns the count of entities that match the predicate
        /// </summary>
        /// <param name="predicate">Predicate used to match the entities</param>
        /// <returns>
        /// Count of entities of type Int64
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.Count(IFieldPredicate)" />
        public long Count(IFieldPredicate predicate)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Count<TEntity>(predicate);
            }
        }

        /// <summary>
        /// Counts the by predicate group.
        /// </summary>
        /// <param name="predicateGroup">The predicate group.</param>
        /// <returns>
        /// Count of entities
        /// </returns>
        /// <inheritdoc cref="IGenericRepository{TEntity}.CountByPredicateGroup(IPredicateGroup)" />
        public long CountByPredicateGroup(IPredicateGroup predicateGroup)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Count<TEntity>(predicateGroup);
            }
        }

        #endregion Count Methods

        #region Dapper SQL Methods

        /// <summary>
        /// Executes a SQL Query with optional Parameters and Returns a Collection of Entities
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <param name="commandType">SQL Command Type</param>
        /// <param name="commandTimeOut">SQL Command Timeout</param>
        /// <returns>
        /// IEnumerable of Entity
        /// </returns>
        public IEnumerable<TEntity> Execute(string query, object parameters = null, CommandType commandType = CommandType.Text, int commandTimeOut = 60)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<TEntity>(query, parameters, commandType: commandType, commandTimeout: commandTimeOut);
            }
        }

        /// <summary>
        /// Executes a Sql Query with optional parameters and returns a Single Entity Result
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns>
        /// Single Entity/&gt;/&gt;
        /// </returns>
        public TEntity ExecuteSingle(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.QuerySingle<TEntity>(query, parameters, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// Executes a Sql Query with optional parameters and returns a Single Entity Result or Default Value for the entity
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns>
        /// a Single Entity Result or Default Value for the entity
        /// </returns>
        public TEntity ExecuteSingleOrDefault(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.QuerySingleOrDefault<TEntity>(query, parameters, commandType: CommandType.Text);
            }
        }

        #endregion Dapper SQL Methods

        #region Bulk methods

        /// <summary>
        /// Inserts entities into the database in bulk.
        /// </summary>
        /// <typeparam name="T">Generic Class</typeparam>
        /// <param name="destinationTableName">Name of the destination table.</param>
        /// <param name="entities">The entities.</param>
        /// <param name="columnMappings">The column mappings.</param>
        /// <param name="batchSize">Size of the batch.</param>
        /// <param name="isTableExist">A Flag which indicates whether, the table where data need to be dumped, already exists or not.</param>
        /// <exception cref="System.ArgumentNullException">entities</exception>
        public void BulkInsert<T>(string destinationTableName, IEnumerable<T> entities, IEnumerable<BulkCopyColumnMapping> columnMappings = null, int? batchSize = null, bool isTableExist = true)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            bool isExists = true;
            if (!isTableExist)
            {
                // Double checking if table already exists
                string ifExistsSql = @"SELECT COUNT(1) FROM sys.tables WHERE name = @tableName and object_id = OBJECT_ID(@tableName)";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    isExists = ((int)connection.ExecuteScalar(ifExistsSql, new { tableName = destinationTableName }, commandType: CommandType.Text)) == 1;
                }
            }

            string columns = string.Empty;
            if (!isTableExist && !isExists)
            {
                columns = " [Temp_table_Id] bigint IDENTITY(1,1) ";
            }

            var properties = typeof(T).GetProperties();
            IEnumerable<string> mappedProperties = columnMappings?.Select(cm => cm.SourceColumn);

            using (DataTable dataTable = new DataTable())
            {
                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    dataTable.Columns.Add(new DataColumn(property.Name, propertyType));

                    if (!isTableExist && !isExists)
                    {
                        var propertyName = property.Name;

                        if (mappedProperties != null && mappedProperties.Contains(propertyName))
                        {
                            var columnMapping = columnMappings.First(m => m.SourceColumn == propertyName);
                            if (columnMapping.IgnoreColumn)
                            {
                                continue;
                            }

                            propertyName = columnMapping.DestinationColumn;
                        }

                        string column = $" [{propertyName}]" + " " +
                                           SqlHelper.GetSqlColumnTypeForGivenCSharpType(propertyType);

                        columns = string.IsNullOrEmpty(columns) ? column : columns + "," + column;
                    }
                }

                foreach (T entity in entities)
                {
                    object[] values = new object[properties.Length];
                    for (int i = 0; i < properties.Length; i++)
                    {
                        values[i] = properties[i].GetValue(entity);
                    }

                    dataTable.Rows.Add(values);
                }

                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var bulkCopy = new SqlBulkCopy(connection) { DestinationTableName = destinationTableName })
                    {
                        if (!isTableExist && !isExists)
                        {
                            var createTableScript = $@"DROP TABLE 
                                                   IF EXISTS {destinationTableName}
                                                       CREATE TABLE {destinationTableName}
                                                            ({columns})";

                            connection.Execute(createTableScript);
                        }

                        foreach (System.Reflection.PropertyInfo property in properties)
                        {
                            if (mappedProperties is null || (mappedProperties != null && !mappedProperties.Contains(property.Name)))
                            {
                                bulkCopy.ColumnMappings.Add(property.Name, property.Name);
                            }
                            else
                            {
                                var mapping = columnMappings.First(cm => cm.SourceColumn.Equals(property.Name, StringComparison.Ordinal));
                                if (!mapping.IgnoreColumn)
                                {
                                    bulkCopy.ColumnMappings.Add(mapping.SourceColumn, mapping.DestinationColumn);
                                }
                            }
                        }

                        bulkCopy.BulkCopyTimeout = 60;
                        if (batchSize.HasValue)
                        {
                            bulkCopy.BatchSize = batchSize.Value;
                        }

                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        bulkCopy.WriteToServer(dataTable);
                        connection.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Inserts entities into the database in bulk.
        /// </summary>
        /// <param name="destinationTableName">Name of the destination table.</param>
        /// <param name="entities">The entities.</param>
        /// <param name="columnMappings">The column mappings.</param>
        /// <param name="batchSize">Size of the batch.</param>
        /// <exception cref="System.ArgumentNullException">entities</exception>
        public void BulkInsert(string destinationTableName, IEnumerable<TEntity> entities, IEnumerable<BulkCopyColumnMapping> columnMappings = null, int? batchSize = null)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            var properties = typeof(TEntity).GetProperties();
            IEnumerable<string> mappedProperties = columnMappings?.Select(cm => cm.SourceColumn);

            using (DataTable dataTable = new DataTable())
            {
                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    dataTable.Columns.Add(new DataColumn(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType));
                }

                foreach (TEntity entity in entities)
                {
                    object[] values = new object[properties.Length];
                    for (int i = 0; i < properties.Length; i++)
                    {
                        values[i] = properties[i].GetValue(entity);
                    }

                    dataTable.Rows.Add(values);
                }

                using (var connection = new SqlConnection(ConnectionString))
                using (var bulkCopy = new SqlBulkCopy(connection) { DestinationTableName = destinationTableName })
                {
                    foreach (System.Reflection.PropertyInfo property in properties)
                    {
                        if (mappedProperties is null || (mappedProperties != null && !mappedProperties.Contains(property.Name)))
                        {
                            bulkCopy.ColumnMappings.Add(property.Name, property.Name);
                        }
                        else
                        {
                            var mapping = columnMappings.First(cm => cm.SourceColumn.Equals(property.Name, StringComparison.Ordinal));
                            if (!mapping.IgnoreColumn)
                            {
                                bulkCopy.ColumnMappings.Add(mapping.SourceColumn, mapping.DestinationColumn);
                            }
                        }
                    }

                    if (batchSize.HasValue)
                    {
                        bulkCopy.BatchSize = batchSize.Value;
                    }

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    bulkCopy.WriteToServer(dataTable);
                    connection.Close();
                }
            }
        }

        #endregion Bulk methods

        #region IDisposable Support

        /// <inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) below.
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// An override of <see cref="IDisposable.Dispose" /> for the <see cref="GenericRepository{TEntity}" /> class
        /// </summary>
        /// <param name="disposing">Boolean flag to indicate whether to dispose or not</param>
        protected virtual void Dispose(bool disposing)
        {
            // This code is added to correctly implement the disposable pattern.
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // not sure what to dispose here
                }

                _disposedValue = true;
            }
        }
        #endregion IDisposable Support
    }
}
