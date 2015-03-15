using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedmineDotNet.Models
{

    public class TrackerResponse
    {
        [JsonProperty(PropertyName = "trackers")]
        public List<Tracker> Trackers { get; set; }
    }
    
    public class Tracker
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
