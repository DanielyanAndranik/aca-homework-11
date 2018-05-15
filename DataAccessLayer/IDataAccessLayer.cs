using System.Collections.Generic;

namespace DataAccessLayer
{
    /// <summary>
    /// An interface for abstraction data access layer.
    /// </summary>
    interface IDataAccessLayer
    {
        /// <summary>
        /// Abstract method that executes the command and returns result.
        /// </summary>
        /// <typeparam name="T">Return type.</typeparam>
        /// <param name="code">A command for retrieving data.</param>
        /// <param name="parameters">Parameters for command.</param>
        /// <returns>Returns an Enumerable of T-s.</returns>
        IEnumerable<T> GetData<T>(string code, params KeyValuePair<string, object>[] parameters);
    }
}
