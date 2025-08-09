using Microsoft.EntityFrameworkCore;

namespace Aeropost.Models
{
    public class Service : DbContext
    {
        public DbSet<Clientes> clientes { get; set; }
    }
}
