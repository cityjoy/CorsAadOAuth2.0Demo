using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Web_API.OAuth2.Demo
{
    public class OpenAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// 验证客户端
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            //context.TryGetFormCredentials(out clientId, out clientSecret);
            context.TryGetBasicCredentials(out clientId, out clientSecret); //Basic认证

            //TODO:读库，验证
            if (clientId == "user007" && clientSecret == "007go")
            {
            
                context.Validated(clientId);
            }
            return base.ValidateClientAuthentication(context);
        }

        /// <summary>
        ///     客户端授权[生成access token]
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name,"007go"));
            var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties { AllowRefresh = true });
            context.Validated(ticket);
            return base.GrantClientCredentials(context);
        }
    }
}