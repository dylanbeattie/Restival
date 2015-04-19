using System.Web.Http;
using Restival.Api.Common;
using Restival.Api.Common.Entities;

namespace Restival.Api.WebApi.Controllers {
    public class HelloController : ApiController {
        public Greeting Get() {
            return (new Greeting());
        }

        public Greeting Get(string name) {
            return (new Greeting(name));
        }
    }
}