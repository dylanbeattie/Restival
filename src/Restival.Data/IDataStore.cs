using System;
using System.Collections;
using System.Collections.Generic;
using Restival.Data.Entities;

namespace Restival.Data {
    public interface IDataStore {
        User GetUser(Guid userGuid);
        User FindUserByUsername(string username);
        IList<Profile> ListProfilesForUser(Guid userGuid);
    }
}