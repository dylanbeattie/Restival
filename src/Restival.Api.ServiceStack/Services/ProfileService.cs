using System.Linq;
using Restival.Api.Common;
using Restival.Api.Common.Resources;
using Restival.Api.ServiceStack.Services.RequestDto;
using Restival.Data;
using ServiceStack;

namespace Restival.Api.ServiceStack.Services {
    public class ProfileService : Service {
        private readonly IProfileDatabase db;

        public ProfileService(IProfileDatabase db) {
            this.db = db;
        }

        public ProfileListResponse Get(ProfileList items) {
            var profiles = db.ListProfiles();
            var result = new ProfileListResponse() {
                Items = profiles.Select(p => p.ToProfileResponse()).ToList()
            };
            return (result);
        }
    }
}