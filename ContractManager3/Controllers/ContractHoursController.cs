using ContractManager3.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContractManager3.Controllers
{
    public class ContractHoursController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
          private IDeploymentsContext db = new ApplicationDbContext();

            public ContractHoursController() { }

            public ContractHoursController(IDeploymentsContext context)
            {
                db = context;
            }
                                    
            // GET: ContractHours
            public async Task<ActionResult> Index()
        {
            var contractHours = db.ContractHours.Include(c => c.Contract_ID).Include(c => c.Property);
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
            ViewBag.Contract_ID = new SelectList(db.ContractDetail, "Contract_ID", "PriceDescription");
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
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Contract_ID = new SelectList(db.ContractDetail, "Contract_ID", "PriceDescription", contractHour.Contract_ID);
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
            ViewBag.Contract_ID = new SelectList(db.ContractDetail, "Contract_ID", "PriceDescription", contractHour.Contract_ID);
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
                db.MarkAsModified(contractHour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Contract_ID = new SelectList(db.ContractDetail, "Contract_ID", "PriceDescription", contractHour.Contract_ID);
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


        // POST: Calc
        [HttpPost]
        public ActionResult CalcVariableDays(ContractHour calc)
        {
            if (ModelState.IsValid)
            {
                // No errors added to ModelState
                // Get a value for the current year.(int)
                calc.CurrentYear = calc.GetCurrentYear();
                //Calculate if it is a leap year. (Bool)
                calc.LeapYear = calc.GetLeapYear(calc.CurrentYear);
                //Calculate what day xmas falls on in the current year.(String)
                calc.Xmasday = calc.Calcxmasday(calc.CurrentYear);
                //Calculate what day St Stephens day falls on in the current year (Day 366).(String)   
                calc.Boxingday = calc.Calcboxingday(calc.CurrentYear);
                //Calculate what day New Years falls on in the current year (Day 365).(String)  
                calc.Day365 = calc.Calc365day(calc.CurrentYear);
                //Calculate what day is after New Years day falls on in the current year (Day 366).(String)   
                calc.Day366 = calc.Calc366day(calc.CurrentYear);

                return RedirectToAction("Answer", calc);

            }
            // Show the form again (for correction)
            return View(calc);
        }


        //POST: Calc
        [HttpPost]
        public ActionResult CalcHours(ContractHour hours)
        {
            if (ModelState.IsValid)
            {
                //No errors added to ModelState
                // Get a value for the current year.(int)
                hours.Calcweeklyhours();
                hours.CalcAnnualhours(hours.WeeklyHours, hours.LeapYear);
                hours.CalcBankHolidayHours();
                hours.Dayhours365 = hours.Calc365dayhours(hours.Contract_ID, hours.Property_ID, hours.Day365);
                hours.Calc366dayhours(hours.Contract_ID, hours.Property_ID, hours.Day366);
                hours.Xmasdayhours(hours.Contract_ID, hours.Property_ID, hours.Xmasday);
                hours.CalcBoxingdayhours(hours.Contract_ID, hours.Property_ID, hours.Boxingday);
                hours.CalcMondayhours(hours.Contract_ID, hours.Property_ID);
                hours.GoodFridayhours = hours.CalcGoodFridayhours(hours.Contract_ID, hours.Property_ID);
            }
                return View(hours);
        }
            
           
    }
}


