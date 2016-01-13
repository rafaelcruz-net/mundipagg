namespace MundiPagg.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.City", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.City", "CodIbge", c => c.String(maxLength: 200));
            AlterColumn("dbo.City", "Area", c => c.String(maxLength: 200));
            AlterColumn("dbo.State", "Name", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.State", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.City", "Area", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.City", "CodIbge", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.City", "Name", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
