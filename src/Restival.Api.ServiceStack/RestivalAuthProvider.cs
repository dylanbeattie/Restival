using Restival.Data;
using ServiceStack;
using ServiceStack.Auth;

namespace Restival.Api.ServiceStack {
    public class RestivalAuthProvider : BasicAuthProvider {
        private readonly IDataStore db;

        public RestivalAuthProvider(IDataStore db) {
            this.db = db;
        }

        public override bool TryAuthenticate(IServiceBase authService, string userName, string password) {
            var user = db.FindUserByUsername(userName);
            return user != null && user.Password == password;
        }
    }
}
