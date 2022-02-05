//-----------------------------------------------------------------------
// Source File:  "ReportingStructureService.cs"
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
namespace challenge.Services
{
    using challenge.Models;
    using challenge.Repositories;
    using Microsoft.Extensions.Logging;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Service for interfacing with the ReportingStructure repo.
    /// </summary>
    public class ReportingStructureService : IReportingStructureService
    {
        #region Fields
        private readonly IReportingStructureRepository _reportingStructureRepository;
        private readonly ILogger<ReportingStructureService> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">Generic Microsoft Logger.</param>
        /// <param name="reportingStructureRepository">Data Repository.</param>
        public ReportingStructureService(
            ILogger<ReportingStructureService> logger,
            IReportingStructureRepository reportingStructureRepository)
        {
            _logger = logger;
            _reportingStructureRepository = reportingStructureRepository;            
        }
        #endregion

        #region Methods

        /// <summary>
        /// Fetch the <see cref="ReportingStructure"/> based on the <see cref="Employee"/> id value.
        /// </summary>
        /// <param name="id">The <see cref="Employee"/>Id Guid string.</param>
        /// <param name="includeDirectReports">Flag to include or exclude nest direct report <see cref="Employee"/> references.</param>
        /// <returns>The matched <see cref="ReportingStructure"/> or an empty instance if none was found.</re
        public ReportingStructure GetById(string id, bool includeDirectReports = true)
        {
            var reportingStructure = new ReportingStructure();

            // Ensure we actually got a GUID before hitting the data-store
            var pattern = "^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$";
            if (!Regex.IsMatch(id, pattern, RegexOptions.IgnoreCase))
                return reportingStructure;

            if (!string.IsNullOrEmpty(id))
                reportingStructure = _reportingStructureRepository.GetReportingStructure(id, includeDirectReports);                

            return reportingStructure;
        }

        #endregion
    }
}
