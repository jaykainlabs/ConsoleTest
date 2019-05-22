using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace KainLabs.Siren
{
    public class LinkedSubEntity : SubEntity
    {
        public LinkedSubEntity()
        {
        }

        public string Title { get; set; }

        public Uri Href { get; set; }
        public IEnumerable<string> Rel { get; set; }
        public IEnumerable<string> Class { get; set; }
        public string Type { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> ExtensionData { get; } = new Dictionary<string, object>();
    }
}
