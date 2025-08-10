using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;  // Importa Entity Framework para trabajar con base de datos

namespace Aeropost.Models
{
    /// <summary>
    /// Clase de servicio que actúa como contexto de base de datos y capa de acceso a datos
    /// Hereda de DbContext para proporcionar funcionalidad ORM (Object-Relational Mapping)
    /// </summary>
    public class Service : DbContext
    {
        public DbSet<Clientes> clientes { get; set; }
    }
}