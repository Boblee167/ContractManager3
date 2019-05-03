using ContractManager3.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContractManager3.Controllers
{
    public class PropertyController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
            private IDeploymentsContext db = new ApplicationDbContext();

            public PropertyController() { }

            public PropertyController(IDeploymentsContext context)
            {
                db = context;
            }
                                    
            // GET: Property
            public async Task<ActionResult> Index()
        {
            return View(await db.Property.ToListAsync());
        }

        // GET: Property/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = await db.Property.FindAsync(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: Property/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Property/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Property_ID,Prop_Address,Prop_County,Type,Cost_Centre,OPW_Building_Code,Team,SquareMetre,StaffCapacity,CarParkSpots,DateOpened,DateClosed,Lease_ID")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Property.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(property);
        }

        // GET: Property/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = await db.Property.FindAsync(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Property/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Property_ID,Prop_Address,Prop_County,Type,Cost_Centre,OPW_Building_Code,Team,SquareMetre,StaffCapacity,CarParkSpots,DateOpened,DateClosed,Lease_ID")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.MarkAsModified(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(property);
        }

        // GET: Property/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = await db.Property.FindAsync(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Property/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Property property = await db.Property.FindAsync(id);
            db.Property.Remove(property);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
