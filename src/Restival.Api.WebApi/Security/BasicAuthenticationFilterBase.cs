using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Restival.Data;

namespace Restival.Api.WebApi.Security {
    public class BasicAuthenticationFilter : BasicAuthenticationFilterBase {
        private readonly IDataStore db;

        public BasicAuthenticationFilter(IDataStore db) {
            this.db = db;
        }

        protected override async Task<IPrincipal> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken) {
            var userRecord = db.FindUserByUsername(userName);
            if (userRecord != null && userRecord.Password == password) {
                var identity = new BasicAuthenticationIdentity(userName, password);
                return (new GenericPrincipal(identity, new string[] { }));
            }
            return (null);
        }
    }
    public abstract class BasicAuthenticationFilterBase : IAuthenticationFilter {

        public string Realm { get; set; }

        private bool RequireBasicAuthentication(HttpActionContext actionContext) {
            var ad = actionContext.ActionDescriptor;
            if (ad.GetCustomAttributes<RequireBasicAuthenticationAttribute>(true).Any()) return (true);
            var cd = ad.ControllerDescriptor;
            if (cd.GetCustomAttributes<RequireBasicAuthenticationAttribute>(true).Any()) return (true);
            return (false);
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken) {
            if (!RequireBasicAuthentication(context.ActionContext)) return;
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null) {
                // No authentication was attempted (for this authentication method).
                // Do not set either Principal (which would indicate success) or ErrorResult (indicating an error).
                return;
            }

            if (authorization.Scheme != "Basic") {
                // No authentication was attempted (for this authentication method).
                // Do not set either Principal (which would indicate success) or ErrorResult (indicating an error).
                return;
            }

            if (String.IsNullOrEmpty(authorization.Parameter)) {
                // Authentication was attempted but failed. Set ErrorResult to indicate an error.
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
                return;
            }

            var userNameAndPasword = ExtractUserNameAndPassword(authorization.Parameter);

            if (userNameAndPasword == null) {
                // Authentication was attempted but failed. Set ErrorResult to indicate an error.
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);
                return;
            }

            var userName = userNameAndPasword.Item1;
            var password = userNameAndPasword.Item2;

            var principal = await AuthenticateAsync(userName, password, cancellationToken);

            if (principal == null) {
                // Authentication was attempted but failed. Set ErrorResult to indicate an error.
                context.ErrorResult = new AuthenticationFailureResult("Invalid username or password", request);
            } else {
                // Authentication was attempted and succeeded. Set Principal to the authenticated user.
                context.Principal = principal;
            }
        }

        protected abstract Task<IPrincipal> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken);

        private static Tuple<string, string> ExtractUserNameAndPassword(string authorizationParameter) {
            byte[] credentialBytes;

            try {
                credentialBytes = Convert.FromBase64String(authorizationParameter);
            } catch (FormatException) {
                return null;
            }

            // The currently approved HTTP 1.1 specification says characters here are ISO-8859-1.
            // However, the current draft updated specification for HTTP 1.1 indicates this encoding is infrequently
            // used in practice and defines behavior only for ASCII.
            var encoding = Encoding.ASCII;
            // Make a writable copy of the encoding to enable setting a decoder fallback.
            encoding = (Encoding)encoding.Clone();
            // Fail on invalid bytes rather than silently replacing and continuing.
            encoding.DecoderFallback = DecoderFallback.ExceptionFallback;
            string decodedCredentials;

            try {
                decodedCredentials = encoding.GetString(credentialBytes);
            } catch (DecoderFallbackException) {
                return null;
            }

            if (String.IsNullOrEmpty(decodedCredentials)) {
                return null;
            }

            var colonIndex = decodedCredentials.IndexOf(':');

            if (colonIndex == -1) {
                return null;
            }

            var userName = decodedCredentials.Substring(0, colonIndex);
            var password = decodedCredentials.Substring(colonIndex + 1);
            return new Tuple<string, string>(userName, password);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken) {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context) {
            var parameter = String.IsNullOrEmpty(Realm) ? null : "realm=\"" + Realm + "\"";
            context.ChallengeWith("Basic", parameter);
        }

        public virtual bool AllowMultiple {
            get { return true; }
        }
    }
}
