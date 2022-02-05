//-----------------------------------------------------------------------
// Source File:  "ReportingStructure.cs"
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
namespace challenge.Models
{
    /// <summary>
    /// ReportingStructure DataContract
    /// </summary>
    public class ReportingStructure
    {
        /// <summary>
        /// The <see cref="Employee"/>.
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// The <see cref="int"/> number of direct report <see cref="Employee"/> objects assigned.
        /// </summary>
        public int NumberOfReports { get; set; }
    }
}
