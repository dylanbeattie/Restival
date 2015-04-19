using System.Collections.Generic;
using System.Web.Http;

namespace Restival.Api.WebApi.Controllers {
    public class RootController : ApiController {
        // GET /
        public Dictionary<string, string> Get() {
            return (new Dictionary<string, string> {
                { "status_url" , "/status" }
            });
        }
    }
}