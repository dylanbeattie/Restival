using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Restival.Api.Common.Resources;

namespace Restival.Api.Nancy {
    public class HelloModule : NancyModule {
        public HelloModule() {
            Get["/hello"] = _ => {
                var name = Request.Query["name"];
                return name == null ? (new Greeting()) : (new Greeting(name));
            };

            Get["/hello/?name={name}"] = _ => new Greeting(_.name);
        }
    }
}