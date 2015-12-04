using System;
using Restival.Api.Common.Resources;
using Restival.Api.ServiceStack.Services.RequestDto;
using Restival.Data;
using ServiceStack;

namespace Restival.Api.ServiceStack.Services {
    [Authenticate]
    public class WhoAmIService : Service {
        private readonly IDataStore db;

        public WhoAmIService(IDataStore db) {
            this.db = db;
        }

        public WhoAmIResponse Get(WhoAmI dto) {
            var session = GetSession();
            var username = session.UserAuthName;
            var user = db.FindUserByUsername(username);
            return (new WhoAmIResponse(user.Id, user.Username, user.Name));
        }
    }
}