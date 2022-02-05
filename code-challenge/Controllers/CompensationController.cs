//-----------------------------------------------------------------------
// Source File:  "CompensationController.cs"
// Create Date:  02/04/2022 07:27:00 PM
// Last Updated: 02/04/2022 07:27:00 PM
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
namespace challenge.Controllers
{
    using challenge.Models;
    using challenge.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/Compensation")]
    public class CompensationController : Controller
    {
        #region Fields
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">Generic Microsoft Logger.</param>
        /// <param name="employeeService">Service Dependency.</param>
        public CompensationController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        #endregion

        #region REST Http Endpoints

        /// <summary>
        /// Fetch <see cref="Models.Employee"/> instance which includes <see cref="Compensation"/> info.
        /// </summary>
        /// <param name="id">RESTful. The <see cref="Models.Employee"/> id.</param>
        /// <returns>An <see cref="StatusCodeResult"/> as well as the <see cref="Models.Employee"/></returns>
        [HttpGet("{id}", Name = "getCompensationByEmployeeById")]
        public IActionResult GetCompensationByEmployeeById(string id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var employee = _employeeService.GetById(id, true);

            if (employee == null)
                return NotFound();

            return Ok(employee.Compensation);
        }

        /// <summary>
        /// Update the <see cref="Models.Employee"/> <see cref="Compensation"/> instance info.
        /// </summary>
        /// <param name="id">RESTful. The <see cref="Models.Employee"/> id.</param>
        /// <param name="compensation">ReqBody. The new <see cref="Compensation"/> values.</param>
        /// <returns></returns>
        [HttpPatch("{id}", Name ="updateComprensationByEmployeeId")]
        public IActionResult UpdateEmployeeCompensation(string id, [FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Recieved employee update/patch request for '{id}'");

            // Must provide the employee id 
            if (string.IsNullOrEmpty(id))
                return NotFound();

            // Request did not include EmployeeId in body (use url)
            if (string.IsNullOrEmpty(compensation.EmployeeId))
                compensation.EmployeeId = id;

            // The EmployeeId sent on the url doesn't match the one
            // included in the request body.
            if (compensation.EmployeeId != id)
                return NotFound();

            var existingEmployee = _employeeService.GetById(id, true);

            // No matching employee in database
            if (existingEmployee == null)
                return NotFound();

            //Let's do a HttpPatch instead of an HttpPut. Doesn't really
            //make sense to overwrite the entire employee object just to
            //add compensation to it. 
            //
            //var modEmployee = (Employee)existingEmployee.Clone();
            //modEmployee.Compensation = compensation;

            existingEmployee.Compensation = compensation;
            _employeeService.Update(existingEmployee);

            return Ok(existingEmployee);
        }

        #endregion
    }
}
