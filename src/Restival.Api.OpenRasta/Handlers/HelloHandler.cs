using System;
using Restival.Api.Common.Resources;

namespace Restival.Api.OpenRasta.Handlers {
    public class HelloHandler {
        public object Get(string name = "World") {
            return (new Greeting(name));
        }
    }

    public class WhoAmIHandler {
        public object Get() {
            return (new WhoAmIResponse(Guid.NewGuid(), "username", "Test User"));
        }
    }
}