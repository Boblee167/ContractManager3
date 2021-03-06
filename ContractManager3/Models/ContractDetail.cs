﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ContractManager3.Models
{
    public enum Service
    {
        [Display(Name = "Security")] Security,
        [Display(Name = "Waste")] Waste,
        [Display(Name = "Office Cleaning")] Cleaning,
        [Display(Name = "Fire Equipment")] FireEquipment,
        [Display(Name = "Transport")] Transport,
        [Display(Name = "WaterCoolersMains")] WaterCoolersMains,
        [Display(Name = "WaterCoolersBottled")] WaterCoolersBottled,
        [Display(Name = "Q_Management")] Q_Management,
        [Display(Name = "Cheques")] Cheques,
        [Display(Name = "Canteen")] Canteen,
        [Display(Name = "StorageBoxes")] StorageBoxes,
        [Display(Name = "Taxis")] Taxis,
        [Display(Name = "Uniforms")] Uniforms,
        [Display(Name = "Cleaning Products")] CleaningProd,
        [Display(Name = "Document Storage")] DocStorage,
        [Display(Name = "WhiteGoods")] WhiteGoods,
        [Display(Name = "Pest Control")] PestControl,
        [Display(Name = "Window Cleaning")] WindowCleaning,
        [Display(Name = "LightCateringEquipment")] LightCateringEquipment,
        [Display(Name = "Personal Protection Equipment")] PPE,
        [Display(Name = "Confidential Shredding")] Shredding,
        [Display(Name = "Photocopying Paper")] Paper,
        [Display(Name = "Stationery")] Stationery,
        [Display(Name = "Envelopes")] Envelopes,
        [Display(Name = "DocumentStorage")] DocumentStorage,
    }


    public class ContractDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Contract_ID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ContractStartDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ContractFinishDate { get; set; }

        //[Required]
        public int ContractExtensionsAvailable { get; set; }

        //[Required]
        public int DurationContactExtension { get; set; }

        //[Required]
        public Service Servicetype { get; set; }

        //[Required]
        public string PriceDescription { get; set; }

        //[Required]
        public double Price { get; set; }

        //[Required]
        public double VatRate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PriceUpdatedate { get; set; }

        [ForeignKey("Supplier")]
        public int Supplier_ID { get; set; }

        [ForeignKey("Property")]
        public int Property_ID { get; set; }

        //Navigation Property
        public virtual Supplier Supplier { get; set; }
        public virtual List<ContractHour> Contracthour { get; set; }
        public virtual List<Property> Property { get; set; }


        // Methods
        // Calculate the Annual Cost of the Contract by Property this is day 365
        public Double YearlyCost = 0;

        public double CalcYearlyCost()
        {
            var db = new ApplicationDbContext();

            return YearlyCost;
        }

    }

}