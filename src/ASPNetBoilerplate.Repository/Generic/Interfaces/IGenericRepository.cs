using ASPNetBoilerplate.Domain.Entities;
using Dapper;
using DapperExtensions;
using System.Data;

namespace ASPNetBoilerplate.Repository.Generic.Interfaces
{
    /// <summary>
    /// Defines methods to access database for entity operations
    /// </summary>
    /// <typeparam name="TEntity">Entity of type BaseEntity</typeparam>
    /// <seealso cref="System.IDisposable" />
    public interface IGenericRepository<TEntity> : IDisposable
        where TEntity : class
    {
        /// <summary>
        /// Sets the page offset for pagination
        /// </summary>
        /// <param name="pageOffset">Page offset integer value</param>
        void SetPageOffset(int pageOffset);

        /// <summary>
        /// Sets the page limit for pagination
        /// </summary>
        /// <param name="pageLimit">Page limit integer value</param>
        void SetPageLimit(int pageLimit);

        /// <summary>
        /// Sets the page limit for pagination
        /// </summary>
        /// <param name="sortData">Sort info of type IList{ISort}</param>
        void SetPageSort(IList<ISort> sortData);

        /// <summary>
        /// Sets  the  page limit,offset and sort information for pagination
        /// </summary>
        /// <param name="pageOffset">Page offset integer value</param>
        /// <param name="pageLimit">Page limit integer value</param>
        /// <param name="sortData">Sort info of type IList{ISort}</param>
        void SetPagingParameters(int pageOffset, int pageLimit, IList<ISort> sortData);

        /// <summary>
        /// Sets  the  page limit and offset for pagination
        /// </summary>
        /// <param name="pageOffset">Page offset integer value</param>
        /// <param name="pageLimit">Page limit integer value</param>
        void SetPagingParameters(int pageOffset, int pageLimit);

        /// <summary>
        /// Returns the entity matched by Id
        /// </summary>
        /// <param name="id">The Id of the entity</param>
        /// <returns>
        /// Entity inherited from BaseEntity
        /// </returns>
        TEntity Get(int id);

        /// <summary>
        /// GetMultiple
        /// </summary>
        /// <param name="query">query</param>
        /// <param name="dynamicParameters">dynamicParameters</param>
        /// <param name="commandType">commandType</param>
        /// <returns>
        /// SqlMapper.GridReader
        /// </returns>
        PageResult<TEntity> GetMultiple(string query, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure);

        /// <summary>
        /// Returns the entities matched by entityIds
        /// </summary>
        /// <param name="entityIds">The Ids of the entities</param>
        /// <returns>
        /// Entities
        /// </returns>
        IEnumerable<TEntity> Get(IEnumerable<int> entityIds);

        /// <summary>
        /// Returns an enumerable list of entities that matched the predicate
        /// </summary>
        /// <param name="predicate">Predicate used to filter the entities</param>
        /// <returns>
        /// Enumerable list of entities that matched the predicate
        /// </returns>
        IEnumerable<TEntity> GetByPredicate(IFieldPredicate predicate);

        /// <summary>
        /// Returns an enumerable list of entities that matched the group of predicates
        /// </summary>
        /// <param name="predicateGroup">A predicate group of type <see cref="IPredicateGroup" /></param>
        /// <returns>
        /// Enumerable list of entities that matched the group of predicates
        /// </returns>
        IEnumerable<TEntity> GetByPredicate(IPredicateGroup predicateGroup);

        /// <summary>
        /// Defines async version of <see cref="Get(int)" />
        /// </summary>
        /// <param name="id">Id of the Entity</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation. Returns Entity inherited from BaseEntity on completion
        /// </returns>
        Task<TEntity> GetAsync(int id);

        /// <summary>
        /// Defines async version of <see cref="Get(int)" /> with a cancellation token
        /// </summary>
        /// <param name="id">Id of the Entity</param>
        /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        Task<TEntity> GetAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Returns the paginated list of all entities
        /// </summary>
        /// <returns>
        /// IEnumerable of Entity inherited from BaseEntity
        /// </returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Defines async version of <see cref="GetAll" />
        /// </summary>
        /// <returns>
        /// IEnumerable of Entity inherited from BaseEntity
        /// </returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Defines async version of <see cref="GetAll" /> with a cancellation token
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
        /// <returns>
        /// IEnumerable of Entity inherited from BaseEntity
        /// </returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Inserts the entity into the database
        /// </summary>
        /// <param name="entity">Entity of type BaseEntity</param>
        /// <returns>
        /// Id of the inserted entity of type Int64
        /// </returns>
        int Add(TEntity entity);

        /// <summary>
        /// Defines async version of <see cref="Add" />
        /// </summary>
        /// <param name="entity">Entity of type BaseEntity</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation. Returns Id of the inserted entity of type Int64 on completion.
        /// </returns>
        Task<long> AddAsync(TEntity entity);

        /// <summary>
        /// Defines async version of <see cref="Add" /> with a cancellation token
        /// </summary>
        /// <param name="entity">Entity of type BaseEntity</param>
        /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation. Returns Id of the inserted entity of type Int64 on completion.
        /// </returns>
        Task<long> AddAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes the entity identified by the Id from the database
        /// </summary>
        /// <param name="id">Id of the entity of type Int32</param>
        void Delete(int id);

        /// <summary>
        /// Defines async version of <see cref="Delete" />
        /// </summary>
        /// <param name="id">Id of the entity of type Int32</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation. Returns Id of the entity of type Int32 on completion.
        /// </returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Defines async version of <see cref="Delete" /> with a cancellation token
        /// </summary>
        /// <param name="id">Id of the entity of type Int32</param>
        /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation. Returns Id of the entity of type Int32 on completion.
        /// </returns>
        Task DeleteAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the entity in the database
        /// </summary>
        /// <param name="entity">Entity inherited from BaseEntity</param>
        /// <returns>
        /// Boolean value indicating success or failure
        /// </returns>
        bool Update(TEntity entity);

        /// <summary>
        /// Defines async version of <see cref="Update" />
        /// </summary>
        /// <param name="entity">Entity of type BaseEntity</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation. Returns Boolean value indicating success or failure on completion.
        /// </returns>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Defines async version of <see cref="Update" /> with a cancellation token
        /// </summary>
        /// <param name="entity">Entity of type BaseEntity</param>
        /// <param name="cancellationToken"><see cref="CancellationToken" /></param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation. Returns Boolean value indicating success or failure on completion.
        /// </returns>
        Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Returns the count of entities that match the predicate
        /// </summary>
        /// <param name="predicate">Predicate used to match the entities</param>
        /// <returns>
        /// Count of entities of type Int64
        /// </returns>
        long Count(IFieldPredicate predicate);

        /// <summary>
        /// Counts the by predicate group.
        /// </summary>
        /// <param name="predicateGroup">The predicate group.</param>
        /// <returns>
        /// Count of entities
        /// </returns>
        long CountByPredicateGroup(IPredicateGroup predicateGroup);

        /// <summary>
        /// Method to Execute the Sql queries
        /// </summary>
        /// <param name="query">Sql Query</param>
        /// <param name="parameters">Sql Query Parameters</param>
        /// <param name="commandType">SQL Command Type</param>
        /// <param name="commandTimeOut">SQL Command Timeout</param>
        /// <returns>
        /// List of entities
        /// </returns>
        IEnumerable<TEntity> Execute(string query, object parameters = null, CommandType commandType = CommandType.Text, int commandTimeOut = 60);

        /// <summary>
        /// Executes a Sql Query with optional parameters and returns a Single Entity Result
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns>
        /// Single Entity/&gt;/&gt;
        /// </returns>
        TEntity ExecuteSingle(string query, object parameters = null);

        /// <summary>
        /// Executes a Sql Query with optional parameters and returns a Single Entity Result or Default Value for the entity
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <param name="parameters">SQL Parameters</param>
        /// <returns>
        /// a Single Entity Result or Default Value for the entity
        /// </returns>
        TEntity ExecuteSingleOrDefault(string query, object parameters = null);

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
        public void BulkInsert<T>(string destinationTableName, IEnumerable<T> entities, IEnumerable<BulkCopyColumnMapping> columnMappings = null, int? batchSize = null, bool isTableExist = true);

        /// <summary>
        /// Inserts entities into the database in bulk.
        /// </summary>
        /// <param name="destinationTableName">Name of the destination table.</param>
        /// <param name="entities">The entities.</param>
        /// <param name="columnMappings">The column mappings.</param>
        /// <param name="batchSize">Size of the batch.</param>
        /// <exception cref="System.ArgumentNullException">entities</exception>
        public void BulkInsert(string destinationTableName, IEnumerable<TEntity> entities, IEnumerable<BulkCopyColumnMapping> columnMappings = null, int? batchSize = null);
    }

}
