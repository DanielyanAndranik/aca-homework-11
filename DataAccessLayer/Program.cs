using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            DataAccessLayer dal = new DataAccessLayer();

            var a = dal.GetData<Employees>("GetAllEmployees", new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("Id", 2), new KeyValuePair<string, object>("Name", "Anne")});

            foreach (var b in a)
            {
                Console.WriteLine(b);
            }
        }
    }
}