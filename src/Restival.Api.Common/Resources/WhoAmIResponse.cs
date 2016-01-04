using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using Restival.Data.Entities;

namespace Restival.Api.Common.Resources {

    [DataContract]
    public class ProfileResponse {

        public User User { get; set; }

        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "_links")]
        public dynamic Links {
            get {
                return new {
                    self = new { href = String.Format("/users/{0}/profiles/{1}", User.Id, Id) },
                };
            }
        }

    }
    [DataContract]
    public class ProfilesResponse {
        private List<ProfileResponse> profiles;

        public ProfilesResponse() { }

        public ProfilesResponse(User user) {
            profiles = user.Profiles.Select(p => new ProfileResponse() { Name = p.Name }).ToList();
        }

        [DataMember(Name = "links")]
        public dynamic Links {
            get { return (null); }
        }

        [DataMember(Name = "count")]
        public int Count {
            get { return profiles.Count; }
        }

        [DataMember(Name = "total")]
        public int Total {
            get { return 50; }
        }

        [DataMember(Name = "embedded")]
        public dynamic Embedded {
            get { return profiles; }
            set { profiles = value; }
        }
    }

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