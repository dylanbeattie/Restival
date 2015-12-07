using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.Serialization;

namespace Restival.Api.Common.Resources {
    [DataContract]
    public class WhoAmIResponse {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public WhoAmIResponse() {}

        public WhoAmIResponse(Guid id, string username, string name) {
            this.Id = id;
            this.Username = username;
            this.Name = name;
        }

        [DataMember(Name = "links")]
        public dynamic _links
        {
            get { return new { self = "me!me!me!" }; }
        }

        [DataMember(Name = "ObjectFoo")]
        public object Foo
        {
            get { return (new object()); }
        }

        [DataMember(Name = "_underscored")]
        public object Underscored
        {
            get { return ("stringy!"); }
        }
    }
}