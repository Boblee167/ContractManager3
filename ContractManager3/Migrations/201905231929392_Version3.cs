namespace ContractManager3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Property", "DateOpened", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Property", "DateClosed", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Property", "DateClosed", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Property", "DateOpened", c => c.DateTime(nullable: false));
        }
    }
}
