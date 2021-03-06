﻿using System;
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

        public WhoAmIResponse() { }

        public WhoAmIResponse(Guid id, string username, string name) {
            this.Id = id;
            this.Username = username;
            this.Name = name;
        }

        [DataMember(Name = "_links")]
        public dynamic Links {
            get {
                return new {
                    profiles = new { href = String.Format("/users/{0}/profiles", Id) },
                };
            }
        }
    }
}