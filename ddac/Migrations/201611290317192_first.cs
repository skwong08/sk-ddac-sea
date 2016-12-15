namespace ddac.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.countries", t => t.country_Id)
                .Index(t => t.country_Id);
            
            CreateTable(
                "dbo.cruises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Total_Classic_Cabin = c.Int(nullable: false),
                        Total_Luxury_Cabin = c.Int(nullable: false),
                        Classic_Cabin_Price = c.Int(nullable: false),
                        Luxury_Cabin_Price = c.Int(nullable: false),
                        cruise_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.cruises", t => t.cruise_Id)
                .Index(t => t.cruise_Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Email = c.String(),
                        Contact = c.String(),
                        Type = c.String(),
                        Price = c.Int(nullable: false),
                        DepartureLocation = c.String(),
                        ArrivalLocation = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        DayDuration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        Days = c.Int(nullable: false),
                        dName = c.String(),
                        aName = c.String(),
                        sName = c.String(),
                        ArrivalLocation_Id = c.Int(),
                        DepartureLocation_Id = c.Int(),
                        Ships_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.locations", t => t.ArrivalLocation_Id)
                .ForeignKey("dbo.locations", t => t.DepartureLocation_Id)
                .ForeignKey("dbo.ships", t => t.Ships_Id)
                .Index(t => t.ArrivalLocation_Id)
                .Index(t => t.DepartureLocation_Id)
                .Index(t => t.Ships_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "Ships_Id", "dbo.ships");
            DropForeignKey("dbo.Trips", "DepartureLocation_Id", "dbo.locations");
            DropForeignKey("dbo.Trips", "ArrivalLocation_Id", "dbo.locations");
            DropForeignKey("dbo.ships", "cruise_Id", "dbo.cruises");
            DropForeignKey("dbo.locations", "country_Id", "dbo.countries");
            DropIndex("dbo.Trips", new[] { "Ships_Id" });
            DropIndex("dbo.Trips", new[] { "DepartureLocation_Id" });
            DropIndex("dbo.Trips", new[] { "ArrivalLocation_Id" });
            DropIndex("dbo.ships", new[] { "cruise_Id" });
            DropIndex("dbo.locations", new[] { "country_Id" });
            DropTable("dbo.Trips");
            DropTable("dbo.Tickets");
            DropTable("dbo.ships");
            DropTable("dbo.cruises");
            DropTable("dbo.locations");
            DropTable("dbo.countries");
        }
    }
}
