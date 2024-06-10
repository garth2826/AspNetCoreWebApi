using AspNetCoreWebApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace AspNetCoreWebApi.handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        WideWorldImportersContext context;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder
            ) : base(options, logger, encoder)
        {
            context = new WideWorldImportersContext();
        }
        protected override Task<Microsoft.AspNetCore.Authentication.AuthenticateResult> HandleAuthenticateAsync()
        {
#if true
            //garthtest
            //test code do not remove.
            var clains2 = new[] { new Claim(ClaimTypes.Name,  "admin") };
            var identity2 = new ClaimsIdentity(clains2, Scheme.Name);
            var principle2 = new ClaimsPrincipal(identity2);
            var ticket2 = new AuthenticationTicket(principle2, Scheme.Name);

            return Task.FromResult(Microsoft.AspNetCore.Authentication.AuthenticateResult.Success(ticket2));
            //end of garthtest
#endif
            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(Microsoft.AspNetCore.Authentication.AuthenticateResult.Fail("Authorization header was not found"));
            //return AuthenticateResult.Fail("Authorization header was not found");
            var header = Request.Headers["Authorization"].FirstOrDefault();

            try
            {
                if (header == null)
                {
                    return Task.FromResult(Microsoft.AspNetCore.Authentication.AuthenticateResult.Fail("Request header was not found"));
                }
                else
                {
                    var authenticationHeaderValue = AuthenticationHeaderValue.Parse(header);
                    var ecryptCode = authenticationHeaderValue.Parameter;
                    if (ecryptCode != null)
                    {
                        var bytes = Convert.FromBase64String(ecryptCode);
                        var credentials = Encoding.UTF8.GetString(bytes).Split(":");
                        //string ID = creentials[0];
                        string username = credentials[0] ?? "";

                        string? password = credentials[1];
                        var garthDevice = context.GarthDevices.Where(device => device.Username == username && device.Password == password).FirstOrDefault();
                        if (garthDevice == null)
                        {
                            return Task.FromResult(Microsoft.AspNetCore.Authentication.AuthenticateResult.Fail("Invalid username"));

                        }
                        else
                        {
                            var clains = new[] { new Claim(ClaimTypes.Name, garthDevice.Username ?? "") };
                            var identity = new ClaimsIdentity(clains, Scheme.Name);
                            var principle = new ClaimsPrincipal(identity);
                            var ticket = new AuthenticationTicket(principle, Scheme.Name);

                            return Task.FromResult(Microsoft.AspNetCore.Authentication.AuthenticateResult.Success(ticket));

                        }
                    }

                }
             

            }
            catch (Exception) 
            {
                return Task.FromResult(AuthenticateResult.Fail("Error has occured"));
            }
            return Task.FromResult(AuthenticateResult.Fail(""));
            //return AuthenticateResult.Fail("Need to implement");
            //string info = "need to implement";
            //return Task.FromResult(AuthenticateResult.Fail(info));
        }
    }



}
