using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class Employee : ICloneable
    {
        public String EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Position { get; set; }
        public String Department { get; set; }
        public List<Employee> DirectReports { get; set; }

        public Compensation Compensation { get; set; }

        public object Clone()
        {
            return new Employee
            {
                Compensation = this.Compensation,
                Department = this.Department,
                DirectReports = this.DirectReports,
                EmployeeId = this.EmployeeId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Position = this.Position
            };
        }

        public override string ToString()
        {
            return 
                (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName)) 
                ? $"{FirstName} {LastName}" : base.ToString();
        }
    }
}
