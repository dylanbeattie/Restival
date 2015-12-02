using System;
using OpenRasta.Security;
using Restival.Api.Common.Resources;

namespace Restival.Api.OpenRasta.Handlers {
    [RequiresAuthentication]
    public class WhoAmIHandler {
        public object Get() {
            return (new WhoAmIResponse(Guid.NewGuid(), "username", "Test User"));
        }
    }
}