namespace ContractManager3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContractDetail", "ContractStartDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ContractDetail", "ContractFinishDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ContractDetail", "PriceUpdatedate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.ContractHour", "HoursUpdatedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContractHour", "HoursUpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ContractDetail", "PriceUpdatedate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ContractDetail", "ContractFinishDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ContractDetail", "ContractStartDate", c => c.DateTime(nullable: false));
        }
    }
}
