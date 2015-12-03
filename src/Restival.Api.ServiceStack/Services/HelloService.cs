using Restival.Api.Common.Resources;
using Restival.Api.ServiceStack.Services.RequestDto;
using ServiceStack;
// using HelloResponse = Restival.Api.ServiceStack.Services.Response.HelloResponse;

namespace Restival.Api.ServiceStack.Services {
    public class HelloService : Service {
        public HelloResponse Any(Hello dto) {
            var greeting = new Greeting(dto.Name);
            var response = new HelloResponse() { Message = greeting.Message };
            return (response);
        }
    }
}