using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Restival.Data.Entities;

namespace Restival.Api.Common.Resources {
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
}
