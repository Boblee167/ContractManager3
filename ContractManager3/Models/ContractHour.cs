using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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


        public WeekDay Weekday { get; set; }

        private double _dailyhours;
        public double DailyHours
        {
            get
            {
                return _dailyhours;
            }
            set
            {
                _dailyhours = value;
            }
        }

        private double _weeklyhours;
        public double WeeklyHours
        {
            get
            {
                return _weeklyhours;
            }

            set
            {
                if (Property_ID == this.Property_ID && Contract_ID == this.Contract_ID)
                {
                    if (_weeklyhours == 0)
                    {
                        _weeklyhours = DailyHours;
                    }
                    else
                    {
                        _weeklyhours = _weeklyhours + DailyHours;

                    }
                }
            }
        }

        public string SupplierApprover { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime SupplierApprovalDate { get; set; }

        public string DeaspApprover { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DEASPApprovalDate { get; set; }

        private double _annualhours;
        public double AnnualHours
        {
            get
            {
                return _annualhours;
            }
            set
            {
                _annualhours = WeeklyHours * 52 + Dayhours366;
            }
        }

        private double _annualcost;
        public double AnnualCost
        {
            get
            {
                return _annualcost;
            }
            set
            {

                _annualcost = _annualhours;// * price;

            }
        }


        private double _avgmonthlyhours = 0;
        public double AvgMonthlyHours
        {
            get
            {
                return _avgmonthlyhours;
            }
            set
            {
                _avgmonthlyhours = _annualhours / 12;
            }
        }

        private double _costpersqmtr = 0;
        public double CostperSQmtr
        {
            get
            {
                return _costpersqmtr;
            }
            set
            {
                _costpersqmtr = _annualcost;/// Property.SquareMetre;
            }
        }

        private double _costperstaff = 0;
        public double CostperStaff
        {
            get
            {
                return _costperstaff;
            }
            set
            {
                _costperstaff = _annualcost;/// Property.StaffCapacity;
            }
        }

        [ForeignKey("ContractDetail")]
        public int Contract_ID { get; set; }



        // Navigation Properties
        public virtual Property Property { get; set; }
        public virtual ContractDetail ContractDetail { get; set; }

        // Fields to hold details of the days Bank Holidays fall on in the year.
        private int _currentyear;
        public int CurrentYear
        {
            get
            {
                return _currentyear;
            }
            set
            {
                int CurrentYear = DateTime.Now.Year;
                _currentyear = CurrentYear;
            }
        }
        private bool _LeapYear;
        public bool LeapYear
        {
            get
            {
                return _LeapYear;
            }
            set
            {
                var x = GetLeapYear(this.CurrentYear);
                _LeapYear = x;
            }
        }

        private WeekDay _xmasday;
        public WeekDay Xmasday
        {
            get
            {
                return _xmasday;
            }
            set
            {
                DateTime x = new DateTime(CurrentYear, 12, 25);
                var day = x.ToString("ddd");
                var v = calcweekday(day);
                _xmasday = v;
            }
        }

        private WeekDay _boxingday;
        public WeekDay Boxingday
        {
            get
            {
                return _boxingday;
            }
            set
            {
                DateTime x = new DateTime(CurrentYear, 12, 26);
                var day = x.ToString("ddd");
                var v = calcweekday(day);
                _boxingday = v;
            }
        }

        private WeekDay _day365;
        public WeekDay Day365
        {
            get
            { return _day365; }
            set
            {
                DateTime x = new DateTime(CurrentYear, 1, 1);
                var day = x.ToString("ddd");
                var v = calcweekday(day);
                _day365 = v;
            }
        }

        private WeekDay _day366;
        public WeekDay Day366
        {
            get
            {
                return _day366;
            }
            set
            {
                DateTime x = new DateTime(CurrentYear, 12, 26);
                var day = x.ToString("ddd");
                var v = calcweekday(day);
                _day366 = v;

            }
        }

        // Fields to hold details of the Annual hours linked to contracts / Prpoerties
        private double _dayhours365;
        public double Dayhours365
        {
            get
            {
                return _dayhours365;
            }
            set
            {
                var x = Calc365dayhours(this.Contract_ID, this.Property_ID, this.Day365);
                _dayhours365 = x;
            }
        }
        private double _dayhours366;
        public double Dayhours366
        {
            get
            {
                return _dayhours366;
            }
            set
            {
                var x = Calc366dayhours(this.Contract_ID, this.Property_ID, this.Day366);
                _dayhours366 = x;
            }
        }
        private double _xmasdayhours;
        public double XmasDayHours
        {
            get
            {
                return _xmasdayhours;
            }
            set
            {
                var x = Xmasdayhours(this.Contract_ID, this.Property_ID, this.Xmasday);
                _xmasdayhours = x;
            }
        }
        private double _boxingdayhours;
        public double BoxingDayHours
        {
            get
            {
                return _boxingdayhours;
            }
            set
            {
                var x = CalcBoxingdayhours(this.Contract_ID, this.Property_ID, this.Xmasday);
                _boxingdayhours = x;
            }
        }

        private double _Mondaydayhours;
        public double MondayDayHours
        {
            get
            {
                return _Mondaydayhours;
            }
            set
            {
                var x = CalcMondayhours(this.Contract_ID, this.Property_ID);
                _Mondaydayhours = x;
            }
        }

        private double _goodfridayhours;
        public double GoodFridayhours
        {
            get
            {
                return _goodfridayhours;
            }
            set
            {
                var x = CalcGoodFridayhours(this.Contract_ID, this.Property_ID);
                _goodfridayhours = x;
            }
        }

        private double _bankholidayhours;
        public double BankholidayHours

        {
            get
            {
                return _bankholidayhours;
            }
            set
            {
                _bankholidayhours = ((MondayDayHours * 6) + GoodFridayhours + XmasDayHours + BoxingDayHours + Dayhours365);
            }
        }

        // Check to ensure that calculation of hours starts from zero base.
        public int count = 0;

        // Calculate Days that Bank Holidays fall on in the Current Year.
        // Calculate Current Year

        public int GetCurrentYear()
        {
            int CurrentYear = DateTime.Now.Year;

            return CurrentYear;
        }

        // Calculate if it is a leap yearCurrent Year

        public bool GetLeapYear(int CurrentYear)
        {
            if (DateTime.IsLeapYear(CurrentYear))
                return true;
            else return false;
        }

        // Method to convert string to Weekday for Bank holidays
        public WeekDay calcweekday(string day)
        {
            // Convert string to Weekday
            if (day == "Mon")
            {
                return WeekDay.Mon;
            }
            else if (day == "Tue")
            {
                return WeekDay.Tue;
            }
            else if (day == "Wed")
            {
                return WeekDay.Wed;
            }
            else if (day == "Thu")
            {
                return WeekDay.Thu;
            }
            else if (day == "Fri")
            {
                return WeekDay.Fri;
            }
            else if (day == "Sat")
            {
                return WeekDay.Sat;
            }
            else
            {
                return WeekDay.Sun;
            }

        }

        // Calculate what day the 1 January and 31 December fall on. this is day 365
        public WeekDay Calc365day(int CurrentYear)
        {
            DateTime x = new DateTime(CurrentYear, 1, 1);
            var day = x.ToString("ddd");
            Day365 = calcweekday(day);
            return (Day365);

        }

        // Calculate what day xmas fall on. this is Year
        public WeekDay Calcxmasday(int CurrentYear)
        {
            DateTime x = new DateTime(CurrentYear, 12, 25);
            var day = x.ToString("ddd");
            Xmasday = calcweekday(day);
            return (Xmasday);
        }

        // Calculate what day xmas fall on. this is Year
        public WeekDay Calcboxingday(int CurrentYear)
        {
            DateTime x = new DateTime(CurrentYear, 12, 26);
            var day = x.ToString("ddd");
            Boxingday = calcweekday(day);
            return (Boxingday);
        }

        // Calculate what day the Leap year falls on
        public WeekDay Calc366day(int CurrentYear)
        {
            DateTime x = new DateTime(CurrentYear, 1, 2);
            var day = x.ToString("ddd");
            Day366 = calcweekday(day);
            return (Day366);
        }


        //
        // Calculate the Number of Bank Holiday hours related each property on the contract in the current year.
        //

        // Calculate the Hours related to the 1 January (day 365)
        public double Calc365dayhours(int Contract_id, int Property_id, WeekDay Calc365day)
        {
            using (var db = new ApplicationDbContext())
            {
                var Dayhours365 = db.ContractHours.Where(e => e.Contract_ID == Contract_id && e.Property_ID == Property_id && e.Weekday == Calc365day)
                .Select(e => e.DailyHours);

            }
            return Dayhours365;
        }

        // Calculate the Hours related to the Leap year (day 366)
        public double Calc366dayhours(int Contract_id, int Property_id, WeekDay Calc366day)
        {
            using (var db = new ApplicationDbContext())
            {
                var Dayhours366 = db.ContractHours.Where(e => e.Weekday == Calc366day && e.Contract_ID == Contract_id)
                .Select(e => e.DailyHours);
            }

            return (Dayhours366);
        }

        // Calculate the hours on Bank holiday monday per contract_ID and property_ID 
        public double CalcMondayhours(int Contract_id, int Property_id)
        {
            using (var db = new ApplicationDbContext())
            {
                var MondayDayHours = db.ContractHours.Where(e => e.Weekday == WeekDay.Mon && e.Contract_ID == Contract_id)
                .Select(e => e.DailyHours);
            }

            return MondayDayHours;
        }

        // Calculate the Hours related to the Christmas Day
        public double Xmasdayhours(int Contract_id, int Property_id, WeekDay xmasday)
        {
            using (var db = new ApplicationDbContext())

            {
                var XmasDayHours = db.ContractHours.Where(e => e.Weekday == xmasday && e.Contract_ID == Contract_id)
                .Select(e => e.DailyHours);
            }

            return XmasDayHours;
        }

        // Calculate the Hours related to Stephens Day
        public double CalcBoxingdayhours(int Contract_id, int Property_id, WeekDay boxingday)
        {
            using (var db = new ApplicationDbContext())
            {
                var BoxingDayHours = db.ContractHours.Where(e => e.Weekday == boxingday && e.Contract_ID == Contract_id)
                .Select(e => e.DailyHours);
            }

            return BoxingDayHours;
        }

        // Calculate the hours on Bank holiday monday per contract_ID and property_ID 
        public double CalcGoodFridayhours(int Contract_id, int Property_id)
        {
            using (var db = new ApplicationDbContext())
            {
                var GoodFridayHours = db.ContractHours.Where(e => e.Weekday == WeekDay.Fri && e.Contract_ID == Contract_id)
               .Select(e => e.DailyHours);
            }

            return GoodFridayhours;
        }

        //
        // Calculate the total Number of Bank Holiday hours related to the contract in the current year.
        //

        // Calculate the Bank Holidays hours to be excluded from the contract this is Year 
        // Irish Bank holidays + good friday are made up as follows (6 Bank holiday mondays + new years day + christmas + Boxing Bay
        public double CalcBankHolidayHours(int Contract_id, int Property_id)
        {
            using (var db = new ApplicationDbContext())

            {
                double HolidayHours = (MondayDayHours * 6 + GoodFridayhours + XmasDayHours + BoxingDayHours + Dayhours365);
                BankholidayHours = HolidayHours;

            }
            return BankholidayHours;
        }


        //
        // Calculate the total Number of Contract hours in a year including Bank Holiday Hours (current year)
        //

        // Calculate the Weekly Hours of a Property that also Pays for Bank Holidays (365 days)
        public double Calcweeklyhours(int Contract_id)
        {

            var db = new ApplicationDbContext();

            foreach (var Property_id in db.ContractHours)
            {
                foreach (var Weekday in db.ContractHours)
                {
                    if (count == 0)
                    {
                        WeeklyHours += DailyHours;
                        count++;
                    }
                    else
                    {
                        WeeklyHours += DailyHours;
                        count++;
                    }

                    //To be fixed Type issue double to model??
                    //db.ContractHours.Add(WeeklyHours); xxx
                    //db.SaveChanges();     
                }

            }
            return WeeklyHours;


        }

       // Calculate the Annual Hours of a contract that also Pays for Bank Holidays (365 days)
        public double CalcAnnualhours(bool LeapYear)
        {

            if (LeapYear == false)
            {
                // Calculate 365 days. the daily hours is for day 365.(non Leap year)
                var db = new ApplicationDbContext();
                foreach (var property_id in db.ContractHours)
                    AnnualHours = (WeeklyHours * 52) + Dayhours365;

            }
            else
            {
                // Calculate 365 days. the daily hours is for day 365.(non Leap year)
                var db = new ApplicationDbContext();
                foreach (var property_id in db.ContractHours)
                    AnnualHours = (WeeklyHours * 52 + Dayhours366);

            }
            
            return AnnualHours;
        }
       




    }

}









