using Microsoft.EntityFrameworkCore;

namespace Aeropost.Models
{
    public class Service : DbContext
    {
        public DbSet<Cliente> clientes { get; set; }

        // Agrega un nuevo cliente a la base de datos
        public void agregarCliente(Cliente cliente)
        {
            clientes.Add(cliente);
            SaveChanges();
        }

        // Devuelve todos los clientes guardados en la base de datos
        public Array mostrarClientes()
        {
            return clientes.ToArray();
        }

        // Busca un cliente por cédula
        public Cliente buscarCliente(string cedula)
        {
            var clienteBuscado = clientes.FirstOrDefault(x => x.Cedula == cedula);
            if (clienteBuscado != null)
                return clienteBuscado;
            else
                throw new Exception("Ese cliente no está registrado");
        }

        // Elimina un cliente de la base de datos
        public void eliminarCliente(Cliente cliente)
        {
            clientes.Remove(cliente);
            SaveChanges();
        }

        // Actualiza los datos de un cliente existente
        public void actualizarCliente(Cliente cliente)
        {
            var clienteAntiguo = clientes.FirstOrDefault(x => x.Cedula == cliente.Cedula);
            if (clienteAntiguo != null)
            {
                clienteAntiguo.Nombre = cliente.Nombre;
                clienteAntiguo.Tipo = cliente.Tipo;
                clienteAntiguo.Correo = cliente.Correo;
                clienteAntiguo.DireccionEntrega = cliente.DireccionEntrega;
                clienteAntiguo.Telefono = cliente.Telefono;
                SaveChanges();
            }
            else
                throw new Exception("No se pudo actualizar el cliente");
        }

        // Devuelve un arreglo de clientes filtrados por el tipo especificado.
        // Parámetro:
        //   tipo: Cadena que representa el tipo de cliente (por ejemplo: "Regular", "Frecuente", "Suspendido").
        // Retorna:
        //   Un arreglo de objetos Cliente con campo Tipo coincide con el valor proporcionado.
        public Array mostrarClientesPorTipo(string tipo)
        {
            // Esta línea busca y devuelve todos los clientes que tienen el tipo indicado por el parámetro 'tipo'.
            // Por ejemplo, si se llama a ese método con "Regular", solo te regresará los clientes cual el tipo sea "Regular".
            // Primero, 'clientes.Where(x => x.Tipo == tipo)' recorre toda la lista de clientes y selecciona solo los que cumplen con esa condición.
            // Después, 'ToArray()' convierte ese grupo filtrado en un arreglo para que puedas trabajar fácilmente con él.
            // Es útil cuando necesitas mostrar o procesar únicamente los clientes de un tipo específico, como para generar reportes o estadísticas.
            return clientes.Where(x => x.Tipo == tipo).ToArray();

        }


    }
}
