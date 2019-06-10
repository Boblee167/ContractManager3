using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContractManager3.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }


        public enum Company
        {
            [Display(Name = "DEASP")] DEASP,
            [Display(Name = "SARS")] SARS,
            [Display(Name = "Country Clean Ltd")] Country_Clean_Ltd,
            [Display(Name = "William Tracey & Sons")] William_Tracey_and_Sons,
            [Display(Name = "Glenpatrick Water")] Glenpatrick_Water,
            [Display(Name = "MSC Fire")] MSC_Fire,
            [Display(Name = "Tipperary Water")] Tipperary_Water,
            [Display(Name = "JC Hendrick")] Hendrick,
            [Display(Name = "DaFil")] DaFil,
            [Display(Name = "R&B Burke Catering")] Burke_Catering,
            [Display(Name = "Kefron Ltd")] Kefron_Ltd,
            [Display(Name = "Mytaxi")] Mytaxi,
            [Display(Name = "Tailored Image Ltd")] Tailored_Image_Ltd,
            [Display(Name = "Bunzl Irl Ltd")] Bunzl_Irl_Ltd,
            [Display(Name = "Iron Mountain")] Iron_Mountain,
            [Display(Name = "DID Electrical")] DID_Electrical,
            [Display(Name = "Rentokil")] Rentokil,
            [Display(Name = "Emerald")] Emerald,
            [Display(Name = "Bunzl Mc Loughlin")] Bunzl_Mc_Loughlin,
            [Display(Name = "JBS Group")] JBS,
            [Display(Name = "Rehab Recycle Ltd")] Rehab_Recycle_Ltd,
            [Display(Name = "Antalis")] Antalis,
            [Display(Name = "Codex")] Codex,
            [Display(Name = "Trimfold Ltd")] Trimfold_Ltd,
        }

        [Display(Name = "Company"), Required]
        public Company company { get; set; }

    }


    // Create a role Class 
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string rolename) : base(rolename) { }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>,IDeploymentsContext
    {
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<ContractDetail> ContractDetail { get; set; }
        public DbSet<ContractHour> ContractHours { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }

        public void MarkAsModified(object item)
        {
            throw new NotImplementedException();
        }

        //public System.Data.Entity.DbSet<ContractManager3.Models.RoleViewModel> RoleViewModels { get; set; }

        // public System.Data.Entity.DbSet<ContractManager3.Models.ApplicationUser> ApplicationUsers { get; set; }

        // public System.Data.Entity.DbSet<ContractManager3.Models.RoleViewModel> RoleViewModels { get; set; }

        // public System.Data.Entity.DbSet<ContractManager3.Models.RoleViewModel> RoleViewModels { get; set; }
    }
}





