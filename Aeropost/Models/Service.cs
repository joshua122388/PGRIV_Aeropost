using Microsoft.EntityFrameworkCore;

namespace Aeropost.Models
{
    public class Service : DbContext
    {
        public DbSet<Cliente> clientes { get; set; }
    }
}
