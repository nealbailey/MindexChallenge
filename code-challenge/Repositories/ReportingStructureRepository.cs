//-----------------------------------------------------------------------
// Source File:  "ReportingStructureRepository.cs"
// Create Date:  02/04/2022 06:07:00 PM
// Last Updated: 02/04/2022 06:07:00 PM
// Authors(s):   Neal Bailey <nealbailey@hotmail.com>
//-----------------------------------------------------------------------
// COMMERCIAL USE RIGHTS - PROHIBITED PUBLIC DISTRIBUTION
// Intellectual Property Rights Holder: 
// Software Rights Holder: 
//-----------------------------------------------------------------------
// The licensee's rights to use, modify, reproduce, release, perform,
// display, or disclose this technical data/computer software are
// restricted by US patent copyright protections. any reproduction
// of this technical data/computer software or portions thereof marked
// with this legend must also reproduce the all original markings.
//-----------------------------------------------------------------------
// Copyright (c) 2012-2022 
//-----------------------------------------------------------------------
namespace challenge.Repositories
{
    using challenge.Data;
    using challenge.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Linq;

    /// <summary>
    /// Repository abstraction for working with ReportingStructure entities.
    /// </summary>
    public class ReportingStructureRepository : IReportingStructureRepository
    {
        #region Fields
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IReportingStructureRepository> _logger;
        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructors
        /// </summary>
        /// <param name="logger">Generic Microsoft Logger.</param>
        /// <param name="employeeContext">The database context.</param>
        public ReportingStructureRepository(ILogger<IReportingStructureRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fetch the <see cref="ReportingStructure"/> based on the <see cref="Employee"/> id value.
        /// </summary>
        /// <param name="id">The <see cref="Employee"/>Id Guid string.</param>
        /// <param name="includeDirectReports">Flag to include or exclude nest direct report <see cref="Employee"/> references.</param>
        /// <returns>The matched <see cref="ReportingStructure"/> or an empty instance if none was found.</returns>
        /// <exception cref="System.ArgumentNullException">The exception that is thrown when a null reference (Nothing in Visual Basic) is passed to a method that does not accept it as a valid argument.</exception>
        /// <exception cref="System.InvalidOperationException">The exception that is thrown when a method call is invalid for the object's current state.</exception>
        public ReportingStructure GetReportingStructure(string id, bool includeDirectReports)
        {
            //Note: this expression doesn't return nested direct-report employee references
            //return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);

            //Note: could make employees a static member of EmployeeDataSeeder
            // but I think doing that is a hack and not in the spirit of this exercise.
            //return EmployeeDataSeeder.Employees.SingleOrDefault(e => e.EmployeeId == id);            
            
            var reportingStructure = new ReportingStructure();
                        
            // Force EF to include our nested objects
            reportingStructure.Employee = 
                _employeeContext.Employees.Include(x => x.DirectReports).ThenInclude(x => x.DirectReports).SingleOrDefault(e => e.EmployeeId == id);

            // No matching record
            if (reportingStructure.Employee == null)
                return reportingStructure;

            if (reportingStructure.Employee.DirectReports != null)
                reportingStructure.NumberOfReports = CalculateNumberOfReports(reportingStructure.Employee, 0);

            // The other responses return just the root level so support that here to. 
            if (!includeDirectReports)
                reportingStructure.Employee.DirectReports = null;

            return reportingStructure;
        }

        /// <summary>
        /// Calculate the number of direct reports recursively. 
        /// </summary>
        /// <param name="employee">The root <see cref="Employee"/>.</param>
        /// <param name="count">The current count.</param>
        /// <returns>An <see cref="int"/> for the total number of direct reports.</returns>
        /// <exception cref="System.ArgumentNullException">The exception that is thrown when a null reference (Nothing in Visual Basic) is passed to a method that does not accept it as a valid argument.</exception>
        /// <exception cref="System.InvalidOperationException">The exception that is thrown when a method call is invalid for the object's current state.</exception>
        /// <exception cref="System.NullReferenceException">The exception that is thrown when there is an attempt to dereference a null object reference.</exception>
        protected internal virtual int CalculateNumberOfReports(Employee employee, int count)
        {
            if (employee.DirectReports == null)
                return count;

            for(int i = 0; i < employee.DirectReports.Count; i++)
            {
                if (employee.DirectReports == null)
                    continue;

                if (employee.DirectReports.Count == 0)
                    continue;

                count++;
                count = CalculateNumberOfReports(employee.DirectReports[i], count);                
            }

            return count;
        }

        #endregion
    }
}
