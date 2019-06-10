using ContractManager3;
using ContractManager3.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ContractManager3.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
           private ApplicationRoleManager _roleManager;

            // Empty role constructor
            public RoleController() { }

            // Empty role constructor that takes a parameter roleManager
            public RoleController(ApplicationRoleManager roleManager)
            {
                RoleManager = roleManager;
            }

            public ApplicationRoleManager RoleManager
            {
                get
                {
                    return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
                }
                private set
                {
                    _roleManager = value;
                }
            }


            // GET: Role

            public ActionResult Index()
            {
                List<RoleViewModel> list = new List<RoleViewModel>();
                foreach (var role in RoleManager.Roles)
                    list.Add(new RoleViewModel(role));
                return View(list);
            }

            // GET: Create
            public ActionResult Create()
            {
                return View();
            }


            [HttpPost]
            public async Task<ActionResult> Create(RoleViewModel model)
            {
                var role = new ApplicationRole() { Name = model.Name };
                await RoleManager.CreateAsync(role);
                return RedirectToAction("Index");
            }

            public async Task<ActionResult> Edit(string Id)
            {
                var role = await RoleManager.FindByIdAsync(Id);
                return View(new RoleViewModel(role));
            }

            [HttpPost]
            public async Task<ActionResult> Edit(RoleViewModel model)
            {
                var role = new ApplicationRole() { Id = model.Id, Name = model.Name };
                await RoleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }

            public async Task<ActionResult> Details(string id)
            {
                var role = await RoleManager.FindByIdAsync(id);
                return View(new RoleViewModel(role));

            }

            // Get display view for Delete method 
            public async Task<ActionResult> Delete(string id)
            {
                var role = await RoleManager.FindByIdAsync(id);
                return View(new RoleViewModel(role));
            }

            // posted Delete method
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> DeleteConfirmed(string id)
            {
                var role = await RoleManager.FindByIdAsync(id);
                await RoleManager.DeleteAsync(role);
                return RedirectToAction("Index");
            }


    }


}//[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
    private ApplicationRoleManager _roleManager;

    // Empty role constructor
    public RoleController() { }

    // Empty role constructor that takes a parameter roleManager
    public RoleController(ApplicationRoleManager roleManager)
    {
        RoleManager = roleManager;
    }

    public ApplicationRoleManager RoleManager
    {
        get
        {
            return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
        }
        private set
        {
            _roleManager = value;
        }
    }


    // GET: Role

    public ActionResult Index()
    {
        List<RoleViewModel> list = new List<RoleViewModel>();
        foreach (var role in RoleManager.Roles)
            list.Add(new RoleViewModel(role));
        return View(list);
    }

    // GET: Create
    public ActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<ActionResult> Create(RoleViewModel model)
    {
        var role = new ApplicationRole() { Name = model.Name };
        await RoleManager.CreateAsync(role);
        return RedirectToAction("Index");
    }

    public async Task<ActionResult> Edit(string Id)
    {
        var role = await RoleManager.FindByIdAsync(Id);
        return View(new RoleViewModel(role));
    }

    [HttpPost]
    public async Task<ActionResult> Edit(RoleViewModel model)
    {
        var role = new ApplicationRole() { Id = model.Id, Name = model.Name };
        await RoleManager.UpdateAsync(role);
        return RedirectToAction("Index");
    }

    public async Task<ActionResult> Details(string id)
    {
        var role = await RoleManager.FindByIdAsync(id);
        return View(new RoleViewModel(role));

    }

    // Get display view for Delete method 
    public async Task<ActionResult> Delete(string id)
    {
        var role = await RoleManager.FindByIdAsync(id);
        return View(new RoleViewModel(role));
    }

    // posted Delete method
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(string id)
    {
        var role = await RoleManager.FindByIdAsync(id);
        await RoleManager.DeleteAsync(role);
        return RedirectToAction("Index");
    }


}

