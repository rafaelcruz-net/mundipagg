namespace MundiPagg.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTableCodIbge : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.State", "CodIbge", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.State", "CodIbge", c => c.String(nullable: false));
        }
    }
}
