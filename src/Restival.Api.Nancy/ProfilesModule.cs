using Nancy;
using Nancy.Security;
using Restival.Api.Common.Resources;
using Restival.Data;

namespace Restival.Api.Nancy {
    public class ProfilesModule : NancyModule {
        public ProfilesModule(IDataStore db) {
            this.RequiresAuthentication();
            Get["/users/{id}/profiles"] = parameters => {
                var user = db.FindUserByUsername(this.Context.CurrentUser.UserName);
                if (user.Id == parameters.id) return new ProfilesResponse(user);
                return HttpStatusCode.Forbidden;
            };
        }
    }
}