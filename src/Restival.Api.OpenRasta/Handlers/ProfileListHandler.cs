using System.Linq;
using Restival.Api.Common;
using Restival.Api.Common.Resources;
using Restival.Data;

namespace Restival.Api.OpenRasta.Handlers {
    public class ProfileListHandler {
        private readonly IProfileDatabase db;

        public ProfileListHandler(IProfileDatabase db) {
            this.db = db;
        }

        public object Get() {
            var profiles = db.ListProfiles();
            var result = new ProfileListResponse { Items = profiles.Select(p => ProfileExtensions.ToProfileResponse(p)).ToList() };
            return (result);
        }
    }
}