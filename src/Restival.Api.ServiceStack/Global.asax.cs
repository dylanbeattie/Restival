using System;
using System.Web;
using Funq;
using Restival.Api.ServiceStack.Services;
using Restival.Data;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Text;

namespace Restival.Api.ServiceStack {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication {
        //Initialize your application singleton
        protected void Application_Start(object sender, EventArgs e) {
            new AppHost().Init();
        }

        public class AppHost : AppHostBase {
            //Tell ServiceStack the name of your application and where to find your services
            public AppHost() : base("Restival", typeof(HelloService).Assembly) { }

            public override void Configure(Container container) {
                var db = new FakeDataStore();
                container.Register<IDataStore>(c => db).ReusedWithin(ReuseScope.Container);
                JsConfig.ExcludeTypeInfo = true;

                var auth = new AuthFeature(() => new AuthUserSession(), new IAuthProvider[] { new RestivalAuthProvider(db) }) {
                    HtmlRedirect = null
                };
                Plugins.Add(auth);
            }
        }
    }
}
