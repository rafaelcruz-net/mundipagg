namespace MundiPagg.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PaymentTokenizer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerPaymentTokenizer",
                c => new
                    {
                        CustomerPaymentTokenizerId = c.Guid(nullable: false),
                        Token = c.String(),
                        SecurityCode = c.String(),
                        IdCustomer = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerPaymentTokenizerId)
                .ForeignKey("dbo.Customer", t => t.IdCustomer, cascadeDelete: true)
                .Index(t => t.IdCustomer);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerPaymentTokenizer", "IdCustomer", "dbo.Customer");
            DropIndex("dbo.CustomerPaymentTokenizer", new[] { "IdCustomer" });
            DropTable("dbo.CustomerPaymentTokenizer");
        }
    }
}
