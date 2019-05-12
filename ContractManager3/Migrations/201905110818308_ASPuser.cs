namespace ContractManager3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ASPuser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ContractDetails", newName: "ContractDetail");
            RenameTable(name: "dbo.Suppliers", newName: "Supplier");
            RenameTable(name: "dbo.ContractHours", newName: "ContractHour");
            RenameTable(name: "dbo.Properties", newName: "Property");
            DropForeignKey("dbo.ContractDetails", "Supplier_ID", "dbo.Suppliers");
            DropForeignKey("dbo.ContractHours", "Contract_ID", "dbo.ContractDetails");
            DropForeignKey("dbo.ContractHours", "Property_ID", "dbo.Properties");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            AddColumn("dbo.ContractDetail", "State", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetRoles", "Description", c => c.String());
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Company", c => c.String());
            AlterColumn("dbo.ContractDetail", "PriceDescription", c => c.String());
            AlterColumn("dbo.Supplier", "SupplierNumber", c => c.String());
            AlterColumn("dbo.Supplier", "SupplierName", c => c.String());
            AlterColumn("dbo.Supplier", "SupplierAddress", c => c.String());
            AlterColumn("dbo.Supplier", "SupplierCounty", c => c.String());
            AlterColumn("dbo.Supplier", "SupplierContact", c => c.String());
            AlterColumn("dbo.Supplier", "SupplierEMail", c => c.String());
            AlterColumn("dbo.Property", "Prop_Address", c => c.String());
            AlterColumn("dbo.Property", "Prop_County", c => c.String());
            AlterColumn("dbo.Property", "Cost_Centre", c => c.String());
            AlterColumn("dbo.Property", "OPW_Building_Code", c => c.String());
            AddForeignKey("dbo.ContractDetail", "Supplier_ID", "dbo.Supplier", "Supplier_ID");
            AddForeignKey("dbo.ContractHour", "Contract_ID", "dbo.ContractDetail", "Contract_ID");
            AddForeignKey("dbo.ContractHour", "Property_ID", "dbo.Property", "Property_ID");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ContractHour", "Property_ID", "dbo.Property");
            DropForeignKey("dbo.ContractHour", "Contract_ID", "dbo.ContractDetail");
            DropForeignKey("dbo.ContractDetail", "Supplier_ID", "dbo.Supplier");
            AlterColumn("dbo.Property", "OPW_Building_Code", c => c.String(nullable: false));
            AlterColumn("dbo.Property", "Cost_Centre", c => c.String(nullable: false));
            AlterColumn("dbo.Property", "Prop_County", c => c.String(nullable: false));
            AlterColumn("dbo.Property", "Prop_Address", c => c.String(nullable: false));
            AlterColumn("dbo.Supplier", "SupplierEMail", c => c.String(nullable: false));
            AlterColumn("dbo.Supplier", "SupplierContact", c => c.String(nullable: false));
            AlterColumn("dbo.Supplier", "SupplierCounty", c => c.String(nullable: false));
            AlterColumn("dbo.Supplier", "SupplierAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Supplier", "SupplierName", c => c.String(nullable: false));
            AlterColumn("dbo.Supplier", "SupplierNumber", c => c.String(nullable: false));
            AlterColumn("dbo.ContractDetail", "PriceDescription", c => c.String(nullable: false));
            DropColumn("dbo.AspNetUsers", "Company");
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropColumn("dbo.AspNetRoles", "Description");
            DropColumn("dbo.ContractDetail", "State");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ContractHours", "Property_ID", "dbo.Properties", "Property_ID", cascadeDelete: true);
            AddForeignKey("dbo.ContractHours", "Contract_ID", "dbo.ContractDetails", "Contract_ID", cascadeDelete: true);
            AddForeignKey("dbo.ContractDetails", "Supplier_ID", "dbo.Suppliers", "Supplier_ID", cascadeDelete: true);
            RenameTable(name: "dbo.Property", newName: "Properties");
            RenameTable(name: "dbo.ContractHour", newName: "ContractHours");
            RenameTable(name: "dbo.Supplier", newName: "Suppliers");
            RenameTable(name: "dbo.ContractDetail", newName: "ContractDetails");
        }
    }
}
