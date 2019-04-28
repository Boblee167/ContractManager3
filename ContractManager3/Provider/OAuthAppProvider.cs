
using ContractManager3.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup I am creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "admin@ead.com";
                user.Email = "admin@ead.com";

                string userPWD = "whatever";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }

}

