using System;
using System.Collections.Generic;
using System.Linq;
using Restival.Data.Entities;

namespace Restival.Data {
    public class FakeDataStore : IDataStore {

        public User FindUserByUsername(string username) {
            return (Users.FirstOrDefault(user => user.Username == username));
        }

        public IList<Profile> ListProfilesForUser(Guid userGuid) {
            var user = GetUser(userGuid);
            return user == null ? (null) : (user.Profiles.ToList().AsReadOnly());
        }

        public User GetUser(Guid userGuid) {
            return (Users.FirstOrDefault(user => user.Id == userGuid));
        }

        public static List<User> Users = new List<User> {
            new User("639E3C81-2398-E511-B599-005056C00008", "ali", "baba", "Alistair Baba") { Profiles = new List<Profile>(MakeProfiles("ali", 0)) },
            new User("649E3C81-2398-E511-B599-005056C00008", "bob", "hope", "Robert Hope")  { Profiles = new List<Profile>(MakeProfiles("bob", 1)) },
            new User("659E3C81-2398-E511-B599-005056C00008", "cat", "flap", "Catherine Flap")  { Profiles = new List<Profile>(MakeProfiles("cat", 2)) },
            new User("669E3C81-2398-E511-B599-005056C00008", "dan", "dare", "Daniel Dare") { Profiles = new List<Profile>(MakeProfiles("dan", 3)) },
            new User("679E3C81-2398-E511-B599-005056C00008", "eli", "roth", "Elijay Roth") { Profiles = new List<Profile>(MakeProfiles("eli", 4)) },
            new User("689E3C81-2398-E511-B599-005056C00008", "fox", "trot", "Foxworth Trotsky") { Profiles = new List<Profile>(MakeProfiles("fox", 5)) }
        };

        public static IEnumerable<Profile> MakeProfiles(string name, int count) {
            for (var i = 0; i < count; i++) yield return (new Profile(String.Format("{0}{1:0000}", name, i)));
        }
    }
}