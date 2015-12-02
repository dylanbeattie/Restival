using OpenRasta.Security;
using Restival.Data;

namespace Restival.Api.OpenRasta {
    public class AuthenticationProvider : IAuthenticationProvider {
        private readonly IDataStore db;

        public AuthenticationProvider(IDataStore db) {
            this.db = db;
        }

        public Credentials GetByUsername(string username) {
            var user = db.FindUserByUsername(username);
            if (user == null) return (null);
            return (new Credentials() {
                Username = user.Username,
                Password = user.Password,
                Roles = new string[] { }
            });
        }

        public bool ValidatePassword(Credentials credentials, string suppliedPassword) {
            return (credentials.Password == suppliedPassword);
        }
    }
}