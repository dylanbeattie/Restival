using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Restival.Api.Common;
using Restival.Api.Common.Resources;
using Restival.Api.WebApi.Security;
using Restival.Data;

namespace Restival.Api.WebApi.Controllers {
    [RequireHttpBasicAuthorization("Restival.WebAPI")]
    public class WhoAmIController : ApiController {
        private readonly IDataStore db;

        public WhoAmIController(IDataStore db) {
            this.db = db;
        }

        [Route("whoami")]
        public WhoAmIResponse Get() {
            var user = db.FindUserByUsername(User.Identity.Name);
            var response = new WhoAmIResponse() {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name
            };
            return (response);
        }
    }

    [RequireHttpBasicAuthorization("Restival.WebAPI")]
    public class ProfilesController : ApiController {
        private readonly IDataStore db;

        public ProfilesController(IDataStore db) {
            this.db = db;
        }

        [Route("users/{userid}/profiles")]
        public object Get(Guid userId) {
            var user = db.FindUserByUsername(User.Identity.Name);
            if (user.Id == userId) return (new ProfilesResponse(user));
            return (this.Request.CreateErrorResponse(HttpStatusCode.Forbidden, Messages.YouDoNotHavePermissionToViewThoseProfiles));
        }
    }
}