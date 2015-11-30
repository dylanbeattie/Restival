using System.Collections.Generic;

namespace Restival.Api.Common.Resources {
    public class ProfileResponse {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Link {
        public string Name { get; set; }
        public string Href { get; set; }
        public Link(string name, string href) {
            Name = name;
            Href = href;
        }
    }

    // ReSharper disable InconsistentNaming
    // ReSharper restore InconsistentNaming
}