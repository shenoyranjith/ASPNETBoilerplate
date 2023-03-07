namespace ASPNetBoilerplate.Repository
{
    /// <summary>
    /// Represents the mapping class for SQL bulk copy operation.
    /// </summary>
    public class BulkCopyColumnMapping
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BulkCopyColumnMapping" /> class.
        /// </summary>
        /// <param name="sourceColumn">The source column.</param>
        /// <param name="destinationColumn">The destination column.</param>
        /// <param name="ignoreColumn">if set to <c>true</c> [ignore column].</param>
        public BulkCopyColumnMapping(string sourceColumn, string destinationColumn, bool ignoreColumn = false)
        {
            SourceColumn = sourceColumn;
            DestinationColumn = destinationColumn;
            IgnoreColumn = ignoreColumn;
        }

        /// <summary>
        /// Gets or sets the source column.
        /// </summary>
        /// <value>
        /// The source column.
        /// </value>
        public string SourceColumn { get; set; }

        /// <summary>
        /// Gets or sets the destination column.
        /// </summary>
        /// <value>
        /// The destination column.
        /// </value>
        public string DestinationColumn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [ignore column].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ignore column]; otherwise, <c>false</c>.
        /// </value>
        public bool IgnoreColumn { get; set; }
    }
}
