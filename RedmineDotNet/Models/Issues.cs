using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmineDotNet.Models
{
    public class Issues
    {
        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount { get; set; }

        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

        [JsonProperty(PropertyName = "issues")]
        public List<Issue> AllIssues { get; set; }
    }

    public class Issue
    {
        [JsonProperty(PropertyName = "status")]
        public Reference Status { get; set; }

        [JsonProperty(PropertyName = "project")]
        public Reference Project { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "author")]
        public Reference Author { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public Reference Priority { get; set; }

        [JsonProperty(PropertyName = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "tracker")]
        public Reference Tracker { get; set; }

        [JsonProperty(PropertyName = "custom_fields")]
        public List<CustomField> CustomFields { get; set; }

        [JsonProperty(PropertyName = "updated_on")]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "done_ratio")]
        public int DoneRatio { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }
    }

    public class Reference
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
    }

    public class CustomField
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "value")]
        public Object Value { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "multiple")]
        public bool? Multiple { get; set; }
    }
    
}
