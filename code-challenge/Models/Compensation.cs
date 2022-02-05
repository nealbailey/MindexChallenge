//-----------------------------------------------------------------------
// Source File:  "Compensation.cs"
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
namespace challenge.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Compensation Abstraction
    /// </summary>
    public class Compensation
    {
        /// <summary>
        /// UniqueIdenditifer needed for EF
        /// </summary>
        [Key]
        public string Id { get; set; }
        
        /// <summary>
        /// The Employee Id
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// Employee Annual Salary
        /// </summary>
        public virtual double Salary { get; set; }

        /// <summary>
        /// Employee Starting Date
        /// </summary>
        public virtual DateTime EffectiveDate { get; set; }

    }
}
