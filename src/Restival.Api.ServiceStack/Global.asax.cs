using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Restival.Api.ServiceStack.Services;
using ServiceStack;

namespace Restival.Api.ServiceStack {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication {
        public class AppHost : AppHostBase {
            //Tell ServiceStack the name of your application and where to find your services
            public AppHost() : base("Restival", typeof(HelloService).Assembly) { }

            public override void Configure(Funq.Container container) {
                //register any dependencies your services use, e.g:
                //container.Register<ICacheClient>(new MemoryCacheClient());
            }
        }

        //Initialize your application singleton
        protected void Application_Start(object sender, EventArgs e) {
            new AppHost().Init();
        }
    }
}