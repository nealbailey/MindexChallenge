//-----------------------------------------------------------------------
// Source File:  "ReportingStructureController.cs"
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
namespace challenge.Controllers
{
    using challenge.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/ReportingStructure")]
    public class ReportingStructureController : Controller
    {
        #region Fields
        private readonly ILogger _logger;
        private readonly IReportingStructureService _reportingStructureService;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">Generic Microsoft Logger.</param>
        /// <param name="reportingStructureService">Service Dependency.</param>
        public ReportingStructureController(
            ILogger<ReportingStructureController> logger, 
            IReportingStructureService reportingStructureService)
        {
            _logger = logger;
            _reportingStructureService = reportingStructureService;
        }

        #endregion

        #region REST Http Endpoints

        /// <summary>
        /// Fetch <see cref="Models.ReportingStructure"/> instance.
        /// </summary>
        /// <param name="id">RESTful. The <see cref="Models.Employee"/> id.</param>
        /// <param name="includeDirectReports">QueryString. Optional flag to indicate if the response should include nested <see cref="Models.Employee"/> references.</param>
        /// <returns>An <see cref="StatusCodeResult"/> as well as the <see cref="Models.ReportingStructure"/></returns>
        [HttpGet("{id}", Name = "getReportingStructureByEmployeeId")]
        public IActionResult GetReportingStructureByEmployeeId(string id, bool includeDirectReports = true)
        {
            _logger.LogDebug($"Received ReportingStructure get request for '{id}'. IncludeDirectReports = '{includeDirectReports}'.");
            
            // Fetch record
            var reportingStruct = _reportingStructureService.GetById(id, includeDirectReports);

            if (reportingStruct.Employee == null)
                return NotFound();

            // Return serialized response
            return Ok(reportingStruct);
        }

        #endregion
    }
}
