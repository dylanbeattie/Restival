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

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "_links")]
        public dynamic Links {
            get { return(new { self = new { href = String.Format("/users/{0}/profiles/{1}", User.Id, Name) } }); }
        }

    }
    [DataContract]
    public class ProfilesResponse {
        private IList<ProfileResponse> profiles = new List<ProfileResponse>();

        public IList<ProfileResponse> Profiles {
            get { return (profiles); }
        }

        public ProfilesResponse() { }

        public ProfilesResponse(User user) {
            profiles = user.Profiles.Select(p => new ProfileResponse() { Name = p.Name, User = user }).ToList();
        }

        [DataMember(Name = "links")]
        public dynamic Links { get; set; }

        [DataMember(Name = "count")]
        public int Count {
            get { return profiles.Count; }
        }

        [DataMember(Name = "total")]
        public int Total {
            get { return profiles.Count; }
        }

        [DataMember(Name = "index")]
        public int Index {
            get { return 0; }
        }

        [DataMember(Name = "_embedded")]
        public dynamic Embedded {
            get { return new { profiles }; }
            // set { profiles = value.profiles; }
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