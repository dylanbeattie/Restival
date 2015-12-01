using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Restival.Api.Common.Resources {
    public abstract class Resource {

        protected Dictionary<string, Dictionary<string, string>> links;

        [DataMember(Name = "_links")]
        public Dictionary<string, Dictionary<string, string>> Links {
            get {
                if (links != null) return (links);
                links = new Dictionary<string, Dictionary<string, string>> {
                    { "self", new Dictionary<string, string> { { "href", SelfUrl } } }
                };
                AddLinks(links);
                return (links);
            }
            set { links = value; }
        }

        protected abstract void AddLinks(Dictionary<string, Dictionary<string, string>> linkset);
        protected abstract string SelfUrl { get; }
    }
}