using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmineDotNet.CommandLine
{
    public class IssueStatusReport
    {
        public IssueSummary OpenIssues { get; set; }
        public IssueSummary NewIssues { get; set; }
        public IssueSummary UpdatedIssues { get; set; }
        public IssueSummary ClosedIssues { get; set; }
    }
}
