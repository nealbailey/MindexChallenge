//-----------------------------------------------------------------------
// Source File:  "IReportingStructureRepository.cs"
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
    using challenge.Models;

    /// <summary>
    /// Interface for ReportingStructure abstraction.
    /// </summary>
    public interface IReportingStructureRepository
    {
        /// <summary>
        /// Fetch the <see cref="ReportingStructure"/> based on the <see cref="Employee"/> id value.
        /// </summary>
        /// <param name="id">The <see cref="Employee"/>Id Guid string.</param>
        /// <param name="includeDirectReports">Flag to include or exclude nest direct report <see cref="Employee"/> references.</param>
        /// <returns>The matched <see cref="ReportingStructure"/> or an empty instance if none was found.</returns>
        ReportingStructure GetReportingStructure(string id, bool includeDirectReports = true);
    }
}
