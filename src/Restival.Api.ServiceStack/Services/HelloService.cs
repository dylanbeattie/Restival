using Restival.Api.Common.Resources;
using ServiceStack;

namespace Restival.Api.ServiceStack.Services {
    public class HelloService : Service {
        public HelloResponse Any(Hello dto) {
            var greeting = new Greeting(dto.Name);
            var response = new HelloResponse() { Message = greeting.Message };
            return (response);
        }
    }
}