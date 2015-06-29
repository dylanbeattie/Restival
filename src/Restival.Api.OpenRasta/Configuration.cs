using OpenRasta.Configuration;
using OpenRasta.DI;
using Restival.Api.Common.Resources;
using Restival.Api.OpenRasta.Handlers;
using Restival.Data;

namespace Restival.Api.OpenRasta {
    public class Configuration : IConfigurationSource {
        public void Configure() {
            using (OpenRastaConfiguration.Manual) {

                ResourceSpace.Uses.CustomDependency<IProfileDatabase, FakeProfileDatabase>(DependencyLifetime.Singleton);

                ResourceSpace.Has.ResourcesOfType<Greeting>()
                    .AtUri("/hello")
                    .And.AtUri("/hello/{name}")
                    .HandledBy<HelloHandler>()
                    .AsJsonDataContract();

                ResourceSpace.Has.ResourcesOfType<ProfileListResponse>()
                    .AtUri("/profiles")
                    .HandledBy<ProfileListHandler>()
                    .AsJsonDataContract();
            }
        }
    }
}