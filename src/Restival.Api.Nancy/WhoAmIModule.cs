﻿using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;
using Nancy;
using Nancy.Security;
using Restival.Api.Common.Resources;
using Restival.Data;

namespace Restival.Api.Nancy {
    public class WhoAmIModule : NancyModule {
        private readonly IDataStore db;

        public WhoAmIModule(IDataStore db) {
            this.db = db;
            this.RequiresAuthentication();
            Get["/whoami"] = _ => {
                var user = db.FindUserByUsername(this.Context.CurrentUser.UserName);
                return (new WhoAmIResponse() {
                    Id = user.Id,
                    Username = user.Username,
                    Name = user.Name
                });
            };
        }
    }
}