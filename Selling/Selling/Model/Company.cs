using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selling.Model
{
    public class Company
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("FechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }
    }
}
