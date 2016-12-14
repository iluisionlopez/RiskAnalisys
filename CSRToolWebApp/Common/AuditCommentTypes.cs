using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSRToolWebApp.Common
{
    public enum AuditCommentTypes
    {
        [EnumDisplayName(DisplayName = "Refused the Audit")]
        Refused = 0,
        [EnumDisplayName(DisplayName = "More than three major deviations")]
        ThreeMajorDeviations = 2,
        [EnumDisplayName(DisplayName = "Major deviations and performance clearly below country and industry average")]
        MajorDeviationsAndPerformance = 3,        
        [EnumDisplayName(DisplayName = "No major deviations and performance in line with country and industry average")]
        NoMajorDeviationsAndPerformance = 7,
        [EnumDisplayName(DisplayName = "Performance significantly over average for industry and country, good example")]
        PerformanceOverAverage = 10,
        [EnumDisplayName(DisplayName = "No audit has been performede")]
        NoAuditPerformed = 5
    }
}