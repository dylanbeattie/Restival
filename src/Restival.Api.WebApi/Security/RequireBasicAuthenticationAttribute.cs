using System;
using System.Web.Http.Filters;

namespace Restival.Api.WebApi.Security {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class RequireBasicAuthenticationAttribute : FilterAttribute {}
}