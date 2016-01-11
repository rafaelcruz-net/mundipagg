namespace MundiPagg.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        Uf = c.String(nullable: false, maxLength: 2),
                        CodIbge = c.String(nullable: false, maxLength: 200),
                        Area = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.State", t => t.Uf, cascadeDelete: true)
                .Index(t => t.Uf);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        UF = c.String(nullable: false, maxLength: 2),
                        Name = c.String(nullable: false, maxLength: 200),
                        CodIbge = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UF);
            
            CreateTable(
                "dbo.CustomerAddress",
                c => new
                    {
                        CustomerAdressId = c.Guid(nullable: false, identity: true),
                        IdCustomer = c.Guid(nullable: false),
                        CityId = c.Int(nullable: false),
                        Cep = c.String(nullable: false, maxLength: 9),
                        Address = c.String(nullable: false, maxLength: 500),
                        Complement = c.String(nullable: false, maxLength: 200),
                        Number = c.String(nullable: false, maxLength: 5),
                        Neighbor = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.CustomerAdressId)
                .ForeignKey("dbo.City", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.IdCustomer, cascadeDelete: true)
                .Index(t => t.IdCustomer)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        CPF = c.String(nullable: false, maxLength: 11),
                        Birthday = c.DateTime(nullable: false),
                        Genre = c.String(nullable: false, maxLength: 1),
                        Email = c.String(nullable: false, maxLength: 200),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAddress", "IdCustomer", "dbo.Customer");
            DropForeignKey("dbo.CustomerAddress", "CityId", "dbo.City");
            DropForeignKey("dbo.City", "Uf", "dbo.State");
            DropIndex("dbo.CustomerAddress", new[] { "CityId" });
            DropIndex("dbo.CustomerAddress", new[] { "IdCustomer" });
            DropIndex("dbo.City", new[] { "Uf" });
            DropTable("dbo.Customer");
            DropTable("dbo.CustomerAddress");
            DropTable("dbo.State");
            DropTable("dbo.City");
        }
    }
}
