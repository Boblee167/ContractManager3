using ContractManager3.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContractManager3.Controllers
{
    public class ContractDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContractDetails
        public async Task<ActionResult> Index()
        {
            var contractDetails = db.ContractDetails.Include(c => c.Supplier);
            return View(await contractDetails.ToListAsync());
        }

        // GET: ContractDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractDetail contractDetail = await db.ContractDetails.FindAsync(id);
            if (contractDetail == null)
            {
                return HttpNotFound();
            }
            return View(contractDetail);
        }

        // GET: ContractDetails/Create
        public ActionResult Create()
        {
            ViewBag.Supplier_ID = new SelectList(db.Supplier, "Supplier_ID", "SupplierNumber");
            return View();
        }

        // POST: ContractDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Contract_ID,ContractStartDate,ContractFinishDate,ContractExtensionsAvailable,DurationContactExtension,Servicetype,PriceDescription,Price,VatRate,PriceUpdatedate,Supplier_ID")] ContractDetail contractDetail)
        {
            if (ModelState.IsValid)
            {
                db.ContractDetails.Add(contractDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Supplier_ID = new SelectList(db.Supplier, "Supplier_ID", "SupplierNumber", contractDetail.Supplier_ID);
            return View(contractDetail);
        }

        // GET: ContractDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractDetail contractDetail = await db.ContractDetails.FindAsync(id);
            if (contractDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Supplier_ID = new SelectList(db.Supplier, "Supplier_ID", "SupplierNumber", contractDetail.Supplier_ID);
            return View(contractDetail);
        }

        // POST: ContractDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Contract_ID,ContractStartDate,ContractFinishDate,ContractExtensionsAvailable,DurationContactExtension,Servicetype,PriceDescription,Price,VatRate,PriceUpdatedate,Supplier_ID")] ContractDetail contractDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Supplier_ID = new SelectList(db.Supplier, "Supplier_ID", "SupplierNumber", contractDetail.Supplier_ID);
            return View(contractDetail);
        }

        // GET: ContractDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractDetail contractDetail = await db.ContractDetails.FindAsync(id);
            if (contractDetail == null)
            {
                return HttpNotFound();
            }
            return View(contractDetail);
        }

        // POST: ContractDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ContractDetail contractDetail = await db.ContractDetails.FindAsync(id);
            db.ContractDetails.Remove(contractDetail);
            await db.SaveChangesAsync();
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
