namespace ContractManager3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdminUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "company", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetRoles", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetRoles", "Description", c => c.String());
            AlterColumn("dbo.AspNetUsers", "company", c => c.String(nullable: false));
        }
    }
}
