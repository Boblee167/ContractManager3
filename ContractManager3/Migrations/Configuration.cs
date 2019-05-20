namespace ContractManager3.Migrations
{
    using ContractManager3.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContractManager3.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContractManager3.Models.ApplicationDbContext context)
        {
            try
            {
                var property = new List<Property>
            {
                 new Property{Prop_Address="	Ardee (Social Welfare Branch Office), Moore Hall, Ardee, Co. Louth 	", Prop_County="	Co. Louth	",Type = Property_Type.Branch_Office,   Cost_Centre="	V3315	",OPW_Building_Code="		",Team=Property_Team.Team_North ,SquareMetre=   0   ,StaffCapacity= 0   ,CarParkSpots=  0   ,Lease_ID=  null    ,},
            };

                foreach (Property p in property)
                {
                    var existingproperty = context.Property.Where(x => x.Prop_Address == p.Prop_Address && x.Prop_County == p.Prop_County && x.Cost_Centre == p.Cost_Centre).FirstOrDefault();
                    if (existingproperty == null)
                    {
                        context.Property.Add(p);
                    }
                }
                context.SaveChanges();
            }

            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

            var Supplier = new List<Supplier>
            {
            new Supplier{SupplierNumber="	SN001	",SupplierName="	SARS	",SupplierAddress="	A8 Centrepoint Business Park, Oak Road, Dublin 12	",SupplierCounty="	Dublin	",SupplierContact="	Eugene Sloan	",SupplierEMail="	info@sargroup.ie	"},


            };

            foreach (Supplier s in Supplier)
            {
                var existingsupplier = context.Supplier.Where(x => x.SupplierName == s.SupplierName && x.SupplierAddress == s.SupplierAddress && x.SupplierNumber == s.SupplierNumber).FirstOrDefault();
                if (existingsupplier == null)
                {
                    context.Supplier.Add(s);
                }
            }
            context.SaveChanges();
        }
    }
}

