using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Employee test class.
    /// </summary>
    class Employees
    {
        /// <summary>
        /// The id of the employee.
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// The first name of the employee.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the employee.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// ToString method.
        /// </summary>
        /// <returns>Returns string.</returns>
        public override string ToString()
        {
            return $"{this.EmployeeID} {this.FirstName} {this.LastName}.";
        }

    }
}
