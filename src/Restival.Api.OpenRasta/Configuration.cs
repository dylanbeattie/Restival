using System.Diagnostics;
using OpenRasta.Authentication;
using OpenRasta.Authentication.Basic;
using OpenRasta.Authentication.Digest;
using OpenRasta.Configuration;
using OpenRasta.DI;
using OpenRasta.Pipeline;
using OpenRasta.Pipeline.Contributors;
using OpenRasta.Security;
using Restival.Api.Common.Resources;
using Restival.Api.OpenRasta.Handlers;
using Restival.Data;

namespace Restival.Api.OpenRasta {
    public class Configuration : IConfigurationSource {
        public void Configure() {
            using (OpenRastaConfiguration.Manual) {

                ResourceSpace.Uses.CustomDependency<IAuthenticationScheme, BasicAuthenticationScheme>(DependencyLifetime.Singleton);

                // register your basic authenticator in the DI resolver
                ResourceSpace.Uses.CustomDependency<IBasicAuthenticator, MyBasicAuthenticator>(DependencyLifetime.Transient);

                ResourceSpace.Uses.CustomDependency<IDataStore, FakeDataStore>(DependencyLifetime.Singleton);
                ResourceSpace.Uses.CustomDependency<IAuthenticationProvider, AuthenticationProvider>(DependencyLifetime.Singleton);
                ResourceSpace.Uses.PipelineContributor<BasicAuthorizerContributor>();

                ResourceSpace.Has.ResourcesOfType<Greeting>()
                    .AtUri("/hello")
                    .And.AtUri("/hello/{name}")
                    .HandledBy<HelloHandler>()
                    .AsJsonDataContract();

                //ResourceSpace.Has.ResourcesOfType<ProfileListResponse>()
                //    .AtUri("/profiles")
                //    .HandledBy<ProfileListHandler>()
                //    .AsJsonDataContract();

                ResourceSpace.Has.ResourcesOfType<WhoAmIResponse>()
                    .AtUri("/whoami")
                    .HandledBy<WhoAmIHandler>()
                    .TranscodedBy<JsonCodec>();
            }
        }
    }
}
