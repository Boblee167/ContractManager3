namespace ContractManager3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractDetail",
                c => new
                    {
                        Contract_ID = c.Int(nullable: false, identity: true),
                        ContractStartDate = c.DateTime(nullable: false),
                        ContractFinishDate = c.DateTime(nullable: false),
                        ContractExtensionsAvailable = c.Int(nullable: false),
                        DurationContactExtension = c.Int(nullable: false),
                        Servicetype = c.Int(nullable: false),
                        PriceDescription = c.String(),
                        Price = c.Double(nullable: false),
                        VatRate = c.Double(nullable: false),
                        PriceUpdatedate = c.DateTime(nullable: false),
                        Supplier_ID = c.Int(nullable: false),
                        Property_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Contract_ID)
                .ForeignKey("dbo.Supplier", t => t.Supplier_ID)
                .Index(t => t.Supplier_ID);
            
            CreateTable(
                "dbo.ContractHour",
                c => new
                    {
                        Transaction_ID = c.Int(nullable: false, identity: true),
                        Property_ID = c.Int(nullable: false),
                        Weekday = c.Int(nullable: false),
                        DailyHours = c.Double(nullable: false),
                        HoursUpdatedDate = c.DateTime(nullable: false),
                        WeeklyHours = c.Double(nullable: false),
                        AvgMonthlyHours = c.Double(nullable: false),
                        Contract_ID = c.Int(nullable: false),
                        CurrentYear = c.Int(nullable: false),
                        LeapYear = c.Boolean(nullable: false),
                        Xmasday = c.String(),
                        Boxingday = c.String(),
                        Day365 = c.String(),
                        Day366 = c.String(),
                        Annualhours = c.Double(nullable: false),
                        Dayhours365 = c.Double(nullable: false),
                        Dayhours366 = c.Double(nullable: false),
                        XmasDayHours = c.Double(nullable: false),
                        BoxingDayHours = c.Double(nullable: false),
                        MondayDayHours = c.Double(nullable: false),
                        GoodFridayhours = c.Double(nullable: false),
                        BankholidayHours = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Transaction_ID)
                .ForeignKey("dbo.ContractDetail", t => t.Contract_ID)
                .ForeignKey("dbo.Property", t => t.Property_ID)
                .Index(t => t.Property_ID)
                .Index(t => t.Contract_ID);
            
            CreateTable(
                "dbo.Property",
                c => new
                    {
                        Property_ID = c.Int(nullable: false, identity: true),
                        Prop_Address = c.String(),
                        Prop_County = c.String(),
                        Type = c.Int(nullable: false),
                        Cost_Centre = c.String(),
                        OPW_Building_Code = c.String(),
                        Team = c.Int(nullable: false),
                        SquareMetre = c.Int(),
                        StaffCapacity = c.Int(),
                        CarParkSpots = c.Int(),
                        DateOpened = c.DateTime(nullable: false),
                        DateClosed = c.DateTime(nullable: false),
                        Lease_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Property_ID);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        Supplier_ID = c.Int(nullable: false, identity: true),
                        SupplierNumber = c.String(),
                        SupplierName = c.String(),
                        SupplierAddress = c.String(),
                        SupplierCounty = c.String(),
                        SupplierContact = c.String(),
                        SupplierEMail = c.String(),
                    })
                .PrimaryKey(t => t.Supplier_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Company = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ContractDetailProperty",
                c => new
                    {
                        ContractDetail_Contract_ID = c.Int(nullable: false),
                        Property_Property_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContractDetail_Contract_ID, t.Property_Property_ID })
                .ForeignKey("dbo.ContractDetail", t => t.ContractDetail_Contract_ID, cascadeDelete: true)
                .ForeignKey("dbo.Property", t => t.Property_Property_ID, cascadeDelete: true)
                .Index(t => t.ContractDetail_Contract_ID)
                .Index(t => t.Property_Property_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ContractDetail", "Supplier_ID", "dbo.Supplier");
            DropForeignKey("dbo.ContractDetailProperty", "Property_Property_ID", "dbo.Property");
            DropForeignKey("dbo.ContractDetailProperty", "ContractDetail_Contract_ID", "dbo.ContractDetail");
            DropForeignKey("dbo.ContractHour", "Property_ID", "dbo.Property");
            DropForeignKey("dbo.ContractHour", "Contract_ID", "dbo.ContractDetail");
            DropIndex("dbo.ContractDetailProperty", new[] { "Property_Property_ID" });
            DropIndex("dbo.ContractDetailProperty", new[] { "ContractDetail_Contract_ID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ContractHour", new[] { "Contract_ID" });
            DropIndex("dbo.ContractHour", new[] { "Property_ID" });
            DropIndex("dbo.ContractDetail", new[] { "Supplier_ID" });
            DropTable("dbo.ContractDetailProperty");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Supplier");
            DropTable("dbo.Property");
            DropTable("dbo.ContractHour");
            DropTable("dbo.ContractDetail");
        }
    }
}
