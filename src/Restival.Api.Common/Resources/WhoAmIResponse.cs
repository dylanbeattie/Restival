using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.Serialization;

namespace Restival.Api.Common.Resources {
    [DataContract]
    public class WhoAmIResponse : Resource {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public WhoAmIResponse(Guid id, string username, string name) {
            this.Id = id;
            this.Username = username;
            this.Name = name;
        }

        public WhoAmIResponse() { }

        protected override void AddLinks(Dictionary<string, Dictionary<string, string>> linkset) {
            linkset.Add("agencies", new Dictionary<string, string> { { "href", String.Format("/users/{0}/agencies", Id) } });
        }

        protected override string SelfUrl {
            get { return (String.Format("/users/{0}", this.Id)); }
        }
    }
}