using System;
using System.Collections.Generic;
using System.Linq;
using Restival.Data.Entities;

namespace Restival.Data {
    public class FakeDataStore : IDataStore {

        public User FindUserByUsername(string username) {
            return (Users.FirstOrDefault(user => user.Username == username));
        }

        public User GetUser(Guid userGuid) {
            return (Users.FirstOrDefault(user => user.Id == userGuid));
        }

        public static List<User> Users = new List<User> {
            new User("639E3C81-2398-E511-B599-005056C00008", "ali", "baba", "Alistair Baba"),
            new User("649E3C81-2398-E511-B599-005056C00008", "bob", "hope", "Robert Hope"),
            new User("659E3C81-2398-E511-B599-005056C00008", "cat", "flap", "Catherine Flap"),
            new User("669E3C81-2398-E511-B599-005056C00008", "dan", "dare", "Daniel Dare"),
            new User("679E3C81-2398-E511-B599-005056C00008", "eli", "roth", "Elijay Roth"),
            new User("689E3C81-2398-E511-B599-005056C00008", "fox", "trot", "Foxworth Trotsky")
        };
    }
}