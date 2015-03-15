using RedmineDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmineDotNet.CommandLine.Models
{
    public class IssueSummary
    {
        public Issues Issues { get; set; }
        public IEnumerable<IGrouping<string, Issue>> GroupedIssues { get; set; }
        public int AverageAge { get; set; }

        public IssueSummary(Issues issues)
        {
            Issues = issues;
            GroupedIssues = issues.AllIssues.GroupBy(i => i.Priority.Name).OrderByDescending(grp => grp.Count());

            if (Issues.TotalCount > 0)
            {
                AverageAge = Convert.ToInt32(issues.AllIssues.Select(i => (DateTime.Now - i.CreatedOn).TotalDays).Average());
            }
            else
            {
                AverageAge = 0;
            }
            
        }
    }
}
