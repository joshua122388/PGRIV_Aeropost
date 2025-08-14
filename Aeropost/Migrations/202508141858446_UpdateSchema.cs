namespace Aeropost.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSchema : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Paquetes", "CedulaCliente", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Paquetes", "TiendaOrigen", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Paquetes", "NumeroTracking", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Paquetes", "NumeroTracking", c => c.String());
            AlterColumn("dbo.Paquetes", "TiendaOrigen", c => c.String());
            AlterColumn("dbo.Paquetes", "CedulaCliente", c => c.String());
        }
    }
}
