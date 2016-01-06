using System;
using OpenRasta.Pipeline;
using OpenRasta.Security;
using OpenRasta.Web;
using Restival.Api.Common;
using Restival.Api.Common.Resources;
using Restival.Data;

namespace Restival.Api.OpenRasta.Handlers {
    [RequiresAuthentication]
    public class ProfilesHandler {
        private readonly IDataStore db;
        private readonly ICommunicationContext context;

        public ProfilesHandler(IDataStore db, ICommunicationContext context) {
            this.db = db;
            this.context = context;
        }

        public object Get(Guid id) {
            var userRecord = db.FindUserByUsername(context.User.Identity.Name);
            if (userRecord.Id == id) return new ProfilesResponse(userRecord);
            return new OperationResult.Forbidden() { Description = Messages.YouDoNotHavePermissionToViewThoseProfiles };

        }
    }
}