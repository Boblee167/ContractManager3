using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ContractManager3.Models
{

    public enum WeekDay { Mon, Tue, Wed, Thu, Fri, Sat, Sun }


    public class ContractHour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Transaction_ID { get; set; }

        [ForeignKey("Property")]
        public int Property_ID { get; set; }

        [ForeignKey("ContractDetail")]
        public int Contract_ID { get; set; }

        [Required]
        public WeekDay Weekday { get; set; }

        [Required]
        public double DailyHours { get; set; }

        public DateTime HoursUpdatedDate { get; set; }

        public double WeeklyHours { get; set; }

        public double AvgMonthlyHours { get; set; }

        // Navigation Properties
        public virtual Property Property { get; set; }
        public virtual ContractDetail ContractDetail { get; set; }

        public double Annualhours { get; set; }
        public double Dayhours365 { get; set; }
        public double Dayhours366 { get; set; }
        public double XmasDayHours { get; set; }
        public double BoxingDayHours { get; set; }
        public double MondayDayHours { get; set; }
        public double GoodFridayhours { get; set; }
        public double BankholidayHours { get; set; }



        // Calculate the Weekly Hours of a Property that also Pays for Bank Holidays (365 days)
        public double Calcweeklyhours(double Weeklyhours, WeekDay Weekday)
        {
            var DB = new ApplicationDbContext();

            foreach (var contract_id in DB.ContractHours)
            {
                foreach (var property_id in DB.ContractHours)
                {
                    foreach (var weekday in DB.ContractHours)
                    {
                        if (Weeklyhours == 0)
                        {
                            Weeklyhours += DailyHours;
                        }
                        else Weeklyhours = DailyHours;
                    }
                }

            }
            return Weeklyhours;
        }




        // Calculate the Annual Hours of a contract that also Pays for Bank Holidays (365 days)



        public double CalcAnnualhours(double Weeklyhours, bool LeapYear)
        {

            if (LeapYear == false)
            {
                // Calculate 365 days. the daily hours is for day 365.(non Leap year)
                var DB = new ApplicationDbContext();
                foreach (var property_id in DB.ContractHours)
                    Annualhours = (Weeklyhours * 52 + DailyHours);
            }
            else
            {
                // Calculate 365 days. the daily hours is for day 365.(non Leap year)
                var DB = new ApplicationDbContext();
                foreach (var property_id in DB.ContractHours)
                    Annualhours = (Weeklyhours * 52 + DailyHours);

            }
            return Annualhours;
        }

        // Calculate Current Year

        public int GetCurrentYear()
        {
            int CurrentYear = DateTime.Now.Year;

            return CurrentYear;
        }

        // Calculate if it is a leap yearCurrent Year

        public bool LeapYear(int CurrentYear)
        {
            if (DateTime.IsLeapYear(CurrentYear))
                return true;
            else return false;
        }

        // Calculate what day the 1 January and 31 December fall on. this is day 365
        public String Calc365day(int CurrentYear)
        {
            DateTime day365 = new DateTime(CurrentYear, 1, 1);
            return day365.ToString("ddd");
        }

        // Calculate the Hours related to the 1 January (day 365)
        public double Calc365dayhours(int Contract_id, int Property_id, String Calc365day)
        {
            using (var db = new ApplicationDbContext())
            {
                var Dayhours365 = db.ContractHours.Where(e => e.Weekday.ToString() == Calc365day && e.Contract_ID == Contract_id && e.Property_ID == Contract_id)
                .Select(e => e.DailyHours);
            }

            return Dayhours365;
        }

        // Calculate what day the Leap year falls on
        public String Calc366day(int CurrentYear)
        {
            DateTime Calc366day = new DateTime(CurrentYear, 1, 2);
            return Calc366day.ToString("ddd");
        }

        // Calculate the Hours related to the Leap year (day 366)
        public double Calc366dayhours(int Contract_id, int Property_id, String Calc366day)
        {
            using (var db = new ApplicationDbContext())
            {
                var Dayhours366 = db.ContractHours.Where(e => e.Weekday.ToString() == Calc366day && e.Contract_ID == Contract_id && e.Property_ID == Contract_id)
                .Select(e => e.DailyHours);
            }

            return Dayhours366;
        }

             

        // Calculate what day xmas fall on. this is Year
        public String Calcxmasday(int CurrentYear)
        {
            DateTime Calcxmasday = new DateTime(CurrentYear, 12, 25);
            return Calcxmasday.ToString("ddd");
        }

        // Calculate the Hours related to the Leap year (day 366)
        public double xmasdayhours(int Contract_id, int Property_id, String xmasday)
        {
            using (var db = new ApplicationDbContext())
            {
                var XmasDayHours = db.ContractHours.Where(e => e.Weekday.ToString() == xmasday && e.Contract_ID == Contract_id && e.Property_ID == Contract_id)
                .Select(e => e.DailyHours);
            }

            return XmasDayHours;
        }


        // Calculate what day xmas fall on. this is Year
        public String Calcboxingday(int CurrentYear)
        {
            DateTime Calcboxingday = new DateTime(CurrentYear, 12, 25);
            return Calcboxingday.ToString("ddd");
        }

        public double CalcBoxingdayhours(int Contract_id, int Property_id, String boxingday)
        {
            using (var db = new ApplicationDbContext())
            {
                var BoxingDayHours = db.ContractHours.Where(e => e.Weekday.ToString() == boxingday && e.Contract_ID == Contract_id && e.Property_ID == Contract_id)
                .Select(e => e.DailyHours);
            }

            return BoxingDayHours;
        }


        // Calculate the hours on Bank holiday monday per contract_ID and property_ID 
        public double CalcMondayhours(int Contract_id, int Property_id)
        {
            using (var db = new ApplicationDbContext())
            {
                var MondayDayHours = db.ContractHours.Where(e => e.Weekday.ToString() == "Mon" && e.Contract_ID == Contract_id && e.Property_ID == Contract_id)
                .Select(e => e.DailyHours);

            }

            return MondayDayHours;
        }

        // Calculate the hours on Bank holiday monday per contract_ID and property_ID 
        public double CalcGoodFridayhours(int Contract_id, int Property_id)
        {
            using (var db = new ApplicationDbContext())
            {
                var GoodFridayhours = db.ContractHours.Where(e => e.Weekday.ToString() == "Fri" && e.Contract_ID == Contract_id && e.Property_ID == Contract_id)
                .Select(e => e.DailyHours);
            }

            return GoodFridayhours;
        }


        // Calculate the Bank Holidays hours to be excluded from the contract this is Year 
        // Irish Bank holidays + good friday are made up as follows (6 Bank holiday mondays + new years day + christmas + Boxing Bay
        public double CalcBankHolidayHours()
        {
            double HolidayHours = (MondayDayHours * 6 + GoodFridayhours + XmasDayHours + BoxingDayHours + Dayhours365);
            BankholidayHours = HolidayHours;
            return BankholidayHours;
        }
    }

}










    