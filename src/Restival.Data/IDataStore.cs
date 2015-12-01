using System;
using Restival.Data.Entities;

namespace Restival.Data {
    public interface IDataStore {
        User GetUser(Guid userGuid);
        User FindUserByUsername(string username);
    }
}