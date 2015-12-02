#region License

/* Authors:
 *      Sebastien Lambla (seb@serialseb.com)
 * Copyright:
 *      (C) 2007-2009 Caffeine IT & naughtyProd Ltd (http://www.caffeine-it.com)
 * License:
 *      This file is distributed under the terms of the MIT License found at the end of this file.
 */
#endregion

// Digest Authentication implementation
//  Inspired by mono's implenetation, rewritten for OpenRasta.
// Original authors:
//  Greg Reinacker (gregr@rassoc.com)
//  Sebastien Pouliot (spouliot@motus.com)
// Portions (C) 2002-2003 Greg Reinacker, Reinacker & Associates, Inc. All rights reserved.
// Portions (C) 2003 Motus Technologies Inc. (http://www.motus.com)
// Original source code available at
// http://www.rassoc.com/gregr/weblog/stories/2002/07/09/webServicesSecurityHttpDigestAuthenticationWithoutActiveDirectory.html
using System;
using System.Linq;
using System.Security.Principal;
using System.Text;
using OpenRasta.DI;
using OpenRasta.Security;
using OpenRasta.Web;
using OpenRasta.Pipeline;

namespace OpenRasta.Pipeline.Contributors {
    public class BasicAuthorizerContributor : IPipelineContributor {
        readonly IDependencyResolver _resolver;
        IAuthenticationProvider _authentication;

        public BasicAuthorizerContributor(IDependencyResolver resolver) {
            _resolver = resolver;
        }

        public void Initialize(IPipeline pipelineRunner) {
            pipelineRunner.Notify(ReadCredentials)
                .After<KnownStages.IBegin>()
                .And
                .Before<KnownStages.IHandlerSelection>();

            pipelineRunner.Notify(WriteCredentialRequest)
                .After<KnownStages.IOperationResultInvocation>()
                .And
                .Before<KnownStages.IResponseCoding>();
        }

        public PipelineContinuation ReadCredentials(ICommunicationContext context) {
            if (!_resolver.HasDependency(typeof(IAuthenticationProvider))) return PipelineContinuation.Continue;

            _authentication = _resolver.Resolve<IAuthenticationProvider>();

            var header = GetBasicHeader(context);

            if (header == null) return PipelineContinuation.Continue;

            var creds = _authentication.GetByUsername(header.Username);

            if (creds == null) return NotAuthorized(context);

            if (creds.Password != header.Password) return NotAuthorized(context);

            var identity = new GenericIdentity(creds.Username, "Basic");
            context.User = new GenericPrincipal(identity, creds.Roles);
            return PipelineContinuation.Continue;
        
    }

        static BasicAuthorizationHeader GetBasicHeader(ICommunicationContext context) {
            var header = context.Request.Headers["Authorization"];
            return string.IsNullOrEmpty(header) ? null : BasicAuthorizationHeader.Parse(header);
        }

        static PipelineContinuation ClientError(ICommunicationContext context) {
            context.OperationResult = new OperationResult.BadRequest();
            return PipelineContinuation.RenderNow;
        }

        static PipelineContinuation NotAuthorized(ICommunicationContext context) {
            context.OperationResult = new OperationResult.Unauthorized();
            return PipelineContinuation.RenderNow;
        }

        static PipelineContinuation WriteCredentialRequest(ICommunicationContext context) {
            if (context.OperationResult is OperationResult.Unauthorized) {
                context.Response.Headers["WWW-Authenticate"] =
                    new BasicAuthenticateHeader {
                        Realm = "Basic Authentication",
                    }.ServerResponseHeader;
            }
            return PipelineContinuation.Continue;
        }
    }

    public class BasicAuthenticateHeader {
        public string Realm { get; set; }

        public string ServerResponseHeader {
            get { return (String.Format("Basic realm=\"{0}\"", Realm)); }
        }
    }

    public class BasicAuthorizationHeader {
        public string Username { get; set; }
        public string Password { get; set; }

        public static BasicAuthorizationHeader Parse(string header) {
            var tokens = header.Split(' ');
            if (tokens.Length != 2) throw (new ArgumentException("Supplied header is not in the format Basic {base64-encoded credential pair}", "header"));
            if (tokens[0] != "Basic") throw (new ArgumentException("Supplied header is not an HTTP Basic authorization header", header));
            var credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(tokens[1]));
            var credentials = credentialString.Split(':');
            return (new BasicAuthorizationHeader() {
                Username = credentials[0],
                Password = credentials[1]
            });
        }
    }
}

#region Full license

// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion