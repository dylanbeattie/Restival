using OpenRasta.Authentication;
using OpenRasta.Authentication.Basic;
using Restival.Data;

#pragma warning disable 618

namespace Restival.Api.OpenRasta.Security {
    public class RestivalAuthenticator : IBasicAuthenticator {
        private readonly IDataStore db;

        public RestivalAuthenticator(IDataStore db) {
            this.db = db;
        }

        public AuthenticationResult Authenticate(BasicAuthRequestHeader header) {
            var user = db.FindUserByUsername(header.Username);
            if (user != null && user.Password == header.Password) return (new AuthenticationResult.Success(user.Username, new string[] { }));
            return (new AuthenticationResult.Failed());
        }

        public string Realm { get { return ("Restival.OpenRasta"); } }
    }
}

#pragma warning restore 618
