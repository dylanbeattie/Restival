using OpenRasta;
using OpenRasta.Authentication;
using OpenRasta.Authentication.Basic;
using OpenRasta.Configuration;
using OpenRasta.DI;
using OpenRasta.Pipeline.Contributors;
using OpenRasta.Web;
using Restival.Api.Common.Resources;
using Restival.Api.OpenRasta.Handlers;
using Restival.Api.OpenRasta.Security;
using Restival.Data;

#pragma warning disable 618

namespace Restival.Api.OpenRasta {
    public class Configuration : IConfigurationSource {
        public void Configure() {
            using (OpenRastaConfiguration.Manual) {

                ResourceSpace.Uses.CustomDependency<IDataStore, FakeDataStore>(DependencyLifetime.Singleton);

                // In this implementation, we're using the 'deprecated' OpenRasta authentication pipeline - it's 
                // not actually deprecated per se, it's just been earmarked for migrating into a standalone
                // authentication module but this hasn't happened yet. It works perfectly, though.
                ResourceSpace.Uses.PipelineContributor<AuthenticationContributor>();
                ResourceSpace.Uses.PipelineContributor<AuthenticationChallengerContributor>();
                ResourceSpace.Uses.CustomDependency<IAuthenticationScheme, BasicAuthenticationScheme>(DependencyLifetime.Singleton);
                ResourceSpace.Uses.CustomDependency<IBasicAuthenticator, RestivalAuthenticator>(DependencyLifetime.Transient);

                ResourceSpace.Has.ResourcesOfType<Greeting>()
                    .AtUri("/hello")
                    .And.AtUri("/hello/{name}")
                    .HandledBy<HelloHandler>()
                    .AsJsonDataContract();

                ResourceSpace.Has.ResourcesOfType<WhoAmIResponse>()
                    .AtUri("/whoami")
                    .HandledBy<WhoAmIHandler>()
                    .TranscodedBy<JsonCodec>();
            }
        }
    }
}

#pragma warning restore 618