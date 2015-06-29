using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Restival.Data.Entities;

namespace Restival.Data {
    public class FakeProfileDatabase : IProfileDatabase {
        private readonly List<Profile> profiles = new List<Profile>();

        private const int FAKE_PROFILE_COUNT = 100;
        public FakeProfileDatabase() {
            for (var i = 1; i <= FAKE_PROFILE_COUNT; i++) profiles.Add(MakeProfile(i));
        }

        private static Profile MakeProfile(int seed) {
            var forename = FakeData.Forenames[(seed * 4791) % FakeData.Forenames.Length];
            var surname = FakeData.Surnames[(seed * 7125) % FakeData.Surnames.Length];
            var name = String.Format("{0} {1}", forename, surname);
            var id = (forename + surname + seed).ToLowerInvariant();
            return (new Profile(id, name));
        }

        public Profile SelectProfile(string profileId) {
            return (profiles.FirstOrDefault(p => p.Id == profileId));
        }

        public IEnumerable<Profile> ListProfiles() {
            return (profiles);
        }
    }
}


