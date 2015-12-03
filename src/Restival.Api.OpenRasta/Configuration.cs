using OpenRasta.Configuration;
using Restival.Api.Common.Resources;
using Restival.Api.OpenRasta.Handlers;

namespace Restival.Api.OpenRasta {
    public class Configuration : IConfigurationSource {
        public void Configure() {
            using (OpenRastaConfiguration.Manual) {
                ResourceSpace.Has.ResourcesOfType<Greeting>()
                    .AtUri("/hello")
                    .And.AtUri("/hello/{name}")
                    .HandledBy<HelloHandler>()
                    .AsJsonDataContract();
            }
        }
    }
}