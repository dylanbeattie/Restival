using System;
using OpenRasta.Security;
using OpenRasta.Web;
using Restival.Api.Common.Resources;
using Restival.Data;

namespace Restival.Api.OpenRasta.Handlers {
    [RequiresAuthentication]
    public class WhoAmIHandler {
        private readonly IDataStore db;
        private readonly ICommunicationContext context;

        public WhoAmIHandler(IDataStore db, ICommunicationContext context) {
            this.db = db;
            this.context = context;
        }

        public object Get() {
            var userRecord = db.FindUserByUsername(context.User.Identity.Name);
            return (new WhoAmIResponse(userRecord.Id, userRecord.Username, userRecord.Name));
        }
    }
}