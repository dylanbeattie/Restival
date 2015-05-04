using Nancy;
using Restival.Api.Common.Resources;

namespace Restival.Api.Nancy {
    public class HelloModule : NancyModule {
        public HelloModule() {
            Get["/hello"] = _ => new Greeting();
            Get["/hello/{name}"] = parameters => new Greeting(parameters.name);
        }
    }
}