using System.Collections.Generic;
using Restival.Data.Entities;

namespace Restival.Data {
    public interface IProfileDatabase {
        Profile SelectProfile(string profileId);
        IEnumerable<Profile> ListProfiles();
    }
}