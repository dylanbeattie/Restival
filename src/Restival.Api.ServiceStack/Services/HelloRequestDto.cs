using ServiceStack;

namespace Restival.Api.ServiceStack.Services {
    [Route("/hello")]
    [Route("/hello/{name}")]
    public class HelloRequestDto {
        public string Name { get; set; }
    }
}