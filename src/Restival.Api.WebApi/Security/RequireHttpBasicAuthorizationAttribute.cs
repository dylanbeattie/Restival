using Restival.Data;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Restival.Api.WebApi.Security {

    public class RequireHttpBasicAuthorizationAttribute : AuthorizeAttribute {
        private readonly string realm;

        public RequireHttpBasicAuthorizationAttribute(string realm = "HTTP Basic") {
            this.realm = realm;
        }

        public IDataStore DataStore { get; set; }

        protected override bool IsAuthorized(HttpActionContext actionContext) {
            var principal = actionContext.ControllerContext.RequestContext.Principal;
            if (principal != null && principal.Identity != null && principal.Identity.IsAuthenticated) return (true);
            var auth = actionContext.Request.Headers.Authorization;
            if (auth == null) return (false);
            var credential = ParseCredential(auth);

            if (TryValidateCredential(credential, out principal)) {
                Thread.CurrentPrincipal = actionContext.RequestContext.Principal = principal;
                return (true);
            }
            return (false);
        }

        private bool TryValidateCredential(NetworkCredential credential, out IPrincipal principal) {
            principal = null;
            if (credential == null) return (false);
            var user = DataStore.FindUserByUsername(credential.UserName);
            if (user == null || user.Password != credential.Password) return (false);
            var identity = new GenericIdentity(user.Username, "Basic");
            principal = new GenericPrincipal(identity, new string[] { });
            return (true);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext) {
            base.HandleUnauthorizedRequest(actionContext);
            if (actionContext.Request.Headers.Authorization != null) return;
            var headerValue = String.Format("Basic realm=\"{0}\"", realm);
            actionContext.Response.Headers.Add("WWW-Authenticate", headerValue);
        }

        /// <summary>Returns a <see cref="NetworkCredential"/> containing the username and password encoded in an HTTP Basic authorization header. If the header is missing or invalid, returns null.</summary>
        /// <param name="header">An <see cref="AuthenticationHeaderValue"/> containing an HTTP Basic authorization header.</param>
        /// <returns>A <see cref="NetworkCredential"/> containing the decoded username and password, or null of the header is not a valid authorization header.</returns>
        public static NetworkCredential ParseCredential(AuthenticationHeaderValue header) {
            switch (header.Scheme) {
                case "Basic":
                    try {
                        var credentialString = Encoding.ASCII.GetString(Convert.FromBase64String(header.Parameter));
                        var tokens = credentialString.Split(new[] { ':' }, 2, StringSplitOptions.None);
                        return tokens.Length != 2 ? (null) : (new NetworkCredential(tokens[0], tokens[1]));
                    } catch (FormatException) {
                        return null;
                    }
                default:
                    return (null);
            }
        }
    }
}