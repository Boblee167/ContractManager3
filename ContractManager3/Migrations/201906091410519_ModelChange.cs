namespace ContractManager3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractHour", "SupplierApprover", c => c.String());
            AddColumn("dbo.ContractHour", "SupplierApprovalDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ContractHour", "DeaspApprover", c => c.String());
            AddColumn("dbo.ContractHour", "DEASPApprovalDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ContractHour", "AnnualCost", c => c.Double(nullable: false));
            AddColumn("dbo.ContractHour", "CostperSQmtr", c => c.Double(nullable: false));
            AddColumn("dbo.ContractHour", "CostperStaff", c => c.Double(nullable: false));
            AlterColumn("dbo.ContractHour", "Xmasday", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractHour", "Boxingday", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractHour", "Day365", c => c.Int(nullable: false));
            AlterColumn("dbo.ContractHour", "Day366", c => c.Int(nullable: false));
            AlterColumn("dbo.Property", "SquareMetre", c => c.Double(nullable: false));
            AlterColumn("dbo.Property", "StaffCapacity", c => c.Double(nullable: false));
            DropColumn("dbo.ContractHour", "HoursUpdatedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContractHour", "HoursUpdatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Property", "StaffCapacity", c => c.Int());
            AlterColumn("dbo.Property", "SquareMetre", c => c.Int());
            AlterColumn("dbo.ContractHour", "Day366", c => c.String());
            AlterColumn("dbo.ContractHour", "Day365", c => c.String());
            AlterColumn("dbo.ContractHour", "Boxingday", c => c.String());
            AlterColumn("dbo.ContractHour", "Xmasday", c => c.String());
            DropColumn("dbo.ContractHour", "CostperStaff");
            DropColumn("dbo.ContractHour", "CostperSQmtr");
            DropColumn("dbo.ContractHour", "AnnualCost");
            DropColumn("dbo.ContractHour", "DEASPApprovalDate");
            DropColumn("dbo.ContractHour", "DeaspApprover");
            DropColumn("dbo.ContractHour", "SupplierApprovalDate");
            DropColumn("dbo.ContractHour", "SupplierApprover");
        }
    }
}
