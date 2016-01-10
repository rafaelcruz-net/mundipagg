namespace MundiPagg.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CustomerAdressId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        CityId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerAdressId)
                .ForeignKey("dbo.CustomerAddress", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        CustomerAdressId = c.Int(nullable: false, identity: true),
                        UF = c.String(nullable: false, maxLength: 2),
                        Name = c.String(nullable: false, maxLength: 200),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerAdressId)
                .ForeignKey("dbo.City", t => t.StateId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.CustomerAddress",
                c => new
                    {
                        CustomerAdressId = c.Guid(nullable: false, identity: true),
                        IdCustomer = c.Guid(nullable: false),
                        Cep = c.String(nullable: false, maxLength: 9),
                        Address = c.String(nullable: false, maxLength: 500),
                        Complement = c.String(nullable: false, maxLength: 200),
                        Number = c.String(nullable: false, maxLength: 5),
                        Neighbor = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CustomerAdressId)
                .ForeignKey("dbo.Customer", t => t.IdCustomer, cascadeDelete: true)
                .Index(t => t.IdCustomer);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.Guid(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 200),
                        CustomerCPF = c.String(nullable: false, maxLength: 11),
                        CustomerBirthday = c.DateTime(nullable: false),
                        CustomerGenre = c.String(nullable: false, maxLength: 1),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAddress", "IdCustomer", "dbo.Customer");
            DropForeignKey("dbo.City", "CityId", "dbo.CustomerAddress");
            DropForeignKey("dbo.State", "StateId", "dbo.City");
            DropIndex("dbo.CustomerAddress", new[] { "IdCustomer" });
            DropIndex("dbo.State", new[] { "StateId" });
            DropIndex("dbo.City", new[] { "CityId" });
            DropTable("dbo.Customer");
            DropTable("dbo.CustomerAddress");
            DropTable("dbo.State");
            DropTable("dbo.City");
        }
    }
}
