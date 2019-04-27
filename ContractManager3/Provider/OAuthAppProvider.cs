using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContractManager3.Provider
{
    public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                if (context.UserName == "admin" && context.Password == "admin")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                    identity.AddClaim(new Claim("username", "admin"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, "administrator"));
                 
                    context.Validated(identity);
                }
                else if (context.UserName == "DEASP_user" && context.Password == "user")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "DEASP_user"));
                    identity.AddClaim(new Claim("username", "DEASP_user"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, "DEASP user"));
                    context.Validated(identity);
                }
                else if (context.UserName == "Supplier_user" && context.Password == "user")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Supplier_user"));
                    identity.AddClaim(new Claim("username", "Supplier_user"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, "Supplier user"));
                    context.Validated(identity);
                }

                else if (context.UserName == "Supplier_user" && context.Password == "user")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Supplier_user"));
                    identity.AddClaim(new Claim("username", "Supplier_user"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, "Supplier user"));
                    context.Validated(identity);
                }

                else if (context.UserName == "Pending" && context.Password == "Pending")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Pending"));
                    identity.AddClaim(new Claim("username", "Pending"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, "Pending"));
                    context.Validated(identity);
                }


                else
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                }
            });
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
           
    }
}