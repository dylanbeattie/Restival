using ServiceStack;

namespace Restival.Api.ServiceStack.Services {
    [Route("/hello")]
    [Route("/hello/{name}")]
    public class Hello {
        private string name = "World";
        public string Name {
            get { return name; }
            set { name = value; }
        }
    }
}