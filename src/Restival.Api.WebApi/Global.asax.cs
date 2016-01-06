using System.Web;
using System.Web.Http;
using Restival.Api.WebApi.LightInject;
using Restival.Data;

namespace Restival.Api.WebApi {
    public class WebApiApplication : HttpApplication {
        protected void Application_Start() {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var container = new ServiceContainer();
            container.RegisterApiControllers();
            container.Register<IDataStore, FakeDataStore>();
            container.EnablePerWebRequestScope();
            container.EnableWebApi(GlobalConfiguration.Configuration);
        }
    }
}
