namespace Aeropost.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bitacoras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        NombreCompleto = c.String(),
                        FechaHora = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Cedula = c.String(nullable: false, maxLength: 20),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Tipo = c.String(nullable: false, maxLength: 20),
                        Correo = c.String(nullable: false),
                        DireccionEntrega = c.String(nullable: false, maxLength: 200),
                        Telefono = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Cedula);
            
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroFactura = c.String(),
                        NumeroTracking = c.String(),
                        CedulaCliente = c.String(),
                        FechaEntrega = c.DateTime(nullable: false),
                        MontoTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Paquetes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CedulaCliente = c.String(),
                        TiendaOrigen = c.String(),
                        Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductosEspeciales = c.Boolean(nullable: false),
                        NumeroTracking = c.String(),
                        FechaRegistro = c.DateTime(nullable: false),
                        Cliente_Cedula = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Cedula)
                .Index(t => t.Cliente_Cedula);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Cedula = c.String(),
                        Genero = c.String(),
                        FechaRegistro = c.DateTime(nullable: false),
                        Estado = c.String(),
                        User = c.String(),
                        Pass = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paquetes", "Cliente_Cedula", "dbo.Clientes");
            DropIndex("dbo.Paquetes", new[] { "Cliente_Cedula" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Paquetes");
            DropTable("dbo.Facturas");
            DropTable("dbo.Clientes");
            DropTable("dbo.Bitacoras");
        }
    }
}
