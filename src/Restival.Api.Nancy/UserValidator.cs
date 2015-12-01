using Nancy.Authentication.Basic;
using Nancy.Security;
using Restival.Data;

namespace Restival.Api.Nancy {
    public class UserValidator : IUserValidator {
        private readonly IDataStore db;

        public UserValidator(IDataStore db) {
            this.db = db;
        }

        public IUserIdentity Validate(string username, string password) {
            var user = db.FindUserByUsername(username);
            if (user == null) return (null);
            return user.Password == password ? new UserIdentity() { UserName = username } : null;
        }
    }
}