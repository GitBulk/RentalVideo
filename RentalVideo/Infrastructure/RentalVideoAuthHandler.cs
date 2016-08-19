using RentalVideo.Infrastructure.Extensions;
using RentalVideo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace RentalVideo.Infrastructure
{
    public class RentalVideoAuthHandler : DelegatingHandler
    {
        IEnumerable<string> authHeaderValues = null;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                request.Headers.TryGetValues("Authorization", out authHeaderValues);
                if (authHeaderValues == null)
                {
                    return base.SendAsync(request, cancellationToken);
                }
                var tokens = authHeaderValues.FirstOrDefault();
                tokens = tokens.Replace("Basic", "").Trim();
                if (!string.IsNullOrEmpty(tokens))
                {
                    byte[] data = Convert.FromBase64String(tokens);
                    string decodedString = Encoding.UTF8.GetString(data);
                    string[] tokensValues = decodedString.Split(':');
                    var membershipService = request.GetMembershipService();
                    string username = tokensValues[0];
                    string password = tokensValues[1];
                    MembershipContext membershipContext = membershipService.ValidateUser(username, password);
                    if (membershipContext.User != null)
                    {
                        IPrincipal principal = membershipContext.Principal;
                        Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;
                    }
                    else // unauthorized access - wrong credentials
                    {
                        GetTaskHttpResponseMessage(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    return GetTaskHttpResponseMessage(HttpStatusCode.Forbidden);
                }
                return base.SendAsync(request, cancellationToken);
            }
            catch (Exception)
            {
                return GetTaskHttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }

        private static Task<HttpResponseMessage> GetTaskHttpResponseMessage(HttpStatusCode status)
        {
            var response = new HttpResponseMessage(status);
            var task = new TaskCompletionSource<HttpResponseMessage>();
            task.SetResult(response);
            return task.Task;
        }
    }
}