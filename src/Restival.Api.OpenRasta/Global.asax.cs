using OpenRasta.Configuration;
using Restival.Api.Common.Resources;
using Restival.Api.OpenRasta.Handlers;

namespace Restival.Api.OpenRasta {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {

        }
    }

    public class Configuration : IConfigurationSource {
        public void Configure() {
            using (OpenRastaConfiguration.Manual) {
                ResourceSpace.Has.ResourcesOfType<Greeting>()
                    .AtUri("/hello")
                    .And.AtUri("/hello?name={name}")
                    .HandledBy<HelloHandler>()
                    .AsXmlDataContract()
                    .And.AsJsonDataContract();
            }
        }
    }
}