using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    /// <summary>
    /// A class for implementation of data access layer.
    /// </summary>
    class DataAccessLayer : IDataAccessLayer
    {
        /// <summary>
        /// The method executes the code and returns the result.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="code">The code.</param>
        /// <param name="parameters">The KeyValuePair parameters.</param>
        /// <returns>Returns an Enumerable of T-s.</returns>
        public IEnumerable<T> GetData<T>(string code, params KeyValuePair<string, object>[] parameters)
        {
            string connectionString = null;

            var lines = File.ReadLines(@"D:\Projects\DataAccessLayer\DataAccessLayer\resources.txt");

            foreach (var line in lines)
            {
                var operations = line.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
                if (operations[0].Trim() == "ConnectionString")
                {
                    connectionString = operations[1].Trim();
                    break;
                };

            }
            Console.WriteLine(connectionString);

            List<T> list = new List<T>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = null;

                string[] operations = null;

                foreach (var line in lines)
                {
                    operations = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if(operations[0].Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries)[0].Trim() == code)
                    {
                        query = operations[0].Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                        break;
                    }
                }

                SqlCommand command = new SqlCommand(query, connection);

                if (operations.Length > 1)
                {
                    command.CommandType = CommandType.StoredProcedure;
                    for(var i = 1; i < operations.Length; i++)
                    {
                        var check = false;
                        var key = operations[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();                      
                        foreach(var keyValuePair in parameters)
                        {
                            if(key == keyValuePair.Key)
                            {
                                command.Parameters.AddWithValue(operations[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim(), keyValuePair.Value);
                                check = true;
                                break;
                            }
                        }
                        if(!check)
                        {
                            throw new ArgumentException("Parameter has not been given or been invalid");
                        }
                    }
                }

                Console.WriteLine(query);

                           

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        T obj = (T)typeof(T).GetConstructor(new Type[] { }).Invoke(null);
                        foreach (var propertyInfo in obj.GetType().GetProperties())
                        {
                            propertyInfo.SetValue(obj, reader[propertyInfo.Name]);
                            //Console.WriteLine(reader[propertyInfo.Name]);
                        }

                        list.Add(obj);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }               
            }

            return list;
        }
    }
}
