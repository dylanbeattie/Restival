using System;
using System.Runtime.Serialization;

namespace Restival.Api.Common.Resources {
    [DataContract]
    public class WhoAmIResponse {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        private dynamic links;

        [DataMember(Name = "_links")]
        public dynamic Links {
            get {
                return (links ?? new {
                    self = new { href = String.Format("/users/{0}", Id) },
                    agencies = new { href = String.Format("/users/{0}/agencies", Id) }
                });
            }
            set { links = value; }
        }

        public WhoAmIResponse(int id, string username, string name) {
            this.Id = id;
            this.Username = username;
            this.Name = name;
        }

        public WhoAmIResponse() { }
    }
}