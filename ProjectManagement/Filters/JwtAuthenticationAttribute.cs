using ProjectManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace ProjectManagement.Filters
{
    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            context.Principal = null;
            var request = context.Request;

            string token;
            if (request.Headers.GetValues("Authorization").Count() != 0)
            {
                token = request.Headers.GetValues("Authorization").FirstOrDefault();
                token = token.Replace("\"", "");
            }
            else return null;
            if (token != null)
            {
                var principal = JWT.GetPrincipal(token);
                context.Principal = principal;
            }

            if (context.Principal == null)
            {
                context.ErrorResult
                = new UnauthorizedResult(new AuthenticationHeaderValue[] {
                       new AuthenticationHeaderValue("Bearer") }, context.Request);
            }
            return Task.FromResult<object>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }
    }
}