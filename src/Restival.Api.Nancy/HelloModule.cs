using Nancy;
using Restival.Api.Common.Resources;

namespace Restival.Api.Nancy {
    public class HelloModule : NancyModule {
        public HelloModule() {
            Get["/hello"] = _ => new HelloResponse { Message = new Greeting("World").Message };
            Get["/hello/{name}"] = parameters => new HelloResponse { Message = new Greeting(parameters.name).Message };
        }
    }
}