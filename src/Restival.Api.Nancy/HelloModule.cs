using System.Runtime.InteropServices;
using Nancy;
using Restival.Api.Common.Resources;

namespace Restival.Api.Nancy {
    public class HelloModule : NancyModule {
        public HelloModule() {

            Get["/hello"] = _ => new Greeting("World");

            Get["/hello/{name}"] = parameters => new Greeting(parameters.name);

        }
    }

    public class WhoAmIModule : NancyModule {
        public WhoAmIModule() {
            Get["/whoami"] = _ => new WhoAmIResponse(12345, "username", "Test User");


        }
    }
}