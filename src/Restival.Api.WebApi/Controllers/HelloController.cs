using System.Linq;
using System.Web.Http;
using Restival.Api.Common;
using Restival.Api.Common.Resources;
using Restival.Data;

namespace Restival.Api.WebApi.Controllers {
    public class HelloController : ApiController {
        [Route("hello/{name=World}")]
        public HelloResponse Get(string name) {
            return (new HelloResponse { Message = new Greeting(name).Message });
        }
    }

    public class ProfileListController : ApiController {
        // TODO: wire up IOC/dependency injection for WebAPI implementation
        // for now we're using a "dirty singleton" approach.
        private static IProfileDatabase db = new FakeProfileDatabase();

        [Route("profiles")]
        public ProfileListResponse Get() {
            var profiles = db.ListProfiles();
            var result = new ProfileListResponse() { Items = profiles.Select(p => p.ToProfileResponse()).ToList() };
            return (result);
        }
    }
}