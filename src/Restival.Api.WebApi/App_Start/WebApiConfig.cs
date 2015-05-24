using System.Web.Http;

namespace Restival.Api.WebApi {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            config.MapHttpAttributeRoutes();
            config.EnableSystemDiagnosticsTracing();
        }
    }
}