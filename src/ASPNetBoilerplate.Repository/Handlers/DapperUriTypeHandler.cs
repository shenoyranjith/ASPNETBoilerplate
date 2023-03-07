using Dapper;
using System.Data;

namespace ASPNetBoilerplate.Repository.Handlers
{
    /// <summary>
    /// Dapper type conversion handler.
    /// </summary>
    /// <seealso cref="Dapper.SqlMapper.TypeHandler&lt;System.Uri&gt;" />
    public class DapperUriTypeHandler : SqlMapper.TypeHandler<Uri>
    {
        /// <summary>
        /// Converts Uri to string .
        /// </summary>
        /// <param name="parameter">The IDbDataParameter.</param>
        /// <param name="value">The Uri to be converted to string.</param>
        public override void SetValue(IDbDataParameter parameter, Uri value)
        {
            if (parameter != null && value != null)
            {
                parameter.Value = value.ToString();
            }
        }

        /// <summary>
        /// Converts string to Uri .
        /// </summary>
        /// <param name="value">The value to be converted.</param>
        /// <returns>
        /// The converted Uri.
        /// </returns>
        public override Uri Parse(object value)
        {
            return new Uri((string)value);
        }
    }
}
