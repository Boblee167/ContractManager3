using ContractManager3.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContractManager3.Controllers
{
    public class ContractHoursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContractHours
        public async Task<ActionResult> Index()
        {
            var contractHours = db.ContractHours.Include(c => c.ContractDetail).Include(c => c.Property);
            return View(await contractHours.ToListAsync());
        }

        // GET: ContractHours/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractHour contractHour = await db.ContractHours.FindAsync(id);
            if (contractHour == null)
            {
                return HttpNotFound();
            }
            return View(contractHour);
        }

        // GET: ContractHours/Create
        public ActionResult Create()
        {
            ViewBag.Contract_ID = new SelectList(db.ContractDetails, "Contract_ID", "PriceDescription");
            ViewBag.Property_ID = new SelectList(db.Property, "Property_ID", "Prop_Address");
            return View();
        }

        // POST: ContractHours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Transaction_ID,Property_ID,Contract_ID,Weekday,DailyHours,HoursUpdatedDate,WeeklyHours,AvgMonthlyHours")] ContractHour contractHour)
        {
            if (ModelState.IsValid)
            {
                db.ContractHours.Add(contractHour);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Contract_ID = new SelectList(db.ContractDetails, "Contract_ID", "PriceDescription", contractHour.Contract_ID);
            ViewBag.Property_ID = new SelectList(db.Property, "Property_ID", "Prop_Address", contractHour.Property_ID);
            return View(contractHour);
        }

        // GET: ContractHours/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractHour contractHour = await db.ContractHours.FindAsync(id);
            if (contractHour == null)
            {
                return HttpNotFound();
            }
            ViewBag.Contract_ID = new SelectList(db.ContractDetails, "Contract_ID", "PriceDescription", contractHour.Contract_ID);
            ViewBag.Property_ID = new SelectList(db.Property, "Property_ID", "Prop_Address", contractHour.Property_ID);
            return View(contractHour);
        }

        // POST: ContractHours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Transaction_ID,Property_ID,Contract_ID,Weekday,DailyHours,HoursUpdatedDate,WeeklyHours,AvgMonthlyHours")] ContractHour contractHour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractHour).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Contract_ID = new SelectList(db.ContractDetails, "Contract_ID", "PriceDescription", contractHour.Contract_ID);
            ViewBag.Property_ID = new SelectList(db.Property, "Property_ID", "Prop_Address", contractHour.Property_ID);
            return View(contractHour);
        }

        // GET: ContractHours/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractHour contractHour = await db.ContractHours.FindAsync(id);
            if (contractHour == null)
            {
                return HttpNotFound();
            }
            return View(contractHour);
        }

        // POST: ContractHours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ContractHour contractHour = await db.ContractHours.FindAsync(id);
            db.ContractHours.Remove(contractHour);
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
