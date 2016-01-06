using System;
using ServiceStack;

namespace Restival.Api.ServiceStack.Services.RequestDto {
    [Route("/whoami")]
    public class WhoAmI { }

    [Route("/users/{userid}/profiles")]
    public class Profiles {
        public Guid UserId { get; set; }
    }
}