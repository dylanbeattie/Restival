using System.Web.Http;
using Restival.Api.WebApi.Security;
using Restival.Data;

namespace Restival.Api.WebApi {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            config.MapHttpAttributeRoutes();
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
