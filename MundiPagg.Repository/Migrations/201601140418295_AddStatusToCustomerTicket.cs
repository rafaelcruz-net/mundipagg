namespace MundiPagg.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToCustomerTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerEvent", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerEvent", "Status");
        }
    }
}
