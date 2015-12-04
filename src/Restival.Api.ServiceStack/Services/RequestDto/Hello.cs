using ServiceStack;

namespace Restival.Api.ServiceStack.Services.RequestDto {
    [Route("/hello")]
    [Route("/hello/{name}")]
    public class Hello {
        private string name = "World";
        public string Name {
            get { return name; }
            set { name = value; }
        }
    }

    [Route("/whoami")]
    public class WhoAmI { }
}