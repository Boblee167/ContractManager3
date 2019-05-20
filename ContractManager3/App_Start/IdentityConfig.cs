using ContractManager3.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContractManager3
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }


    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore): base(roleStore) { }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
           var applicationRoleManager =  new ApplicationRoleManager(new RoleStore<ApplicationRole>(context.Get<ApplicationDbContext>()));
           return applicationRoleManager;
        }
    }

    public class ApplicationDbInitializer
    : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            {
                try
                {
                    var property = new List<Property>
            {
                 new Property{Prop_Address="	Ardee (Social Welfare Branch Office), Moore Hall, Ardee, Co. Louth 	", Prop_County="	Co. Louth	",Type = Property_Type.Branch_Office,   Cost_Centre="	V3315	",OPW_Building_Code="		",Team=Property_Team.Team_North ,SquareMetre=   0   ,StaffCapacity= 0   ,CarParkSpots=  0   ,Lease_ID=  null    ,},
            };

                    foreach (Property p in property)
                    {
                        var existingproperty = context.Property.Where(x => x.Prop_Address == p.Prop_Address && x.Prop_County == p.Prop_County && x.Cost_Centre == p.Cost_Centre).FirstOrDefault();
                        if (existingproperty == null)
                        {
                            context.Property.Add(p);
                        }
                    }
                    context.SaveChanges();
                }

                catch (Exception e)
                {
                    Console.WriteLine("{0} Exception caught.", e);
                }

                var Supplier = new List<Supplier>
            {
            new Supplier{SupplierNumber="	SN001	",SupplierName="	SARS	",SupplierAddress="	A8 Centrepoint Business Park, Oak Road, Dublin 12	",SupplierCounty="	Dublin	",SupplierContact="	Eugene Sloan	",SupplierEMail="	info@sargroup.ie	"},


            };

                foreach (Supplier s in Supplier)
                {
                    var existingsupplier = context.Supplier.Where(x => x.SupplierName == s.SupplierName && x.SupplierAddress == s.SupplierAddress && x.SupplierNumber == s.SupplierNumber).FirstOrDefault();
                    if (existingsupplier == null)
                    {
                        context.Supplier.Add(s);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}

   



