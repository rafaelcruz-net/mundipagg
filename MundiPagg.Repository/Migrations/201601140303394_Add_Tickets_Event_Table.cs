namespace MundiPagg.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Tickets_Event_Table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerAddress", "IdCustomer", "dbo.Customer");
            DropPrimaryKey("dbo.Customer");
            CreateTable(
                "dbo.CustomerEvent",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdEvent = c.Guid(nullable: false),
                        DtEvent = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        IdCustomer = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Event", t => t.IdEvent, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.IdCustomer, cascadeDelete: true)
                .Index(t => t.IdEvent)
                .Index(t => t.IdCustomer);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventId = c.Guid(nullable: false),
                        Picture = c.String(),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            AlterColumn("dbo.Customer", "CustomerId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Customer", "CustomerId");
            AddForeignKey("dbo.CustomerAddress", "IdCustomer", "dbo.Customer", "CustomerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAddress", "IdCustomer", "dbo.Customer");
            DropForeignKey("dbo.CustomerEvent", "IdCustomer", "dbo.Customer");
            DropForeignKey("dbo.CustomerEvent", "IdEvent", "dbo.Event");
            DropIndex("dbo.CustomerEvent", new[] { "IdCustomer" });
            DropIndex("dbo.CustomerEvent", new[] { "IdEvent" });
            DropPrimaryKey("dbo.Customer");
            AlterColumn("dbo.Customer", "CustomerId", c => c.Guid(nullable: false, identity: true));
            DropTable("dbo.Event");
            DropTable("dbo.CustomerEvent");
            AddPrimaryKey("dbo.Customer", "CustomerId");
            AddForeignKey("dbo.CustomerAddress", "IdCustomer", "dbo.Customer", "CustomerId", cascadeDelete: true);
        }
    }
}
