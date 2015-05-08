﻿using System.Web.Http;
using Restival.Api.Common.Resources;

namespace Restival.Api.WebApi.Controllers {
    public class HelloController : ApiController {
        public Greeting Get(string name) {
            return (new Greeting(name));
        }
    }
}