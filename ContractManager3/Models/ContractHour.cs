using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public double Annualhours = 0;

        
        public double CalcAnnualhours(double Weeklyhours)
        {
            var DB = new ApplicationDbContext();
            foreach (var property_id in DB.ContractHours)
            {
                Annualhours = (Weeklyhours * 52);
               
            }
            return Annualhours;
        }

        // Calculate what day the 1 January and 31 December fall on. this is day 365
        public String Calc365day()
        {
            DateTime datevalue = new DateTime(2008, 1, 1);
            return datevalue.ToString("ddd");
        }

        // Calculate the Hours the 1 January and 31 December fall on. this is day 365

        public double Calc365dayhours(int Contract_id, int Property_id, String Calc365day)
        {
            var db = new ApplicationDbContext();
            var hours = db.ContractHours.FindAsync(Contract_id,Property_ID, Calc365day);
            return DailyHours;
        }















    }
}