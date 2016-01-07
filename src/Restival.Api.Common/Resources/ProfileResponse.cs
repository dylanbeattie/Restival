using System;
using System.Runtime.Serialization;
using Restival.Data.Entities;

namespace Restival.Api.Common.Resources {
    [DataContract]
    public class ProfileResponse {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "_links")]
        public dynamic Links {
            get { return (new { self = new { href = String.Format("/profiles/{0}", Name) } }); }
        }
    }
}
