using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmineDotNet.Models
{

    public class IssueStatuses
    {
        [JsonProperty(PropertyName = "issue_statuses")]
        public List<IssueStatus> Statuses { get; set; }
    }

    public class IssueStatus
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "is_default")]
        public bool IsDefault { get; set; }

        [JsonProperty(PropertyName = "is_closed")]
        public bool IsClosed { get; set; }
    }
}
