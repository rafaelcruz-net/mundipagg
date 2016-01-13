namespace MundiPagg.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableCodIbge1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerAddress", "IdCustomer", "dbo.Customer");
            DropIndex("dbo.CustomerAddress", new[] { "IdCustomer" });
            DropPrimaryKey("dbo.Customer");
            AddColumn("dbo.CustomerAddress", "Customer_Id", c => c.Guid());
            AlterColumn("dbo.Customer", "CustomerId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Customer", "CustomerId");
            CreateIndex("dbo.CustomerAddress", "Customer_Id");
            AddForeignKey("dbo.CustomerAddress", "Customer_Id", "dbo.Customer", "CustomerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAddress", "Customer_Id", "dbo.Customer");
            DropIndex("dbo.CustomerAddress", new[] { "Customer_Id" });
            DropPrimaryKey("dbo.Customer");
            AlterColumn("dbo.Customer", "CustomerId", c => c.Guid(nullable: false, identity: true));
            DropColumn("dbo.CustomerAddress", "Customer_Id");
            AddPrimaryKey("dbo.Customer", "CustomerId");
            CreateIndex("dbo.CustomerAddress", "IdCustomer");
            AddForeignKey("dbo.CustomerAddress", "IdCustomer", "dbo.Customer", "CustomerId", cascadeDelete: true);
        }
    }
}
